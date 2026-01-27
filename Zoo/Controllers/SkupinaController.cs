using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkupinaController : ControllerBase
{
    private readonly ZooContext _context;

    public SkupinaController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Skupina>>> GetSveSkupine()
    {
        return await _context.Skupina
            .Include(s => s.Vrsta)
            .Include(s => s.Nastamba)
            .Where(s => s.aktivna)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Skupina>> GetSkupina(int id)
    {
        var skupina = await _context.Skupina
            .Include(s => s.Vrsta)
            .Include(s => s.Nastamba)
            .FirstOrDefaultAsync(s => s.ID_skupine == id);

        if (skupina == null) return NotFound();
        return skupina;
    }

    [HttpPost]
    public async Task<ActionResult<Skupina>> PostSkupina(Skupina skupina)
    {
        _context.Skupina.Add(skupina);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSkupina), new { id = skupina.ID_skupine }, skupina);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutSkupina(int id, Skupina skupina)
    {
        if (id != skupina.ID_skupine) return BadRequest();

        _context.Entry(skupina).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>Soft delete: postavlja aktivna = false umjesto brisanja zapisa.</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSkupina(int id)
    {
        var skupina = await _context.Skupina.FindAsync(id);
        if (skupina == null) return NotFound();

        skupina.aktivna = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}/deaktiviraj")]
    public async Task<IActionResult> DeaktivirajSkupinu(int id)
    {
        var skupina = await _context.Skupina.FindAsync(id);
        if (skupina == null) return NotFound();

        skupina.aktivna = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
