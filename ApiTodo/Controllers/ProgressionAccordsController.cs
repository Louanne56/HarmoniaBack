using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Contrôleur qui gère les opérations CRUD sur les progressions d'accords
[ApiController]
[Route("api/progressions")]
public class ProgressionAccordsController : ControllerBase
{
    // Contexte de base de données pour accéder aux entités
    private readonly HarmoniaContext _context;

    // Constructeur qui initialise le contexte par injection de dépendance
    public ProgressionAccordsController(HarmoniaContext context)
    {
        _context = context;
    }

    // Récupère toutes les progressions d'accords avec leurs accords associés
    // Renvoie une collection de ProgressionAccordDTO
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgressionAccordDTO>>> GetProgressions()
    {
        // Utilise AsNoTracking pour optimiser les performances en lecture seule
        // Include pour charger la relation avec les accords en une seule requête
        var progressions = await _context
            .ProgressionAccords.AsNoTracking()
            .Include(p => p.Accords)
            .Select(p => new ProgressionAccordDTO(p))
            .ToListAsync();

        return progressions;
    }

    // Récupère les progressions d'accords avec filtrage par style, tonalité et mode
    // Les paramètres sont optionnels et permettent un filtrage flexible
    [HttpGet("filtred")]
    public async Task<ActionResult<IEnumerable<ProgressionAccordDTO>>> GetProgressions(
        [FromQuery] string? style = null,
        [FromQuery] string? tonalite = null,
        [FromQuery] string? mode = null
    )
    {
        // Commence avec une requête de base incluant les accords
        var query = _context.ProgressionAccords.Include(p => p.Accords).AsQueryable();

        // Applique le filtre par style si fourni
        if (!string.IsNullOrEmpty(style))
        {
            // Normalise la valeur pour éviter les problèmes de casse
            var styleNormalized = style.Trim();

            // Tente de convertir la chaîne en valeur d'énumération Style
            if (Enum.TryParse<Style>(styleNormalized, true, out var styleEnum))
            {
                query = query.Where(p => p.Style == styleEnum);
            }
            else
            {
                // Renvoie une erreur si le style n'est pas valide
                return BadRequest(
                    $"Style '{style}' invalide. Les valeurs possibles sont: {string.Join(", ", Enum.GetNames(typeof(Style)))}"
                );
            }
        }

        // Applique le filtre par tonalité si fournie
        if (!string.IsNullOrEmpty(tonalite))
        {
            // Gestion spéciale pour les notes avec dièse (#) qui ne peuvent pas être utilisées directement dans l'énumération
            var tonaliteNormalized = tonalite.Trim();

            // Mappe les notations musicales standard vers les noms d'énumération
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

            // Tente de convertir la chaîne en valeur d'énumération Tonalite
            if (Enum.TryParse<Tonalite>(tonaliteNormalized, true, out var tonaliteEnum))
            {
                query = query.Where(p => p.Tonalite == tonaliteEnum);
            }
            else
            {
                // Renvoie une erreur si la tonalité n'est pas valide
                return BadRequest(
                    $"Tonalité '{tonalite}' invalide. Les valeurs possibles sont: C, C#, D, D#, E, F, F#, G, G#, A, A#, B"
                );
            }
        }

        // Applique le filtre par mode si fourni
        if (!string.IsNullOrEmpty(mode))
        {
            var modeNormalized = mode.Trim();

            // Tente de convertir la chaîne en valeur d'énumération Mode
            if (Enum.TryParse<Mode>(modeNormalized, true, out var modeEnum))
            {
                query = query.Where(p => p.Mode == modeEnum);
            }
            else
            {
                // Renvoie une erreur si le mode n'est pas valide
                return BadRequest(
                    $"Mode '{mode}' invalide. Les valeurs possibles sont: {string.Join(", ", Enum.GetNames(typeof(Mode)))}"
                );
            }
        }

        // Exécute la requête avec tous les filtres appliqués et convertit en DTOs
        var progressions = await query.Select(p => new ProgressionAccordDTO(p)).ToListAsync();
        return progressions;
    }

