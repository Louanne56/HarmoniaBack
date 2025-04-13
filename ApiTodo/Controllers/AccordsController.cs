using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Contrôleur pour gérer les accords (CRUD complet)
[ApiController]
[Route("api/accords")]
public class AccordsController : ControllerBase
{
    private readonly HarmoniaContext _context;

    // Initialise le contrôleur avec le contexte de base de données
    public AccordsController(HarmoniaContext context)
    {
        _context = context;
    }

    // GET: api/accords
    // Récupère tous les accords
    // Retourne : Liste d'AccordDTO (200 OK)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccordDTO>>> GetAccords()
    {
        var accords = await _context.Accords.Select(a => new AccordDTO(a)).ToListAsync();
        return accords;
    }

    // GET: api/accords/5
    // Récupère un accord par son ID
    // Retourne : AccordDTO (200 OK) ou 404 si non trouvé
    [HttpGet("{id}")]
    public async Task<ActionResult<AccordDTO>> GetAccord(string id)
    {
        var accord = await _context.Accords.FirstOrDefaultAsync(a => a.Id == id);

        if (accord == null)
        {
            return NotFound("Accord non trouvé.");
        }

        return new AccordDTO(accord);
    }

    // POST: api/accords
    // Crée un nouvel accord
    // Retourne : 
    // - 201 Created + AccordDTO si succès
    // - 400 si données invalides
    [HttpPost]
    public async Task<ActionResult<AccordDTO>> CreateAccord(
        [FromBody] AccordCreateDTO accordCreateDTO
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        /* Validation manuelle :
         * - Nom obligatoire
         * - Diagram1 obligatoire
         */
        if (string.IsNullOrWhiteSpace(accordCreateDTO.Nom))
        {
            return BadRequest("Le nom de l'accord est requis.");
        }

        if (string.IsNullOrWhiteSpace(accordCreateDTO.Diagram1))
        {
            return BadRequest("La position 1 est requise.");
        }

        // Mapping DTO -> Entité Accord
        var accord = new Accord
        {
            Nom = accordCreateDTO.Nom,
            Diagram1 = accordCreateDTO.Diagram1,
            Diagram2 = accordCreateDTO.Diagram2,
            Audio = accordCreateDTO.Audio,
            Audio2 = accordCreateDTO.Audio2,
        };

        _context.Accords.Add(accord);
        await _context.SaveChangesAsync();

        // Retourne l'URI de la nouvelle ressource
        var accordDTO = new AccordDTO(accord);
        return CreatedAtAction(nameof(GetAccord), new { id = accord.Id }, accordDTO);
    }

    // PUT: api/accords/5
    // Met à jour un accord existant
    // Retourne :
    // - 204 NoContent si succès
    // - 400 si ID non concordant ou données invalides
    // - 404 si accord non trouvé
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccord(string id, [FromBody] AccordDTO accordDTO)
    {
        if (id != accordDTO.Id)
        {
            return BadRequest("L'ID de l'accord ne correspond pas.");
        }

        // Validation des données (comme pour la création)
        if (string.IsNullOrWhiteSpace(accordDTO.Nom))
        {
            return BadRequest("Le nom de l'accord est requis.");
        }

        if (string.IsNullOrWhiteSpace(accordDTO.Diagram1))
        {
            return BadRequest("La position 1 est requise.");
        }

        var accord = await _context.Accords.FindAsync(id);

        if (accord == null)
        {
            return NotFound("Accord non trouvé.");
        }

        // Mise à jour des propriétés
        accord.Nom = accordDTO.Nom;
        accord.Diagram1 = accordDTO.Diagram1;
        accord.Diagram2 = accordDTO.Diagram2;
        accord.Audio = accordDTO.Audio;
        accord.Audio2 = accordDTO.Audio2;

        _context.Entry(accord).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Accords.Any(a => a.Id == id))
            {
                return NotFound("Accord non trouvé.");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/accords/5
    // Supprime un accord spécifique
    // Retourne :
    // - 204 NoContent si succès
    // - 404 si non trouvé
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccord(string id)
    {
        var accord = await _context.Accords.FindAsync(id);

        if (accord == null)
        {
            return NotFound("Accord non trouvé.");
        }

        _context.Accords.Remove(accord);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/accords/all
    // Supprime TOUS les accords (opération dangereuse)
    // Retourne :
    // - 204 NoContent si succès
    // - 404 si aucun accord existant
    [HttpDelete("all")]
    public async Task<IActionResult> DeleteAllAccords()
    {
        var accords = await _context.Accords.ToListAsync();

        if (accords == null || accords.Count == 0)
        {
            return NotFound("Aucun accord trouvé.");
        }

        _context.Accords.RemoveRange(accords);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}