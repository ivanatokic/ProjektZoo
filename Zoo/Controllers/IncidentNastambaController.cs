using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentNastambaController : ControllerBase
{
    private readonly ZooContext _context;

    public IncidentNastambaController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncidentNastamba>>> GetSvi()
    {
        return await _context.IncidentNastamba
            .Include(x => x.Incident)
            .Include(x => x.Nastamba)
            .ToListAsync();
    }

    [HttpGet("{idIncident}/{idNastambe}")]
    public async Task<ActionResult<IncidentNastamba>> GetJedan(int idIncident, int idNastambe)
    {
        var veza = await _context.IncidentNastamba
            .Include(x => x.Incident)
            .Include(x => x.Nastamba)
            .FirstOrDefaultAsync(x => x.ID_incidenta == idIncident && x.ID_nastambe == idNastambe);

        if (veza == null) return NotFound();
        return veza;
    }

    [HttpPost]
    public async Task<ActionResult<IncidentNastamba>> Post(IncidentNastamba veza)
    {
        _context.IncidentNastamba.Add(veza);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetJedan),
            new { idIncident = veza.ID_incidenta, idNastambe = veza.ID_nastambe }, veza);
    }

    [HttpDelete("{idIncident}/{idNastambe}")]
    public async Task<IActionResult> Delete(int idIncident, int idNastambe)
    {
        var veza = await _context.IncidentNastamba
            .FirstOrDefaultAsync(x => x.ID_incidenta == idIncident && x.ID_nastambe == idNastambe);

        if (veza == null) return NotFound();

        _context.IncidentNastamba.Remove(veza);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

