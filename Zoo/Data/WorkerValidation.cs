using Microsoft.EntityFrameworkCore;

namespace Zoo.Data;

public class WorkerValidationService
{
    private readonly ZooContext _context;

    public WorkerValidationService(ZooContext context)
    {
        _context = context;
    }

    public async Task<bool> RadnikJeSlobodan(int ID_radnika, DateTime datum)
    {
        return !await _context.Obaveza
            .AnyAsync(o =>
                o.ID_radnika == ID_radnika &&
                o.datum.HasValue &&
                o.datum.Value.Date == datum.Date);
    }

    public async Task<bool> RadnikJeURasporedu(
        int ID_radnika,
        DateTime datum,
        string smjena)
    {
        return await _context.Raspored
            .AnyAsync(r =>
                r.ID_radnika == ID_radnika &&
                r.datum.Date == datum.Date &&
                r.smjena == smjena);
    }

    public async Task<bool> RadnikMozeDobitiObavezu(
        int ID_radnika,
        DateTime datum,
        string smjena)
    {
        return await RadnikJeSlobodan(ID_radnika, datum)
            && await RadnikJeURasporedu(ID_radnika, datum, smjena);
    }
}