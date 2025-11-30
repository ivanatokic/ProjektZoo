using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RadnikController : ControllerBase
{
    private readonly ZooContext _context;

    public RadnikController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Radnik>>> GetSviRadnici()
    {
        return await _context.Radnik
            .Include(r => r.Obrazovanje)
            .Include(r => r.Zoo)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Radnik>> GetRadnik(int id)
    {
        var radnik = await _context.Radnik
            .Include(r => r.Obrazovanje)
            .Include(r => r.Zoo)
            .FirstOrDefaultAsync(r => r.ID_radnika == id);

        if (radnik == null) return NotFound();
        return radnik;
    }

    [HttpPost]
    public async Task<ActionResult<Radnik>> PostRadnik(Radnik radnik)
    {
        _context.Radnik.Add(radnik);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRadnik), new { id = radnik.ID_radnika }, radnik);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRadnik(int id, Radnik radnik)
    {
        if (id != radnik.ID_radnika) return BadRequest();

        _context.Entry(radnik).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRadnik(int id)
    {
        var radnik = await _context.Radnik.FindAsync(id);
        if (radnik == null) return NotFound();

        _context.Radnik.Remove(radnik);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

