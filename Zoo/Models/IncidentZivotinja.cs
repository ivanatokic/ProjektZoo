namespace Zoo.Models;

public class IncidentZivotinja
{
    public int ID_incidenta { get; set; }
    public int ID_vrste { get; set; }

    public Incident Incident { get; set; } = null!;
    public Vrsta Vrsta { get; set; } = null!;
}
