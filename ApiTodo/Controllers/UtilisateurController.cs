using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Contrôleur qui gère les opérations CRUD sur les utilisateurs et leurs suites favorites
[ApiController]
[Route("api/utilisateurs")]
public class UtilisateursController : ControllerBase
{
    // Contexte de base de données pour accéder aux entités
    private readonly HarmoniaContext _context;
    // Gestionnaire d'utilisateurs fourni par Identity pour la gestion des comptes
    private readonly UserManager<Utilisateur> _userManager;

    // Constructeur qui initialise les dépendances par injection
    public UtilisateursController(HarmoniaContext context, UserManager<Utilisateur> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Récupère la liste de tous les utilisateurs
    // Renvoie une collection d'objets UtilisateurDTO contenant les informations de base
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UtilisateurDTO>>> GetUtilisateurs()
    {
        // Récupère les utilisateurs et les transforme en DTOs pour ne renvoyer que les données nécessaires
        var utilisateurs = await _userManager
            .Users.Select(u => new UtilisateurDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
            })
            .ToListAsync();

        return utilisateurs;
    }

    // Récupère une suite favorite spécifique par son ID
    // Renvoie un objet SuitesFavoritesDTO avec les détails de la suite favorite
    [HttpGet("suitesFavorites/{id}")]
    public async Task<ActionResult<SuitesFavoritesDTO>> GetSuiteFavorite(string id)
    {
        // Recherche la suite favorite avec ses progressions d'accords associées
        var suiteFavorite = await _context
            .SuitesFavorites.Include(sf => sf.ProgressionAccords)
            .FirstOrDefaultAsync(sf => sf.Id == id.ToString());

        // Renvoie une erreur 404 si la suite n'est pas trouvée
        if (suiteFavorite == null)
        {
            return NotFound("Suite favorite non trouvée.");
        }

        // Convertit l'entité en DTO et la renvoie
        return new SuitesFavoritesDTO(suiteFavorite);
    }

    // Récupère les détails d'un utilisateur spécifique par son ID
    // Renvoie un objet UtilisateurDTO avec les informations de l'utilisateur
    [HttpGet("{id}")]
    public async Task<ActionResult<UtilisateurDTO>> GetUtilisateur(string id)
    {
        // Recherche l'utilisateur par son ID
        var utilisateur = await _userManager.FindByIdAsync(id);

        // Renvoie une erreur 404 si l'utilisateur n'est pas trouvé
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        // Convertit l'utilisateur en DTO et le renvoie
        return new UtilisateurDTO
        {
            Id = utilisateur.Id,
            UserName = utilisateur.UserName,
            Email = utilisateur.Email,
        };
    }

    // Supprime une suite favorite d'un utilisateur
    // Requiert une authentification et vérifie que l'utilisateur ne peut supprimer que ses propres favorites
    [HttpDelete("{userId}/favorites/{favoriteId}")]
    [Authorize] // Protéger cette route
    public async Task<IActionResult> DeleteSuiteFavorite(string userId, string favoriteId)
    {
        // Vérification de sécurité: l'utilisateur ne peut modifier que ses propres données
        if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value != userId)
        {
            return Forbid();
        }

        // Vérifie si l'utilisateur existe
        var utilisateur = await _userManager.FindByIdAsync(userId);
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        // Recherche la suite favorite spécifique à cet utilisateur
        var suiteFavorite = await _context.SuitesFavorites.FirstOrDefaultAsync(sf =>
            sf.Id == favoriteId && sf.UserId == userId
        );

        // Renvoie une erreur 404 si la suite n'est pas trouvée ou n'appartient pas à l'utilisateur
        if (suiteFavorite == null)
        {
            return NotFound("Suite favorite non trouvée ou n'appartient pas à cet utilisateur.");
        }

        // Supprime la suite favorite et enregistre les changements
        _context.SuitesFavorites.Remove(suiteFavorite);
        await _context.SaveChangesAsync();

