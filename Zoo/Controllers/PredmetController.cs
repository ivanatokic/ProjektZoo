using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PredmetController : ControllerBase
{
    private readonly ZooContext _context;

    public PredmetController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Predmet>>> GetSviPredmeti()
    {
        return await _context.Predmet
            .Include(p => p.Nastamba)
            .ToListAsync();
    }

    [HttpGet("Nastamba/{idNastambe}")]
    public async Task<ActionResult<IEnumerable<Predmet>>> GetPredmetiNastambe(int idNastambe)
    {
        var predmeti = await _context.Predmet
            .Include(p => p.Nastamba)
            .Where(p => p.ID_nastambe == idNastambe)
            .ToListAsync();
        return predmeti;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Predmet>> GetPredmet(int id)
    {
        var predmet = await _context.Predmet
            .Include(p => p.Nastamba)
            .FirstOrDefaultAsync(p => p.ID_predmeta == id);

        if (predmet == null) return NotFound();
        return predmet;
    }

    [HttpPost]
    public async Task<ActionResult<Predmet>> PostPredmet(Predmet predmet)
    {
        _context.Predmet.Add(predmet);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPredmet), new { id = predmet.ID_predmeta }, predmet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPredmet(int id, Predmet predmet)
    {
        if (id != predmet.ID_predmeta) return BadRequest();

        _context.Entry(predmet).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePredmet(int id)
    {
        var predmet = await _context.Predmet.FindAsync(id);
        if (predmet == null) return NotFound();

        _context.Predmet.Remove(predmet);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
