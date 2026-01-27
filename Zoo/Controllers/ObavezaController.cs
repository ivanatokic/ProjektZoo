using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObavezaController : ControllerBase
{
    private readonly ZooContext _context;

    public ObavezaController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Obaveza>>> GetSveObaveze()
    {
        return await _context.Obaveza
            .Include(o => o.Radnik)
            .Include(o => o.Jedinka).ThenInclude(j => j!.Vrsta)
            .Include(o => o.Skupina).ThenInclude(s => s!.Vrsta)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Obaveza>> GetObaveza(int id)
    {
        var obaveza = await _context.Obaveza
            .Include(o => o.Radnik)
            .Include(o => o.Jedinka).ThenInclude(j => j!.Vrsta)
            .Include(o => o.Skupina).ThenInclude(s => s!.Vrsta)
            .FirstOrDefaultAsync(o => o.ID_obaveze == id);

        if (obaveza == null) return NotFound();
        return obaveza;
    }
    // GET: api/Obaveza/PoRadniku/5
    [HttpGet("PoRadniku/{idRadnika}")]
    public async Task<ActionResult<IEnumerable<Obaveza>>> GetObavezePoRadniku(int idRadnika)
    {
        var obaveze = await _context.Obaveza
            .Include(o => o.Radnik)
            .Include(o => o.Jedinka).ThenInclude(j => j!.Vrsta)
            .Include(o => o.Skupina).ThenInclude(s => s!.Vrsta)
            .Where(o => o.ID_radnika == idRadnika)
            .OrderBy(o => o.datum)
            .ToListAsync();

        if (!obaveze.Any())
            return NotFound();

        return Ok(obaveze);
    }


    [HttpPost]
    public async Task<ActionResult<Obaveza>> PostObaveza(Obaveza obaveza)
    {
        NormalizeObavezaFk(obaveza);

        // validacija: ako je zadani radnik + datum, provjeri konflikt
        if (obaveza.ID_radnika.HasValue && obaveza.datum.HasValue)
        {
            bool konflikt = await _context.Obaveza.AnyAsync(o =>
                o.ID_radnika == obaveza.ID_radnika &&
                o.datum == obaveza.datum);

            if (konflikt)
            {
                return BadRequest("Radnik već ima obavezu u tom terminu.");
            }
        }

        _context.Obaveza.Add(obaveza);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetObaveza),
            new { id = obaveza.ID_obaveze }, obaveza);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutObaveza(int id, Obaveza obaveza)
    {
        if (id != obaveza.ID_obaveze)
            return BadRequest();

        NormalizeObavezaFk(obaveza);

        // validacija: radnik ne smije imati drugu obavezu u isti termin
        if (obaveza.ID_radnika.HasValue && obaveza.datum.HasValue)
        {
            bool konflikt = await _context.Obaveza.AnyAsync(o =>
                o.ID_radnika == obaveza.ID_radnika &&
                o.datum == obaveza.datum &&
                o.ID_obaveze != obaveza.ID_obaveze);

            if (konflikt)
            {
                return BadRequest("Radnik već ima drugu obavezu u tom terminu.");
            }
        }

        _context.Entry(obaveza).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteObaveza(int id)
    {
        var obaveza = await _context.Obaveza.FindAsync(id);
        if (obaveza == null) return NotFound();

        _context.Obaveza.Remove(obaveza);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>ID=0 ruši FK; u bazi moraju biti NULL kad nije odabrano.</summary>
    private static void NormalizeObavezaFk(Obaveza o)
    {
        if (o.ID_jedinke == 0) o.ID_jedinke = null;
        if (o.ID_skupine == 0) o.ID_skupine = null;
        if (o.ID_radnika == 0) o.ID_radnika = null;
    }
}

