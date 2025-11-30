namespace Zoo.Models;

public class Tura
{
    public int ID_ture { get; set; }
    public DateTime datum { get; set; }
    public int? broj_posjetitelja { get; set; }
    public int? ID_vodica { get; set; }
    public string? opis { get; set; }

    public Radnik? Vodic { get; set; }
}
