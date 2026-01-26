using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Models;

public class Incident
{
    [Key]
    public int ID_incidenta { get; set; }

    public DateTime datum { get; set; }

    public string? opis { get; set; }

    public int? razina_zivotinje { get; set; }

    public int? razina_nastambe { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? trosak_popravka { get; set; }
    public string? radovi_sanacije { get; set; }

    public int? ID_radnika_sanacije { get; set; }

    public DateTime? datum_sanacije { get; set; }

    [ForeignKey(nameof(ID_radnika_sanacije))]
    public Radnik? RadnikSanacije { get; set; }

    public ICollection<IncidentNastamba> IncidentNastambe { get; set; } = new List<IncidentNastamba>();
    public ICollection<IncidentZivotinja> IncidentZivotinje { get; set; } = new List<IncidentZivotinja>();
}
