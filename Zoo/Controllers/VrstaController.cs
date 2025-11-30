using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VrstaController : ControllerBase
{
    private readonly ZooContext _context;

    public VrstaController(ZooContext context)
    {
        _context = context;
    }

    // GET: api/Vrsta
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vrsta>>> GetSveVrste()
    {
        var vrste = await _context.Vrsta.ToListAsync();
        return Ok(vrste);
    }
}

