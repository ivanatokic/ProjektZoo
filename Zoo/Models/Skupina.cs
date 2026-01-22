using System.ComponentModel.DataAnnotations;

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
    public decimal? trosak { get; set; }
    public string? tip_troska { get; set; }

    public Vrsta Vrsta { get; set; } = null!;
    public Nastamba Nastamba { get; set; } = null!;
    public ICollection<Obaveza> Obaveze { get; set; } = new List<Obaveza>();
    public ICollection<Trosak> Troskovi { get; set; } = new List<Trosak>();
}
