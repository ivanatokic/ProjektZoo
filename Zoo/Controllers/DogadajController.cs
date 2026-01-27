using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DogadajController : ControllerBase
{
    private readonly ZooContext _context;

    public DogadajController(ZooContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Vraća sve događaje unutar zadanog vremenskog raspona (npr. jedan mjesec),
    /// uz pripadnu turu i vodiča, za prikaz u kalendaru.
    /// </summary>
    /// <param name="od">Početni datum (uključivo).</param>
    /// <param name="do">Završni datum (isključivo ili uključivo, ovisno o potrebi – ovdje uključivo).</param>
    [HttpGet("Kalendar")]
    public async Task<ActionResult<IEnumerable<Dogadaj>>> GetDogadajiZaKalendar(
        [FromQuery(Name = "od")] DateTime? od,
        [FromQuery(Name = "do")] DateTime? doDatum)
    {
        // default: današnji mjesec ako ništa nije poslano
        var start = od ?? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var end = doDatum ?? start.AddMonths(1).AddDays(-1);

        var query = _context.Dogadaj
            .Include(d => d.Tura)
                .ThenInclude(t => t.Vodic)
            .AsQueryable();

        query = query.Where(d => d.pocetak.Date >= start.Date && d.pocetak.Date <= end.Date);

        var result = await query
            .OrderBy(d => d.pocetak)
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Dogadaj>> GetDogadaj(int id)
    {
        var dogadaj = await _context.Dogadaj
            .Include(d => d.Tura)
                .ThenInclude(t => t.Vodic)
            .FirstOrDefaultAsync(d => d.ID_dogadaja == id);

        if (dogadaj == null) return NotFound();
        return dogadaj;
    }

    /// <summary>
    /// Dodavanje novog događaja u kalendar (npr. novi show).
    /// Ako je ID_ture = 0, postavlja se na NULL (događaj bez konkretne ture).
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Dogadaj>> PostDogadaj(Dogadaj dogadaj)
    {
        NormalizeFk(dogadaj);

        if (dogadaj.kraj.HasValue && dogadaj.kraj < dogadaj.pocetak)
        {
            return BadRequest("Kraj događaja ne može biti prije početka.");
        }

        _context.Dogadaj.Add(dogadaj);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDogadaj),
            new { id = dogadaj.ID_dogadaja }, dogadaj);
    }

    /// <summary>Uređivanje postojećeg događaja u kalendaru.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDogadaj(int id, Dogadaj dogadaj)
    {
        if (id != dogadaj.ID_dogadaja)
            return BadRequest();

        NormalizeFk(dogadaj);

        if (dogadaj.kraj.HasValue && dogadaj.kraj < dogadaj.pocetak)
        {
            return BadRequest("Kraj događaja ne može biti prije početka.");
        }

        _context.Entry(dogadaj).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>Brisanje događaja iz kalendara.</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDogadaj(int id)
    {
        var dogadaj = await _context.Dogadaj.FindAsync(id);
        if (dogadaj == null) return NotFound();

        _context.Dogadaj.Remove(dogadaj);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static void NormalizeFk(Dogadaj d)
    {
        if (d.ID_ture == 0)
            d.ID_ture = null;
    }
}