    // Récupère une progression d'accords spécifique par son ID
    // Renvoie un ProgressionAccordDTO ou une erreur 404 si non trouvée
    [HttpGet("{id}")]
    public async Task<ActionResult<ProgressionAccordDTO>> GetProgression(string id)
    {
        // Recherche la progression par son ID
        var progression = await _context.ProgressionAccords.FirstOrDefaultAsync(p => p.Id == id);

        // Vérifie si la progression existe
        if (progression == null)
        {
            return NotFound("Progression non trouvée.");
        }

        // Convertit et renvoie le DTO
        return new ProgressionAccordDTO(progression);
    }

    // Récupère la liste des noms d'accords d'une progression spécifique
    // Utile pour afficher rapidement les accords sans toutes les détails
    [HttpGet("{id}/accords")]
    public async Task<ActionResult<IEnumerable<string>>> GetAccordsDeLaProgression(string id)
    {
        // Recherche la progression avec ses accords associés
        var progression = await _context
            .ProgressionAccords.AsNoTracking() // Optimisation pour la lecture seule
            .Include(p => p.Accords)
            .FirstOrDefaultAsync(p => p.Id == id);

        // Vérifie si la progression existe
        if (progression == null)
        {
            return NotFound("Progression non trouvée.");
        }

        // Extrait uniquement les noms des accords
        var accordsNoms = progression.Accords.Select(a => a.Nom).ToList();
        return accordsNoms;
    }

    // Crée une nouvelle progression d'accords
    // Renvoie la progression créée avec un code 201 (Created)
    [HttpPost]
    public async Task<ActionResult<ProgressionAccordDTO>> PostProgression(
        [FromBody] ProgressionAccordCreateDTO progressionCreateDTO
    )
    {
        // Vérifie si le modèle reçu est valide
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Crée une nouvelle entité de progression avec un ID généré
        var progression = new ProgressionAccords
        {
            Id = Guid.NewGuid().ToString(),
            Nom = progressionCreateDTO.Nom ?? string.Empty,
            Tonalite = progressionCreateDTO.Tonalite,
            Mode = progressionCreateDTO.Mode,
            Style = progressionCreateDTO.Style,
            Accords = new List<Accord>(), // Initialise la collection d'accords
        };

        // Ajoute les références aux accords existants
        foreach (var accordId in progressionCreateDTO.Accords)
        {
            var accord = await _context.Accords.FindAsync(accordId);
            if (accord != null)
            {
                progression.Accords.Add(accord);
            }
        }

        // Ajoute la progression au contexte et persiste en base de données
        _context.ProgressionAccords.Add(progression);
        await _context.SaveChangesAsync();

        // Nettoie le tracking des entités pour éviter les conflits
        _context.ChangeTracker.Clear();

        // Recharge la progression avec toutes ses relations pour construire le DTO complet
        var loadedProgression = await _context
            .ProgressionAccords.Include(p => p.Accords)
            .FirstOrDefaultAsync(p => p.Id == progression.Id);

        // Vérifie que la progression a bien été chargée
        if (loadedProgression == null)
        {
            return NotFound("Created progression could not be loaded");
        }

        // Renvoie le DTO avec l'URI de la nouvelle ressource
        return CreatedAtAction(
            nameof(GetProgression),
            new { id = progression.Id },
            new ProgressionAccordDTO(loadedProgression)
        );
    }

    // Met à jour une progression d'accords existante
    // Renvoie un code 204 (No Content) en cas de réussite
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProgression(
        string id,
        ProgressionAccordCreateDTO progressionDTO
    )
    {
        // Vérifie si l'ID est valide
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("L'ID ne peut pas être vide.");
        }

        // Recherche la progression avec ses accords associés
        var progression = await _context
            .ProgressionAccords.Include(p => p.Accords)
            .FirstOrDefaultAsync(p => p.Id == id);

        // Vérifie si la progression existe
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
