namespace Zoo.Models;

public class Obaveza
{
    public int ID_obaveze { get; set; }
    public string naziv { get; set; } = null!;
    public string? opis { get; set; }
    public string? status { get; set; }
    public DateTime? datum { get; set; }
    public string? periodicnost { get; set; }
    public int? ID_radnika { get; set; }
    public int? ID_jedinke { get; set; }
    public int? ID_skupine { get; set; }

    public Radnik? Radnik { get; set; }
    public Jedinka? Jedinka { get; set; }
    public Skupina? Skupina { get; set; }
}

