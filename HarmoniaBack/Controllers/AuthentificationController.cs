using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Harmonia.Controllers
{
    // Contrôleur pour gérer l'authentification (inscription, connexion, tokens)
    [ApiController]
    [Route("api/auth")]
    public class AuthentificationController : ControllerBase
    {
        private readonly UserManager<Utilisateur> _userManager;
        private readonly SignInManager<Utilisateur> _signInManager;
        private readonly IConfiguration _configuration;

        /* Constructeur avec injection des dépendances :
         * - userManager : Pour gérer les utilisateurs (création, recherche)
         * - signInManager : Pour gérer les connexions
         * - configuration : Pour accéder aux paramètres JWT
         */
        public AuthentificationController(
            UserManager<Utilisateur> userManager,
            SignInManager<Utilisateur> signInManager,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // Endpoint POST /api/auth/inscription
        // Crée un nouveau compte utilisateur
        // Retourne : Token JWT + Refresh Token + Infos utilisateur
        [HttpPost("inscription")]
        public async Task<ActionResult<object>> Inscription([FromBody] InscriptionDTO model)
        {
            // Validation minimale - 4 caractères pour pseudo et mot de passe
            if (string.IsNullOrWhiteSpace(model.Pseudo) || model.Pseudo.Length < 4)
            {
                return BadRequest(
                    new { Message = "Le pseudo doit contenir au moins 4 caractères" }
                );
            }

            if (string.IsNullOrWhiteSpace(model.MotDePasse) || model.MotDePasse.Length < 4)
            {
                return BadRequest(
                    new { Message = "Le mot de passe doit contenir au moins 4 caractères" }
                );
            }

            // Vérification doublon pseudo/email
            if (await _userManager.FindByNameAsync(model.Pseudo) != null)
            {
                return Conflict(new { Message = "Ce pseudo est déjà pris" });
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return Conflict(new { Message = "Cet email est déjà utilisé" });
            }

            // Création de l'utilisateur
            var utilisateur = new Utilisateur { UserName = model.Pseudo, Email = model.Email };

            var result = await _userManager.CreateAsync(utilisateur, model.MotDePasse);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "Erreur lors de la création du compte" });
            }

            // Génération des tokens
            var token = await GenerateJwtToken(utilisateur);
            var refreshToken = GenerateRefreshToken();

            utilisateur.RefreshToken = refreshToken;
            utilisateur.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(utilisateur);

            return Ok(
                new
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Utilisateur = new
                    {
                        Id = utilisateur.Id,
                        Pseudo = utilisateur.UserName,
                        Email = utilisateur.Email,
                    },
                }
            );
        }

        // Endpoint POST /api/auth/connexion
        // Authentifie un utilisateur existant
        // Retourne : Token JWT + Refresh Token + Infos utilisateur
        [HttpPost("connexion")]
        public async Task<ActionResult<object>> Connexion([FromBody] ConnexionDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var utilisateur = await _userManager.FindByNameAsync(model.Pseudo);

            if (utilisateur == null)
            {
                return Unauthorized(new { Message = "Pseudo ou mot de passe incorrect" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                utilisateur,
                model.MotDePasse,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                var token = await GenerateJwtToken(utilisateur);
                var refreshToken = GenerateRefreshToken();
                utilisateur.RefreshToken = refreshToken;
                utilisateur.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await _userManager.UpdateAsync(utilisateur);

                return Ok(
                    new
                    {
                        Token = token,
                        RefreshToken = refreshToken,
                        Utilisateur = new
                        {
                            Id = utilisateur.Id,
                            Pseudo = utilisateur.UserName,
                            Email = utilisateur.Email,
                        },
                    }
                );
            }

            return Unauthorized(new { Message = "Pseudo ou mot de passe incorrect" });
        }

        // Endpoint POST /api/auth/refresh-token
        // Génère un nouveau token JWT à partir d'un refresh token valide
        [HttpPost("refresh-token")]
        public async Task<ActionResult<object>> RefreshToken([FromBody] RefreshTokenDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.RefreshToken))
            {
                return BadRequest(new { Message = "Token de rafraîchissement invalide" });
            }

            var principal = GetPrincipalFromExpiredToken(model.Token);
            if (principal == null)
            {
                return BadRequest(new { Message = "Token invalide ou expiré" });
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return BadRequest(
                    new { Message = "Token malformé: identifiant utilisateur manquant" }
                );
            }

            var userId = userIdClaim.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || user.RefreshToken != model.RefreshToken)
            {
                return BadRequest(new { Message = "Token de rafraîchissement invalide" });
            }

            // Régénère le refresh token s'il expire dans moins de 24h
            if (user.RefreshTokenExpiryTime <= DateTime.Now.AddHours(24))
            {
                var newRefreshToken = GenerateRefreshToken();
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await _userManager.UpdateAsync(user);
            }

            var newToken = await GenerateJwtToken(user);

            return Ok(
                new
                {
                    Token = newToken,
                    RefreshToken = user.RefreshToken,
                    Utilisateur = new
                    {
                        Id = user.Id,
                        Pseudo = user.UserName,
                        Email = user.Email,
                    },
                }
            );
        }

        /* Génère un token JWT pour l'utilisateur :
         * - Contient l'ID, pseudo et email
         * - Valide 1 heure par défaut
         * - Signé avec la clé secrète JWT
         */
        private async Task<string> GenerateJwtToken(Utilisateur user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["JWT:Secret"] ?? "VotreCleSecreteDeFallbackMinimum32Caracteres"
                )
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Génère un refresh token aléatoire sécurisé (32 bytes en Base64)
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /* Extrait les données d'un token JWT expiré :
         * - Désactive la vérification de date (ValidateLifetime = false)
         * - Vérifie quand même la signature
         */
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["JWT:Secret"]
                            ?? "VotreCleSecreteDeFallbackMinimum32Caracteres"
                    )
                ),
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out securityToken
            );

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (
                jwtSecurityToken == null
                || !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase
                )
            )
            {
                throw new SecurityTokenException("Token invalide");
            }

            return principal;
        }
    }
}
