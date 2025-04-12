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
    [ApiController]
    [Route("api/auth")]
    public class AuthentificationController : ControllerBase
    {
        private readonly UserManager<Utilisateur> _userManager;
        private readonly SignInManager<Utilisateur> _signInManager;
        private readonly IConfiguration _configuration;

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

        [HttpPost("inscription")]
        public async Task<ActionResult<object>> Inscription([FromBody] InscriptionDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Vérifier si l'utilisateur existe déjà
            var existingUser = await _userManager.FindByNameAsync(model.Pseudo);
            if (existingUser != null)
            {
                return Conflict(new { Message = "Ce pseudo est déjà utilisé." });
            }

            existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return Conflict(new { Message = "Cette adresse email est déjà utilisée." });
            }

            // Créer le nouvel utilisateur
            var utilisateur = new Utilisateur(model);
            var result = await _userManager.CreateAsync(utilisateur, model.MotDePasse);

            if (result.Succeeded)
            {
                // Générer le token JWT
                var token = await GenerateJwtToken(utilisateur);

                // Générer un refresh token
                var refreshToken = GenerateRefreshToken();
                utilisateur.RefreshToken = refreshToken;
                utilisateur.RefreshTokenExpiryTime = DateTime.Now.AddDays(7); // Valide pour 7 jours
                await _userManager.UpdateAsync(utilisateur);

                return Ok(
                    new
                    {
                        Token = token,
                        RefreshToken = refreshToken,
                        Utilisateur = new
                        {
                            Id = utilisateur.Id,
                            Pseudo = utilisateur.Pseudo,
                            Email = utilisateur.Email,
                        },
                    }
                );
            }

            // En cas d'erreur de création d'utilisateur
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("connexion")]
        public async Task<ActionResult<object>> Connexion([FromBody] ConnexionDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Trouver l'utilisateur par pseudo
            var utilisateur = await _userManager.FindByNameAsync(model.Pseudo);

            if (utilisateur == null)
            {
                return Unauthorized(new { Message = "Pseudo ou mot de passe incorrect" });
            }

            // Vérifier le mot de passe
            var result = await _signInManager.CheckPasswordSignInAsync(
                utilisateur,
                model.MotDePasse,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                // Générer le token JWT
                var token = await GenerateJwtToken(utilisateur);

                // Générer un refresh token
                var refreshToken = GenerateRefreshToken();
                utilisateur.RefreshToken = refreshToken;
                utilisateur.RefreshTokenExpiryTime = DateTime.Now.AddDays(7); // Valide pour 7 jours
                await _userManager.UpdateAsync(utilisateur);

                return Ok(
                    new
                    {
                        Token = token,
                        RefreshToken = refreshToken,
                        Utilisateur = new
                        {
                            Id = utilisateur.Id,
                            Pseudo = utilisateur.Pseudo,
                            Email = utilisateur.Email,
                        },
                    }
                );
            }

            return Unauthorized(new { Message = "Pseudo ou mot de passe incorrect" });
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<object>> RefreshToken([FromBody] RefreshTokenDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.RefreshToken))
            {
                return BadRequest(new { Message = "Token de rafraîchissement invalide" });
            }

            // Extraire les informations du token expiré
            var principal = GetPrincipalFromExpiredToken(model.Token);
            if (principal == null)
            {
                return BadRequest(new { Message = "Token invalide ou expiré" });
            }

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            // Vérifier si le refresh token est valide
            if (user == null || user.RefreshToken != model.RefreshToken)
            {
                return BadRequest(new { Message = "Token de rafraîchissement invalide" });
            }

            // Vérifier si le refresh token est proche de l'expiration (par exemple, dans les 24 heures)
            if (user.RefreshTokenExpiryTime <= DateTime.Now.AddHours(24))
            {
                // Si le refresh token expire bientôt, on le régénère
                var newRefreshToken = GenerateRefreshToken();
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7); // Par exemple, 7 jours

                await _userManager.UpdateAsync(user);
            }

            // Générer un nouveau token JWT (Access Token)
            var newToken = await GenerateJwtToken(user);

            return Ok(
                new
                {
                    Token = newToken,
                    RefreshToken = user.RefreshToken, // Garder le même refresh token
                    Utilisateur = new
                    {
                        Id = user.Id,
                        Pseudo = user.Pseudo,
                        Email = user.Email,
                    },
                }
            );
        }

        private async Task<string> GenerateJwtToken(Utilisateur user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            // Ajouter les rôles aux claims si nécessaire
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

            // Expiration de l'access token (par exemple 1 heure)
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Expiration après 1 heure
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

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
                ValidateLifetime = false, // Ne pas valider la durée de vie
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
