namespace Zoo.Models;

public class Radnik
{
    public int ID_radnika { get; set; }
    public string ime { get; set; } = null!;
    public string prezime { get; set; } = null!;
    public string kontakt_broj { get; set; } = null!;
    public int? ID_obrazovanja { get; set; }
    public string? tip_radnika { get; set; }
    public int ID_zoo { get; set; }

    public Obrazovanje? Obrazovanje { get; set; }
    public Zooloski Zoo { get; set; } = null!;

    public ICollection<Raspored> Rasporedi { get; set; } = new List<Raspored>();
    public ICollection<Obaveza> Obaveze { get; set; } = new List<Obaveza>();
    public ICollection<Tura> Ture { get; set; } = new List<Tura>();
}

