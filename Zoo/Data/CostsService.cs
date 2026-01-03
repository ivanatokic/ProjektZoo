using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace Zoo.Data;

public class CostsService
{
    private readonly ZooContext _context;

    public CostsService(ZooContext context)
    {
        _context = context;
    }

    public async Task<Trosak> DodajTrosakAsync(CostCreateDto dto)
    {
        var trosak = new Trosak
        {
            ID_jedinke = dto.ID_jedinke,
            ID_skupine = dto.ID_skupine,
            kategorija = dto.kategorija,
            iznos = dto.iznos,
            datum = dto.datum,
            opis = dto.opis
        };

        _context.Troskovi.Add(trosak);
        await _context.SaveChangesAsync();

        return trosak;
    }

    public async Task<CostSummaryDto> TroskoviJedinke(int ID_jedinke)
    {
        var troskovi = await _context.Troskovi
            .Where(t => t.ID_jedinke == ID_jedinke)
            .ToListAsync();

        return new CostSummaryDto
        {
            ukupno = troskovi.Sum(t => t.iznos),
            po_kategorijama = troskovi
                .GroupBy(t => t.kategorija)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.iznos))
        };
    }

    public async Task<CostSummaryDto> TroskoviSkupine(int ID_skupine)
    {
        var troskovi = await _context.Troskovi
            .Where(t => t.ID_skupine == ID_skupine)
            .ToListAsync();

        return new CostSummaryDto
        {
            ukupno = troskovi.Sum(t => t.iznos),
            po_kategorijama = troskovi
                .GroupBy(t => t.kategorija)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.iznos))
        };
    }

    public async Task<object> NajskupljeJedinke(int top)
    {
        return await _context.Troskovi
            .Where(t => t.ID_jedinke != null)
            .GroupBy(t => t.ID_jedinke)
            .Select(g => new
            {
                ID_jedinke = g.Key,
                ukupno = g.Sum(x => x.iznos)
            })
            .OrderByDescending(x => x.ukupno)
            .Take(top)
            .ToListAsync();
    }
}