using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/progressions")]
public class ProgressionAccordsController : ControllerBase
{
    private readonly HarmoniaContext _context;

    public ProgressionAccordsController(HarmoniaContext context)
    {
        _context = context;
    }

    // GET: api/progressions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgressionAccordDTO>>> GetProgressions()
    {
        var progressions = await _context
            .ProgressionAccords.AsNoTracking()
            .Include(p => p.Accords)
            .Select(p => new ProgressionAccordDTO(p))
            .ToListAsync();

        return progressions;
    }

    // GET: api/progressions/filtred
    [HttpGet("filtred")]
    public async Task<ActionResult<IEnumerable<ProgressionAccordDTO>>> GetProgressions(
        [FromQuery] string? style = null,
        [FromQuery] string? tonalite = null,
        [FromQuery] string? mode = null
    )
    {
        // Commencer avec Include pour charger les accords
        var query = _context.ProgressionAccords.Include(p => p.Accords).AsQueryable();

        // Filtrer par Style (seulement si fourni)
        if (!string.IsNullOrEmpty(style))
        {
            // Normaliser pour ignorer la casse
            var styleNormalized = style.Trim();

            if (Enum.TryParse<Style>(styleNormalized, true, out var styleEnum))
            {
                query = query.Where(p => p.Style == styleEnum);
            }
            else
            {
                return BadRequest(
                    $"Style '{style}' invalide. Les valeurs possibles sont: {string.Join(", ", Enum.GetNames(typeof(Style)))}"
                );
            }
        }

        // Filtrer par Tonalite (seulement si fourni)
        if (!string.IsNullOrEmpty(tonalite))
        {
            // Gestion spéciale pour C#, D#, etc.
            var tonaliteNormalized = tonalite.Trim();

            // Mapper les valeurs spéciales
            if (tonaliteNormalized == "C#")
                tonaliteNormalized = "CSharp";
            else if (tonaliteNormalized == "D#")
                tonaliteNormalized = "DSharp";
            else if (tonaliteNormalized == "F#")
                tonaliteNormalized = "FSharp";
            else if (tonaliteNormalized == "G#")
                tonaliteNormalized = "GSharp";
            else if (tonaliteNormalized == "A#")
                tonaliteNormalized = "ASharp";

            if (Enum.TryParse<Tonalite>(tonaliteNormalized, true, out var tonaliteEnum))
            {
                query = query.Where(p => p.Tonalite == tonaliteEnum);
            }
            else
            {
                return BadRequest(
                    $"Tonalité '{tonalite}' invalide. Les valeurs possibles sont: C, C#, D, D#, E, F, F#, G, G#, A, A#, B"
                );
            }
        }

        // Filtrer par Mode (seulement si fourni)
        if (!string.IsNullOrEmpty(mode))
        {
            var modeNormalized = mode.Trim();

            if (Enum.TryParse<Mode>(modeNormalized, true, out var modeEnum))
            {
                query = query.Where(p => p.Mode == modeEnum);
            }
            else
            {
                return BadRequest(
                    $"Mode '{mode}' invalide. Les valeurs possibles sont: {string.Join(", ", Enum.GetNames(typeof(Mode)))}"
                );
            }
        }

        var progressions = await query.Select(p => new ProgressionAccordDTO(p)).ToListAsync();
        return progressions;
    }

    // GET: api/progressions/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProgressionAccordDTO>> GetProgression(string id)
    {
        var progression = await _context.ProgressionAccords.FirstOrDefaultAsync(p => p.Id == id);

        if (progression == null)
        {
            return NotFound("Progression non trouvée.");
        }

        return new ProgressionAccordDTO(progression);
    }

    // GET: api/progressions/{id}/accords
    [HttpGet("{id}/accords")]
    public async Task<ActionResult<IEnumerable<string>>> GetAccordsDeLaProgression(string id)
    {
        var progression = await _context
            .ProgressionAccords.AsNoTracking() // Ajouter cette ligne
            .Include(p => p.Accords)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (progression == null)
        {
            return NotFound("Progression non trouvée.");
        }

        var accordsNoms = progression.Accords.Select(a => a.Nom).ToList();
        return accordsNoms;
    }

