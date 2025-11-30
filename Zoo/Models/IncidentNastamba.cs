namespace Zoo.Models;

public class IncidentNastamba
{
    public int ID_incidenta { get; set; }
    public int ID_nastambe { get; set; }

    public Incident Incident { get; set; } = null!;
    public Nastamba Nastamba { get; set; } = null!;
}