        // Renvoie un code 204 (No Content) pour indiquer que l'opération a réussi
        return NoContent();
    }

    // Ajoute une suite favorite pour un utilisateur
    // Requiert une authentification et vérifie que l'utilisateur ne peut ajouter que ses propres favorites
    [HttpPost("{userId}/favorites")]
    [Authorize] // Protéger cette route
    public async Task<ActionResult<SuitesFavoritesDTO>> AddSuiteFavoriteByUser(
        string userId,
        [FromBody] ProgressionAccordDTO data
    )
    {
        // Vérification de sécurité: l'utilisateur ne peut modifier que ses propres données
        if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value != userId)
        {
            return Forbid();
        }

        // Vérifie si les données reçues sont valides
        if (data == null)
        {
            return BadRequest("Données invalides.");
        }

        // Vérifie si l'utilisateur existe
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        // Vérifie si la progression d'accords existe
        var progressionAccords = await _context
            .ProgressionAccords.Include(pa => pa.Accords)
            .FirstOrDefaultAsync(pa => pa.Id == data.Id);

        if (progressionAccords == null)
        {
            return NotFound("Suite d'accords non trouvée.");
        }

        // Vérifie si cette progression est déjà en favoris pour cet utilisateur
        bool dejaFavori = await _context.SuitesFavorites.AnyAsync(sf =>
            sf.UserId == userId && sf.ProgressionAccordsId == data.Id
        );

        // Renvoie une erreur 409 (Conflict) si déjà en favoris
        if (dejaFavori)
        {
            return Conflict("Cette suite d'accords est déjà dans vos favoris.");
        }

        // Crée une nouvelle entrée de suite favorite
        var suitesFavorites = new SuitesFavorites
        {
            UserId = userId,
            ProgressionAccordsId = data.Id,
            ProgressionAccords = progressionAccords,
        };

        // Ajoute la suite aux favoris et enregistre les changements
        _context.SuitesFavorites.Add(suitesFavorites);
        await _context.SaveChangesAsync();

        // Prépare la réponse avec la nouvelle suite favorite
        var suitesFavoritesDTO = new SuitesFavoritesDTO(suitesFavorites);
        
        // Renvoie un code 201 (Created) avec l'URL de la nouvelle ressource et son contenu
        return CreatedAtAction(
            nameof(GetSuiteFavorite),
            new { id = suitesFavorites.Id },
            suitesFavoritesDTO
        );
    }

    // Met à jour les informations d'un utilisateur
    // Requiert une authentification et vérifie que l'utilisateur ne peut modifier que son propre compte
    [HttpPut("{id}")]
    [Authorize] // Protéger cette route
    public async Task<IActionResult> PutUtilisateur(string id, UtilisateurDTO utilisateurDTO)
    {
        // Vérification de sécurité: l'utilisateur ne peut modifier que ses propres données
        if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value != id)
        {
            return Forbid();
        }

        // Vérifie que l'ID dans l'URL correspond à celui dans le corps de la requête
        if (id != utilisateurDTO.Id)
        {
            return BadRequest("L'ID de l'utilisateur ne correspond pas.");
        }

        // Recherche l'utilisateur par son ID
        var utilisateur = await _userManager.FindByIdAsync(id);

        // Renvoie une erreur 404 si l'utilisateur n'est pas trouvé
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        // Met à jour les propriétés de l'utilisateur
        utilisateur.UserName = utilisateurDTO.UserName;
        utilisateur.Email = utilisateurDTO.Email;
        // Note: cette ligne semble redondante, potentielle erreur de copier-coller
        utilisateur.UserName = utilisateurDTO.UserName; // Mettre à jour le UserName pour Identity

        // Enregistre les modifications via le UserManager
        var result = await _userManager.UpdateAsync(utilisateur);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        // Renvoie un code 204 (No Content) pour indiquer que l'opération a réussi
        return NoContent();
    }

    // Permet à un utilisateur de supprimer son propre compte
    // Requiert une authentification
    [HttpDelete("mon-compte")] //pour que l'utilisateur puisse supprimer son propre compte
    [Authorize] // Seuls les utilisateurs authentifiés peuvent accéder à cette route
    public async Task<IActionResult> DeleteMonCompte()
    {
        // Récupère l'ID de l'utilisateur authentifié depuis le token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
        {
            return BadRequest("ID utilisateur introuvable dans le token.");
        }

        var currentUserId = userIdClaim.Value;

        // Recherche l'utilisateur par son ID
        var utilisateur = await _userManager.FindByIdAsync(currentUserId);
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        // Supprime l'utilisateur via le UserManager
        var result = await _userManager.DeleteAsync(utilisateur);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        // Renvoie un code 204 (No Content) pour indiquer que l'opération a réussi
        return NoContent();
    }

    // Permet de supprimer un utilisateur par son ID
    // Note: L'autorisation a été temporairement supprimée pour les tests
    [HttpDelete("{id}")]
    // Enlevez temporairement [Authorize] pour tester
    public async Task<IActionResult> DeleteUtilisateur(string id)
    {
        // Recherche l'utilisateur par son ID
        var utilisateur = await _userManager.FindByIdAsync(id);
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        // Supprime l'utilisateur via le UserManager
        var result = await _userManager.DeleteAsync(utilisateur);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        // Renvoie un code 204 (No Content) pour indiquer que l'opération a réussi
        return NoContent();
    }
}