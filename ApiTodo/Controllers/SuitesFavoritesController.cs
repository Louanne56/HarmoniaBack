using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/suites-favorites")]
public class SuitesFavoritesController : ControllerBase
{
    private readonly HarmoniaContext _context;

    public SuitesFavoritesController(HarmoniaContext context)
    {
        _context = context;
    }

    // GET: api/suites-favorites
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SuitesFavoritesDTO>>> GetAllSuitesFavorites()
    {
        var suitesFavorites = await _context
            .SuitesFavorites.AsNoTracking()
            .Include(sf => sf.ProgressionAccords)
            .ThenInclude(p => p.Accords)
            .Select(sf => new SuitesFavoritesDTO(sf))
            .ToListAsync();

        return suitesFavorites;
    }

    // GET: api/suites-favorites/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<SuitesFavoritesDTO>>> GetSuitesFavoritesByUser(
        string userId
    )
    {
        var suitesFavorites = await _context
            .SuitesFavorites.AsNoTracking()
            .Where(sf => sf.UserId == userId)
            .Include(sf => sf.ProgressionAccords)
            .ThenInclude(p => p.Accords)
            .Select(sf => new SuitesFavoritesDTO(sf))
            .ToListAsync();

        return suitesFavorites;
    }

    // GET: api/suites-favorites/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SuitesFavoritesDTO>> GetSuiteFavorite(string id)
    {
        var suiteFavorite = await _context
            .SuitesFavorites.AsNoTracking()
            .Include(sf => sf.ProgressionAccords)
            .ThenInclude(p => p.Accords)
            .FirstOrDefaultAsync(sf => sf.Id == id);

        if (suiteFavorite == null)
        {
            return NotFound("Suite favorite non trouvée.");
        }

        return new SuitesFavoritesDTO(suiteFavorite);
    }

    // POST: api/suites-favorites
    [HttpPost]
    public async Task<ActionResult<SuitesFavoritesDTO>> AddSuiteFavorite(
        [FromBody] SuitesFavoritesCreateDTO dto
    )
    {
        // Vérifications standard...

        var suiteFavorite = new SuitesFavorites
        {
            Id = Guid.NewGuid().ToString(),
            UserId = dto.UserId,
            ProgressionAccordsId = dto.ProgressionAccordsId,
            // Ne pas initialiser ProgressionAccords ici
        };

        _context.SuitesFavorites.Add(suiteFavorite);
        await _context.SaveChangesAsync();

        // Nettoyer le contexte
        _context.ChangeTracker.Clear();

        // Recharger avec une requête explicite
        var loadedSuiteFavorite = await _context
            .SuitesFavorites.Include(sf => sf.ProgressionAccords)
            .ThenInclude(p => p.Accords)
            .FirstOrDefaultAsync(sf => sf.Id == suiteFavorite.Id);

        return CreatedAtAction(
            nameof(GetSuiteFavorite),
            new { id = suiteFavorite.Id },
            new SuitesFavoritesDTO(loadedSuiteFavorite)
        );
    }

    // DELETE: api/suites-favorites/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSuiteFavorite(string id)
    {
        var suiteFavorite = await _context.SuitesFavorites.FindAsync(id);
        if (suiteFavorite == null)
        {
            return NotFound("Suite favorite non trouvée.");
        }

        _context.SuitesFavorites.Remove(suiteFavorite);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/suites-favorites/user/{userId}/progression/{progressionId}
    [HttpDelete("user/{userId}/progression/{progressionId}")]
    public async Task<IActionResult> DeleteSuiteFavoriteByUserAndProgression(
        string userId,
        string progressionId
    )
    {
        var suiteFavorite = await _context.SuitesFavorites.FirstOrDefaultAsync(sf =>
            sf.UserId == userId && sf.ProgressionAccordsId == progressionId
        );

        if (suiteFavorite == null)
        {
            return NotFound(
                "Suite favorite non trouvée pour cet utilisateur et cette progression."
            );
        }

        _context.SuitesFavorites.Remove(suiteFavorite);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/suites-favorites/check-favorite?userId={userId}&progressionId={progressionId}
    [HttpGet("check-favorite")]
    public async Task<ActionResult<bool>> CheckIsFavorite(
        [FromQuery] string userId,
        [FromQuery] string progressionId
    )
    {
        var exists = await _context.SuitesFavorites.AnyAsync(sf =>
            sf.UserId == userId && sf.ProgressionAccordsId == progressionId
        );

        return exists;
    }
}
