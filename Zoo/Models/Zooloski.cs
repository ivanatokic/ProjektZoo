using System.ComponentModel.DataAnnotations;

namespace Zoo.Models;

public class Zooloski
{
    [Key]
    public int ID_zoo { get; set; }

    public string naziv { get; set; } = null!;
    public string adresa { get; set; } = null!;
    public int? radno_vrijeme { get; set; }

    public ICollection<Radnik> Radnici { get; set; } = new List<Radnik>();
}
