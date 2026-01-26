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

    var postojeca = await _context.Skupina.FindAsync(id);
    if (postojeca == null) return NotFound();

    postojeca.naziv = skupina.naziv;
    postojeca.ID_vrste = skupina.ID_vrste;
    postojeca.ID_nastambe = skupina.ID_nastambe;
    postojeca.prosjecan_broj = skupina.prosjecan_broj;
    postojeca.opis = skupina.opis;
    postojeca.datum_nabavke = skupina.datum_nabavke;
    postojeca.trosak = skupina.trosak;
    postojeca.tip_troska = skupina.tip_troska;

    postojeca.aktivna = skupina.aktivna;

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

