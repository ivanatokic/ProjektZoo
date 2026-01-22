using System.ComponentModel.DataAnnotations;

namespace Zoo.Models;

public class Oblik
{
    [Key]
    public int ID_oblika { get; set; }

    public string naziv { get; set; } = null!;
    public int? broj_stranica { get; set; }

    public ICollection<DimenzijaOblika> Dimenzije { get; set; } = new List<DimenzijaOblika>();
    public ICollection<Nastamba> Nastambe { get; set; } = new List<Nastamba>();
}
