using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Models;

public class Trosak
{
    [Key]
    public int ID_troska { get; set; }

    public int? ID_jedinke { get; set; }
    public int? ID_skupine { get; set; }

    public string kategorija { get; set; } = null!;
    public decimal iznos { get; set; }
    public DateTime datum { get; set; }
    public string? opis { get; set; }

    public Jedinka? Jedinka { get; set; }
    public Skupina? Skupina { get; set; }
}