using Microsoft.AspNetCore.Mvc;
using Zoo.Data;
using Zoo.Models;

namespace Zoo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TroskoviController : ControllerBase
{
    private readonly CostsService _costsService;

    public TroskoviController(CostsService costsService)
    {
        _costsService = costsService;
    }

    // GET: /api/Troskovi/Izracun/5
    [HttpGet("Izracun/{id}")]
    public async Task<IActionResult> Izracun(int id)
    {
        var rezultat = await _costsService.TroskoviJedinke(id);
        return Ok(rezultat);
    }

    // GET: /api/Troskovi/Najskuplje?top=5
    [HttpGet("Najskuplje")]
    public async Task<IActionResult> Najskuplje([FromQuery] int top = 5)
    {
        var rezultat = await _costsService
            .NajskupljeJedinke(top);   // LINQ GroupBy + Sum

        return Ok(rezultat);
    }
}