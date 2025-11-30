namespace Zoo.Models;

public class Nastamba
{
    public int ID_nastambe { get; set; }
    public string oznaka { get; set; } = null!;
    public string? opis { get; set; }
    public string? sjenčenje { get; set; }
    public string? geometrija { get; set; }

    public ICollection<Jedinka> Jedinke { get; set; } = new List<Jedinka>();
    public ICollection<Skupina> Skupine { get; set; } = new List<Skupina>();
    public ICollection<IncidentNastamba> IncidentNastambe { get; set; } = new List<IncidentNastamba>();
}

