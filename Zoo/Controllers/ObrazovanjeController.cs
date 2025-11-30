using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObrazovanjeController : ControllerBase
{
    private readonly ZooContext _context;

    public ObrazovanjeController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Obrazovanje>>> GetSvaObrazovanja()
    {
        return await _context.Obrazovanje
            .Include(o => o.Radnici)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Obrazovanje>> GetObrazovanje(int id)
    {
        var obrazovanje = await _context.Obrazovanje
            .Include(o => o.Radnici)
            .FirstOrDefaultAsync(o => o.ID_obrazovanja == id);

        if (obrazovanje == null) return NotFound();
        return obrazovanje;
    }

    [HttpPost]
    public async Task<ActionResult<Obrazovanje>> PostObrazovanje(Obrazovanje obrazovanje)
    {
        _context.Obrazovanje.Add(obrazovanje);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetObrazovanje), new { id = obrazovanje.ID_obrazovanja }, obrazovanje);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutObrazovanje(int id, Obrazovanje obrazovanje)
    {
        if (id != obrazovanje.ID_obrazovanja) return BadRequest();

        _context.Entry(obrazovanje).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteObrazovanje(int id)
    {
        var obrazovanje = await _context.Obrazovanje.FindAsync(id);
        if (obrazovanje == null) return NotFound();

        _context.Obrazovanje.Remove(obrazovanje);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
