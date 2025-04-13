using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Contrôleur pour gérer les suites favorites des utilisateurs
[ApiController]
[Route("api/suites-favorites")]
public class SuitesFavoritesController : ControllerBase
{
    // Contexte de base de données pour accéder aux entités
    private readonly HarmoniaContext _context;

    // Constructeur qui initialise le contexte par injection de dépendance
    public SuitesFavoritesController(HarmoniaContext context)
    {
        _context = context;
    }

    // Récupère toutes les suites favorites avec leurs progressions et accords associés
    // Renvoie une collection de SuitesFavoritesDTO
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SuitesFavoritesDTO>>> GetAllSuitesFavorites()
    {
        // Utilise AsNoTracking pour optimiser la performance en lecture seule
        // Include et ThenInclude pour charger les relations en une seule requête (évite les requêtes N+1)
        var suitesFavorites = await _context
            .SuitesFavorites.AsNoTracking() // Optimisation : lecture seule
            .Include(sf => sf.ProgressionAccords) // Charge la progression associée
            .ThenInclude(p => p!.Accords) // Puis les accords de la progression
            .Select(sf => new SuitesFavoritesDTO(sf)) // Convertit chaque entité en DTO
            .ToListAsync();

        return suitesFavorites;
    }

    // Récupère toutes les suites favorites d'un utilisateur spécifique
    // Renvoie une collection de SuitesFavoritesDTO filtrée par userId
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<SuitesFavoritesDTO>>> GetSuitesFavoritesByUser(
        string userId
    )
    {
        // Requête similaire à GetAllSuitesFavorites mais avec un filtre sur l'ID utilisateur
        var suitesFavorites = await _context
            .SuitesFavorites.AsNoTracking()
            .Where(sf => sf.UserId == userId) // Filtre par utilisateur
            .Include(sf => sf.ProgressionAccords)
            .ThenInclude(p => p!.Accords)
            .Select(sf => new SuitesFavoritesDTO(sf))
            .ToListAsync();

        return suitesFavorites;
    }

    // Récupère une suite favorite spécifique par son ID
    // Renvoie un SuitesFavoritesDTO ou une erreur 404 si non trouvée
    [HttpGet("{id}")]
    public async Task<ActionResult<SuitesFavoritesDTO>> GetSuiteFavorite(string id)
    {
        // Recherche une suite favorite spécifique avec ses relations
        var suiteFavorite = await _context
            .SuitesFavorites.AsNoTracking()
            .Include(sf => sf.ProgressionAccords)
            .ThenInclude(p => p!.Accords)
            .FirstOrDefaultAsync(sf => sf.Id == id);

        // Vérifie si la suite favorite existe
        if (suiteFavorite == null)
        {
            return NotFound("Suite favorite non trouvée.");
        }

        // Convertit et renvoie le DTO
        return new SuitesFavoritesDTO(suiteFavorite);
    }

    // Ajoute une nouvelle suite favorite
    // Renvoie le nouveau SuitesFavoritesDTO créé avec un code 201 (Created)
    [HttpPost]
    public async Task<ActionResult<SuitesFavoritesDTO>> AddSuiteFavorite(
        [FromBody] SuitesFavoritesCreateDTO dto
    )
    {
        // Crée une nouvelle entité SuitesFavorites avec un ID généré
        var suiteFavorite = new SuitesFavorites
        {
            Id = Guid.NewGuid().ToString(), // Génération d'un nouvel ID unique
            UserId = dto.UserId,
            ProgressionAccordsId = dto.ProgressionAccordsId
        };

        // Ajoute l'entité au contexte et persiste en base de données
        _context.SuitesFavorites.Add(suiteFavorite);
        await _context.SaveChangesAsync();

        // Nettoie le contexte pour éviter les problèmes de tracking 
        // (important pour la réutilisation du contexte)
        _context.ChangeTracker.Clear();

        // Recharge l'entité avec toutes ses relations pour construire le DTO complet
        var loadedSuiteFavorite = await _context
            .SuitesFavorites.Include(sf => sf.ProgressionAccords)
            .ThenInclude(p => p!.Accords)
            .FirstOrDefaultAsync(sf => sf.Id == suiteFavorite.Id);

        // Vérifie que la suite favorite a bien été chargée
        if (loadedSuiteFavorite == null)
        {
            return NotFound("La suite favorite créée n'a pas pu être chargée");
        }

        // Retourne le DTO avec l'URI de la nouvelle ressource
        return CreatedAtAction(
            nameof(GetSuiteFavorite),
            new { id = suiteFavorite.Id },
            new SuitesFavoritesDTO(loadedSuiteFavorite)
        );
    }

    // Supprime une suite favorite par son ID
    // Renvoie un code 204 (No Content) en cas de réussite ou 404 si non trouvée
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSuiteFavorite(string id)
    {
        // Recherche la suite favorite par son ID
        var suiteFavorite = await _context.SuitesFavorites.FindAsync(id);
        if (suiteFavorite == null)
        {
            return NotFound("Suite favorite non trouvée.");
        }

        // Supprime la suite favorite et persiste le changement
        _context.SuitesFavorites.Remove(suiteFavorite);
        await _context.SaveChangesAsync();

        // Renvoie un code 204 (No Content) pour indiquer que l'opération a réussi
        return NoContent();
    }

    // Supprime une suite favorite pour un utilisateur et une progression spécifiques
    // Utile pour retirer une progression des favoris sans connaître l'ID exact de la relation
    [HttpDelete("user/{userId}/progression/{progressionId}")]
    public async Task<IActionResult> DeleteSuiteFavoriteByUserAndProgression(
        string userId,
        string progressionId
    )
    {
        // Recherche la suite favorite par l'ID utilisateur et l'ID de progression
        var suiteFavorite = await _context.SuitesFavorites.FirstOrDefaultAsync(sf =>
            sf.UserId == userId && sf.ProgressionAccordsId == progressionId
        );

        // Vérifie si la suite favorite existe
        if (suiteFavorite == null)
        {
            return NotFound(
                "Suite favorite non trouvée pour cet utilisateur et cette progression."
            );
        }

        // Supprime la suite favorite et persiste le changement
        _context.SuitesFavorites.Remove(suiteFavorite);
        await _context.SaveChangesAsync();

        // Renvoie un code 204 (No Content) pour indiquer que l'opération a réussi
        return NoContent();
    }

    // Vérifie si une progression est en favoris pour un utilisateur spécifique
    // Renvoie true/false en fonction de l'existence de la relation
    [HttpGet("check-favorite")]
    public async Task<ActionResult<bool>> CheckIsFavorite(
        [FromQuery] string userId,
        [FromQuery] string progressionId
    )
    {
        // Utilise la méthode Any pour vérifier l'existence sans charger l'entité complète
        var exists = await _context.SuitesFavorites.AnyAsync(sf =>
            sf.UserId == userId && sf.ProgressionAccordsId == progressionId
        );

        return exists; // Renvoie true si la relation existe, false sinon
    }
}