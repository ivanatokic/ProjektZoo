using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZooloskiController : ControllerBase
{
    private readonly ZooContext _context;

    public ZooloskiController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Zooloski>>> GetSviZoo()
    {
        return await _context.Zooloski
            .Include(z => z.Radnici)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Zooloski>> GetZoo(int id)
    {
        var zoo = await _context.Zooloski
            .Include(z => z.Radnici)
            .FirstOrDefaultAsync(z => z.ID_zoo == id);

        if (zoo == null) return NotFound();
        return zoo;
    }

    [HttpPost]
    public async Task<ActionResult<Zooloski>> PostZoo(Zooloski zoo)
    {
        _context.Zooloski.Add(zoo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetZoo), new { id = zoo.ID_zoo }, zoo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutZoo(int id, Zooloski zoo)
    {
        if (id != zoo.ID_zoo) return BadRequest();

        _context.Entry(zoo).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteZoo(int id)
    {
        var zoo = await _context.Zooloski.FindAsync(id);
        if (zoo == null) return NotFound();

        _context.Zooloski.Remove(zoo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

