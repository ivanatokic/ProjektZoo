using System.ComponentModel.DataAnnotations;

namespace Zoo.Models;

public class Tura
{
    [Key]
    public int ID_ture { get; set; }

    public DateTime datum { get; set; }
    public int? broj_posjetitelja { get; set; }
    public int? ID_vodica { get; set; }
    public string? opis { get; set; }

    public Radnik? Vodic { get; set; }
}
