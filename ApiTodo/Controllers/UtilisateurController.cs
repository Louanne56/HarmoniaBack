using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/utilisateurs")]
public class UtilisateursController : ControllerBase
{
    private readonly HarmoniaContext _context;
    private readonly UserManager<Utilisateur> _userManager;

    public UtilisateursController(HarmoniaContext context, UserManager<Utilisateur> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: api/utilisateurs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UtilisateurDTO>>> GetUtilisateurs()
    {
        var utilisateurs = await _userManager
            .Users.Select(u => new UtilisateurDTO
            {
                Id = u.Id,
                Pseudo = u.Pseudo,
                Email = u.Email,
            })
            .ToListAsync();

        return utilisateurs;
    }

    // GET: api/utilisateurs/suitesFavorites/{id}
    [HttpGet("suitesFavorites/{id}")]
    public async Task<ActionResult<SuitesFavoritesDTO>> GetSuiteFavorite(string id)
    {
        var suiteFavorite = await _context
            .SuitesFavorites.Include(sf => sf.ProgressionAccords)
            .FirstOrDefaultAsync(sf => sf.Id == id.ToString());

        if (suiteFavorite == null)
        {
            return NotFound("Suite favorite non trouvée.");
        }

        return new SuitesFavoritesDTO(suiteFavorite);
    }

    // GET: api/utilisateurs/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UtilisateurDTO>> GetUtilisateur(string id)
    {
        var utilisateur = await _userManager.FindByIdAsync(id);

        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        return new UtilisateurDTO
        {
            Id = utilisateur.Id,
            Pseudo = utilisateur.Pseudo,
            Email = utilisateur.Email,
        };
    }

    // DELETE: api/utilisateurs/{userId}/favorites/{favoriteId}
    [HttpDelete("{userId}/favorites/{favoriteId}")]
    [Authorize] // Protéger cette route
    public async Task<IActionResult> DeleteSuiteFavorite(string userId, string favoriteId)
    {
        // Vérifier si l'utilisateur est authentifié et correspond à l'ID
        if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value != userId)
        {
            return Forbid();
        }

        // Vérifier si l'utilisateur existe
        var utilisateur = await _userManager.FindByIdAsync(userId);
        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        // Rechercher la suite favorite
        var suiteFavorite = await _context.SuitesFavorites.FirstOrDefaultAsync(sf =>
            sf.Id == favoriteId && sf.UserId == userId
        );

        if (suiteFavorite == null)
        {
            return NotFound("Suite favorite non trouvée ou n'appartient pas à cet utilisateur.");
        }

        // Supprimer la suite favorite
        _context.SuitesFavorites.Remove(suiteFavorite);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{userId}/favorites")]
    [Authorize] // Protéger cette route
    public async Task<ActionResult<SuitesFavoritesDTO>> AddSuiteFavoriteByUser(
        string userId,
        [FromBody] ProgressionAccordDTO data
    )
    {
        // Vérifier si l'utilisateur est authentifié et correspond à l'ID
        if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value != userId)
        {
            return Forbid();
        }

        if (data == null)
        {
            return BadRequest("Données invalides.");
        }

        // Vérifier si l'utilisateur existe
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        // Vérifier si la suite d'accords existe
        var progressionAccords = await _context
            .ProgressionAccords.Include(pa => pa.Accords)
            .FirstOrDefaultAsync(pa => pa.Id == data.Id);

        if (progressionAccords == null)
        {
            return NotFound("Suite d'accords non trouvée.");
        }

        // Vérifier si déjà en favoris
        bool dejaFavori = await _context.SuitesFavorites.AnyAsync(sf =>
            sf.UserId == userId && sf.ProgressionAccordsId == data.Id
        );

        if (dejaFavori)
        {
            return Conflict("Cette suite d'accords est déjà dans vos favoris.");
        }

        // Créer la nouvelle entrée
        var suitesFavorites = new SuitesFavorites
        {
            UserId = userId,
            ProgressionAccordsId = data.Id,
            ProgressionAccords = progressionAccords,
        };

        _context.SuitesFavorites.Add(suitesFavorites);
        await _context.SaveChangesAsync();

        // Renvoyer la réponse
        var suitesFavoritesDTO = new SuitesFavoritesDTO(suitesFavorites);
        return CreatedAtAction(
            nameof(GetSuiteFavorite),
            new { id = suitesFavorites.Id },
            suitesFavoritesDTO
        );
    }

    // PUT: api/utilisateurs/{id}
    [HttpPut("{id}")]
    [Authorize] // Protéger cette route
    public async Task<IActionResult> PutUtilisateur(string id, UtilisateurDTO utilisateurDTO)
    {
        // Vérifier si l'utilisateur est authentifié et correspond à l'ID
        if (User.FindFirst(ClaimTypes.NameIdentifier)?.Value != id)
        {
            return Forbid();
        }

        if (id != utilisateurDTO.Id)
        {
            return BadRequest("L'ID de l'utilisateur ne correspond pas.");
        }

        var utilisateur = await _userManager.FindByIdAsync(id);

        if (utilisateur == null)
        {
            return NotFound("Utilisateur non trouvé.");
        }

        utilisateur.Pseudo = utilisateurDTO.Pseudo;
        utilisateur.Email = utilisateurDTO.Email;
        utilisateur.UserName = utilisateurDTO.Pseudo; // Mettre à jour le UserName pour Identity

        var result = await _userManager.UpdateAsync(utilisateur);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

  // DELETE: api/utilisateurs/mon-compte
[HttpDelete("mon-compte")] //pour que l'utilisateur puisse supp son propre
[Authorize] // Seuls les utilisateurs authentifiés peuvent accéder à cette route
public async Task<IActionResult> DeleteMonCompte()
{
    var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    var utilisateur = await _userManager.FindByIdAsync(currentUserId);
    if (utilisateur == null)
    {
        return NotFound("Utilisateur non trouvé.");
    }

    var result = await _userManager.DeleteAsync(utilisateur);
    if (!result.Succeeded)
    {
        return BadRequest(result.Errors);
    }

    return NoContent();
}

[HttpDelete("{id}")]
// Enlevez temporairement [Authorize] pour tester
public async Task<IActionResult> DeleteUtilisateur(string id)
{
    var utilisateur = await _userManager.FindByIdAsync(id);
    if (utilisateur == null)
    {
        return NotFound("Utilisateur non trouvé.");
    }

    var result = await _userManager.DeleteAsync(utilisateur);
    if (!result.Succeeded)
    {
        return BadRequest(result.Errors);
    }

    return NoContent();
}
}