    [HttpPost]
    public async Task<ActionResult<ProgressionAccordDTO>> PostProgression(
        [FromBody] ProgressionAccordCreateDTO progressionCreateDTO
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Créer une nouvelle progression
        var progression = new ProgressionAccords
        {
            Id = Guid.NewGuid().ToString(),
            Nom = progressionCreateDTO.Nom ?? string.Empty,
            Tonalite = progressionCreateDTO.Tonalite,
            Mode = progressionCreateDTO.Mode,
            Style = progressionCreateDTO.Style,
            Accords = new List<Accord>(),
        };

        // Important: Ne pas réutiliser le contexte pour la recherche et l'ajout
        // Attacher uniquement les références aux accords existants
        foreach (var accordId in progressionCreateDTO.Accords)
        {
            var accord = await _context.Accords.FindAsync(accordId);
            if (accord != null)
            {
                progression.Accords.Add(accord);
            }
        }

        _context.ProgressionAccords.Add(progression);
        await _context.SaveChangesAsync();

        // Détachez toutes les entités du contexte
        _context.ChangeTracker.Clear();

        // Rechargez la progression avec une requête fraîche
        var loadedProgression = await _context
            .ProgressionAccords.Include(p => p.Accords)
            .FirstOrDefaultAsync(p => p.Id == progression.Id);

        // Vérifier si loadedProgression n'est pas null avant de créer le DTO
        if (loadedProgression == null)
        {
            return NotFound("Created progression could not be loaded");
        }

        return CreatedAtAction(
            nameof(GetProgression),
            new { id = progression.Id },
            new ProgressionAccordDTO(loadedProgression)
        );
    }

    // PUT: api/progressions/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProgression(
        string id,
        ProgressionAccordCreateDTO progressionDTO
    )
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("L'ID ne peut pas être vide.");
        }

        var progression = await _context
            .ProgressionAccords.Include(p => p.Accords)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (progression == null)
        {
            return NotFound("Progression non trouvée.");
        }

        // Mettre à jour les propriétés
        progression.Nom = progressionDTO.Nom ?? string.Empty;
        progression.Mode = progressionDTO.Mode;
        progression.Style = progressionDTO.Style;
        progression.Tonalite = progressionDTO.Tonalite;

        // Mettre à jour les accords
        progression.Accords.Clear(); // Vider la collection actuelle

        foreach (var accordId in progressionDTO.Accords)
        {
            var accord = await _context.Accords.FindAsync(accordId);
            if (accord == null)
            {
                return NotFound($"Accord avec l'ID {accordId} non trouvé.");
            }
            progression.Accords.Add(accord);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ProgressionAccords.Any(p => p.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/progressions/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProgression(string id)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // 1. Supprimer d'abord les entrées dans SuitesFavorites qui référencent cette progression
                var suitesFavorites = await _context
                    .SuitesFavorites.Where(sf => sf.ProgressionAccordsId == id)
                    .ToListAsync();

                _context.SuitesFavorites.RemoveRange(suitesFavorites);
                await _context.SaveChangesAsync();

                // 2. Récupérer la progression avec ses accords
                var progression = await _context
                    .ProgressionAccords.Include(p => p.Accords)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (progression == null)
                {
                    return NotFound("Progression non trouvée.");
                }

                // 3. Dissocier les accords de la progression sans les supprimer
                progression.Accords.Clear();
                await _context.SaveChangesAsync(); // Sauvegarder cette modification

                // 4. Supprimer la progression elle-même
                _context.ProgressionAccords.Remove(progression);
                await _context.SaveChangesAsync();

                // 5. Valider la transaction
                await transaction.CommitAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Annuler la transaction en cas d'erreur
                await transaction.RollbackAsync();

                // Ajouter l'exception interne pour le débogage
                string innerMessage =
                    ex.InnerException != null
                        ? $" - Inner exception: {ex.InnerException.Message}"
                        : "";
                return StatusCode(
                    500,
                    $"Erreur lors de la suppression: {ex.Message}{innerMessage}"
                );
            }
        }
    }
}
