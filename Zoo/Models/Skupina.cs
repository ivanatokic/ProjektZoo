using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Models;

public class Skupina
{
    [Key]
    public int ID_skupine { get; set; }

    public string naziv { get; set; } = null!;
    public int ID_vrste { get; set; }
    public int ID_nastambe { get; set; }
    public int? prosjecan_broj { get; set; }
    public string? opis { get; set; }
    public DateTime? datum_nabavke { get; set; }
    /// <summary>Način nabave: rođen, kupljen, doniran, posuđen, ... (tip_troska/trosak = trošak).</summary>
    public string? nacin_nabavke { get; set; }
    public decimal? trosak { get; set; }
    public string? tip_troska { get; set; }
    /// <summary>Poveznica (URL) na dodatne informacije.</summary>
    public string? poveznica { get; set; }
    public bool aktivna { get; set; }
    /// <summary>True = primarne životinje; false = sekundarne (namjerno postavljene radi okoliša).</summary>
    public bool primarna { get; set; } = true;

    /// <summary>Hrvatski naziv vrste (iz tablice Vrsta). U ispisu uz Include(Vrsta).</summary>
    [NotMapped]
    public string? hr_naziv => Vrsta?.hr_naziv;
    /// <summary>Latinski naziv vrste (iz tablice Vrsta). U ispisu uz Include(Vrsta).</summary>
    [NotMapped]
    public string? lat_naziv => Vrsta?.lat_naziv;

    public Vrsta Vrsta { get; set; } = null!;
    public Nastamba Nastamba { get; set; } = null!;
    public ICollection<Obaveza> Obaveze { get; set; } = new List<Obaveza>();
    public ICollection<Trosak> Troskovi { get; set; } = new List<Trosak>();
}
