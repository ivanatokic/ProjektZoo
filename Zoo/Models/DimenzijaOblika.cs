using System.ComponentModel.DataAnnotations;

namespace Zoo.Models;

public class DimenzijaOblika
{
    [Key]
    public int ID_dimenzije { get; set; }

    public int ID_oblika { get; set; }
    public int redni_broj { get; set; }
    public double duljina { get; set; }

    public Oblik Oblik { get; set; } = null!;
}
