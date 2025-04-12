using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/accords")]
public class AccordsController : ControllerBase
{
    private readonly HarmoniaContext _context;

    public AccordsController(HarmoniaContext context)
    {
        _context = context;
    }

    // GET: api/accords
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccordDTO>>> GetAccords()
    {
        var accords = await _context.Accords.Select(a => new AccordDTO(a)).ToListAsync();
        return accords;
    }

    // GET: api/accords/5
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
    [HttpPost]
    public async Task<ActionResult<AccordDTO>> CreateAccord(
        [FromBody] AccordCreateDTO accordCreateDTO
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Validation manuelle des données
        if (string.IsNullOrWhiteSpace(accordCreateDTO.Nom))
        {
            return BadRequest("Le nom de l'accord est requis.");
        }

        if (string.IsNullOrWhiteSpace(accordCreateDTO.Diagram1))
        {
            return BadRequest("La position 1 est requise.");
        }

        // Mapper AccordCreateDTO vers Accord
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

        // Renvoyer la réponse avec le DTO complet
        var accordDTO = new AccordDTO(accord);
        return CreatedAtAction(nameof(GetAccord), new { id = accord.Id }, accordDTO);
    }

    // PUT: api/accords/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAccord(string id, [FromBody] AccordDTO accordDTO)
    {
        if (id != accordDTO.Id)
        {
            return BadRequest("L'ID de l'accord ne correspond pas.");
        }

        // Validation des données
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

        // Mapper les propriétés
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
