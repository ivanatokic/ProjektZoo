namespace Zoo.Models;

public class Obrazovanje
{
    public int ID_obrazovanja { get; set; }
    public string naziv { get; set; } = null!;
    public bool? trajno { get; set; }

    public ICollection<Radnik> Radnici { get; set; } = new List<Radnik>();
}

