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
        if (tura.ID_vodica == 0) tura.ID_vodica = null;

        var overlap = await PreklapanjeVodicaAsync(tura.ID_vodica, tura.datum, tura.vrijeme_zavrsetka, iskljucitiIdTure: null);
        if (overlap)
            return BadRequest("Odabrani vodič je zauzet u tom terminu.");

        _context.Tura.Add(tura);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTura), new { id = tura.ID_ture }, tura);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTura(int id, Tura tura)
    {
        if (id != tura.ID_ture) return BadRequest();

        if (tura.ID_vodica == 0) tura.ID_vodica = null;

        var overlap = await PreklapanjeVodicaAsync(tura.ID_vodica, tura.datum, tura.vrijeme_zavrsetka, iskljucitiIdTure: id);
        if (overlap)
            return BadRequest("Odabrani vodič je zauzet u tom terminu.");

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

    /// <summary>Provjera preklapanja termina vodiča. Vraća true ako postoji preklapanje.</summary>
    private async Task<bool> PreklapanjeVodicaAsync(int? idVodica, DateTime datumPocetka, DateTime? vrijemeZavrsetka, int? iskljucitiIdTure)
    {
        if (!idVodica.HasValue || !vrijemeZavrsetka.HasValue)
            return false;

        var ostale = await _context.Tura
            .Where(t => t.ID_vodica == idVodica && t.vrijeme_zavrsetka != null &&
                (!iskljucitiIdTure.HasValue || t.ID_ture != iskljucitiIdTure.Value))
            .ToListAsync();

        foreach (var t in ostale)
        {
            if (t.vrijeme_zavrsetka == null) continue;
            if (datumPocetka < t.vrijeme_zavrsetka && vrijemeZavrsetka > t.datum)
                return true;
        }

        return false;
    }
}

