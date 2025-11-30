using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JedinkaController : ControllerBase
{
    private readonly ZooContext _context;

    public JedinkaController(ZooContext context)
    {
        _context = context;
    }

    // GET: api/Jedinka
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Jedinka>>> GetSveJedinke()
    {
        return await _context.Jedinka
            .Include(j => j.Vrsta)
            .Include(j => j.Nastamba)
            .ToListAsync();
    }

    // GET: api/Jedinka/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Jedinka>> GetJedinka(int id)
    {
        var jedinka = await _context.Jedinka
            .Include(j => j.Vrsta)
            .Include(j => j.Nastamba)
            .FirstOrDefaultAsync(j => j.ID_jedinke == id);

        if (jedinka == null)
            return NotFound();

        return jedinka;
    }
    // GET: api/Jedinka/PoVrsti/3
    [HttpGet("PoVrsti/{idVrste}")]
    public async Task<ActionResult<IEnumerable<Jedinka>>> GetJedinkePoVrsti(int idVrste)
    {
        var jedinke = await _context.Jedinka
            .Include(j => j.Vrsta)
            .Include(j => j.Nastamba)
            .Where(j => j.ID_vrste == idVrste && (j.aktivna == true || j.aktivna == null))
            .ToListAsync();

        if (!jedinke.Any())
            return NotFound();

        return Ok(jedinke);
    }


    // POST: api/Jedinka
    [HttpPost]
    public async Task<ActionResult<Jedinka>> PostJedinka(Jedinka jedinka)
    {
        _context.Jedinka.Add(jedinka);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJedinka),
            new { id = jedinka.ID_jedinke }, jedinka);
    }

    // PUT: api/Jedinka/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutJedinka(int id, Jedinka jedinka)
    {
        if (id != jedinka.ID_jedinke)
            return BadRequest();

        _context.Entry(jedinka).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Jedinka/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJedinka(int id)
    {
        var jedinka = await _context.Jedinka.FindAsync(id);
        if (jedinka == null)
            return NotFound();

        _context.Jedinka.Remove(jedinka);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

