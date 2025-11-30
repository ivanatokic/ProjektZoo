namespace Zoo.Models;

public class Incident
{
    public int ID_incidenta { get; set; }
    public DateTime datum { get; set; }
    public string? opis { get; set; }
    public int? razina_zivotinje { get; set; }
    public int? razina_nastambe { get; set; }
    public decimal? trosak_popravka { get; set; }

    public ICollection<IncidentNastamba> IncidentNastambe { get; set; } = new List<IncidentNastamba>();
    public ICollection<IncidentZivotinja> IncidentZivotinje { get; set; } = new List<IncidentZivotinja>();
}
