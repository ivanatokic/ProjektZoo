namespace Zoo.Models;

public class Raspored
{
    public int ID_rasporeda { get; set; }
    public int ID_radnika { get; set; }
    public DateTime datum { get; set; }
    public string? smjena { get; set; }   // char(2)
    public string? status { get; set; }

    public Radnik Radnik { get; set; } = null!;
}

