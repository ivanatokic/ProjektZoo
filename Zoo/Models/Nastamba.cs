using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Zoo.Models;

public class Nastamba
{
    [Key]
    public int ID_nastambe { get; set; }

    [Required]
    public string oznaka { get; set; } = null!;

    public string? opis { get; set; }

    public string? sijencenje { get; set; }

    [Required]
    public Geometry oblik { get; set; } = null!;

    [NotMapped]
    public Geometry? koordinate { get; set; }

    public ICollection<Jedinka> Jedinke { get; set; } = new List<Jedinka>();
    public ICollection<Skupina> Skupine { get; set; } = new List<Skupina>();
    public ICollection<Predmet> Predmeti { get; set; } = new List<Predmet>();
    public ICollection<IncidentNastamba> IncidentNastambe { get; set; } = new List<IncidentNastamba>();
}