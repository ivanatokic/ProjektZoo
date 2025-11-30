using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentController : ControllerBase
{
    private readonly ZooContext _context;

    public IncidentController(ZooContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Incident>>> GetSviIncidenti()
    {
        return await _context.Incident
            .Include(i => i.IncidentNastambe)
            .Include(i => i.IncidentZivotinje)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Incident>> GetIncident(int id)
    {
        var incident = await _context.Incident
            .Include(i => i.IncidentNastambe)
            .Include(i => i.IncidentZivotinje)
            .FirstOrDefaultAsync(i => i.ID_incidenta == id);

        if (incident == null) return NotFound();
        return incident;
    }

    [HttpPost]
    public async Task<ActionResult<Incident>> PostIncident(Incident incident)
    {
        _context.Incident.Add(incident);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetIncident), new { id = incident.ID_incidenta }, incident);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutIncident(int id, Incident incident)
    {
        if (id != incident.ID_incidenta) return BadRequest();

        _context.Entry(incident).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncident(int id)
    {
        var incident = await _context.Incident.FindAsync(id);
        if (incident == null) return NotFound();

        _context.Incident.Remove(incident);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

