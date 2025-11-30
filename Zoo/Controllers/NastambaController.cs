using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NastambaController : ControllerBase
{
    private readonly ZooContext _context;

    public NastambaController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Nastamba>>> GetSveNastambe()
    {
        return await _context.Nastamba
            .Include(n => n.Jedinke)
            .Include(n => n.Skupine)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Nastamba>> GetNastamba(int id)
    {
        var nastamba = await _context.Nastamba
            .Include(n => n.Jedinke)
            .Include(n => n.Skupine)
            .FirstOrDefaultAsync(n => n.ID_nastambe == id);

        if (nastamba == null) return NotFound();
        return nastamba;
    }

    [HttpPost]
    public async Task<ActionResult<Nastamba>> PostNastamba(Nastamba nastamba)
    {
        _context.Nastamba.Add(nastamba);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNastamba), new { id = nastamba.ID_nastambe }, nastamba);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNastamba(int id, Nastamba nastamba)
    {
        if (id != nastamba.ID_nastambe) return BadRequest();

        _context.Entry(nastamba).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNastamba(int id)
    {
        var nastamba = await _context.Nastamba.FindAsync(id);
        if (nastamba == null) return NotFound();

        _context.Nastamba.Remove(nastamba);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

