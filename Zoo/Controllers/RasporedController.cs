using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RasporedController : ControllerBase
{
    private readonly ZooContext _context;

    public RasporedController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Raspored>>> GetSviRasporedi()
    {
        return await _context.Raspored
            .Include(r => r.Radnik)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Raspored>> GetRaspored(int id)
    {
        var raspored = await _context.Raspored
            .Include(r => r.Radnik)
            .FirstOrDefaultAsync(r => r.ID_rasporeda == id);

        if (raspored == null) return NotFound();
        return raspored;
    }

    [HttpPost]
    public async Task<ActionResult<Raspored>> PostRaspored(Raspored raspored)
    {
        _context.Raspored.Add(raspored);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRaspored), new { id = raspored.ID_rasporeda }, raspored);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRaspored(int id, Raspored raspored)
    {
        if (id != raspored.ID_rasporeda) return BadRequest();

        _context.Entry(raspored).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRaspored(int id)
    {
        var raspored = await _context.Raspored.FindAsync(id);
        if (raspored == null) return NotFound();

        _context.Raspored.Remove(raspored);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

