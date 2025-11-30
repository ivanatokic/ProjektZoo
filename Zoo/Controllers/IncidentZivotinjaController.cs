using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentZivotinjaController : ControllerBase
{
    private readonly ZooContext _context;

    public IncidentZivotinjaController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncidentZivotinja>>> GetSvi()
    {
        return await _context.IncidentZivotinja
            .Include(x => x.Incident)
            .Include(x => x.Vrsta)
            .ToListAsync();
    }

    [HttpGet("{idIncident}/{idVrste}")]
    public async Task<ActionResult<IncidentZivotinja>> GetJedan(int idIncident, int idVrste)
    {
        var veza = await _context.IncidentZivotinja
            .Include(x => x.Incident)
            .Include(x => x.Vrsta)
            .FirstOrDefaultAsync(x => x.ID_incidenta == idIncident && x.ID_vrste == idVrste);

        if (veza == null) return NotFound();
        return veza;
    }

    [HttpPost]
    public async Task<ActionResult<IncidentZivotinja>> Post(IncidentZivotinja veza)
    {
        _context.IncidentZivotinja.Add(veza);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJedan),
            new { idIncident = veza.ID_incidenta, idVrste = veza.ID_vrste }, veza);
    }

    [HttpDelete("{idIncident}/{idVrste}")]
    public async Task<IActionResult> Delete(int idIncident, int idVrste)
    {
        var veza = await _context.IncidentZivotinja
            .FirstOrDefaultAsync(x => x.ID_incidenta == idIncident && x.ID_vrste == idVrste);

        if (veza == null) return NotFound();

        _context.IncidentZivotinja.Remove(veza);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

