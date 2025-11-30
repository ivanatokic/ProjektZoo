namespace Zoo.Models;

public class Vrsta
{
    public int ID_vrste { get; set; }
    public string hr_naziv { get; set; } = null!;
    public string lat_naziv { get; set; } = null!;

    public ICollection<Jedinka> Jedinke { get; set; } = new List<Jedinka>();
    public ICollection<Skupina> Skupine { get; set; } = new List<Skupina>();
    public ICollection<IncidentZivotinja> IncidentZivotinje { get; set; } = new List<IncidentZivotinja>();
}

