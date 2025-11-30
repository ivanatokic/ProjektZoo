using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TuraController : ControllerBase
{
    private readonly ZooContext _context;

    public TuraController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tura>>> GetSveTure()
    {
        return await _context.Tura
            .Include(t => t.Vodic)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tura>> GetTura(int id)
    {
        var tura = await _context.Tura
            .Include(t => t.Vodic)
            .FirstOrDefaultAsync(t => t.ID_ture == id);

        if (tura == null) return NotFound();
        return tura;
    }

    [HttpPost]
    public async Task<ActionResult<Tura>> PostTura(Tura tura)
    {
        _context.Tura.Add(tura);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTura), new { id = tura.ID_ture }, tura);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTura(int id, Tura tura)
    {
        if (id != tura.ID_ture) return BadRequest();

        _context.Entry(tura).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTura(int id)
    {
        var tura = await _context.Tura.FindAsync(id);
        if (tura == null) return NotFound();

        _context.Tura.Remove(tura);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

