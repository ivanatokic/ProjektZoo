using System.ComponentModel.DataAnnotations;

namespace Zoo.Models;

public class Tura
{
    [Key]
    public int ID_ture { get; set; }

    /// <summary>Početak ture (datum i vrijeme).</summary>
    public DateTime datum { get; set; }
    /// <summary>Kraj ture (datum i vrijeme). Za provjeru preklapanja vodiča.</summary>
    public DateTime? vrijeme_zavrsetka { get; set; }
    public int? broj_posjetitelja { get; set; }
    public int? ID_vodica { get; set; }
    public string? opis { get; set; }
    /// <summary>Je li za turu potreban vodič?</summary>
    public bool potreban_vodic { get; set; }
    /// <summary>Status: planirana, dodijeljena, otkazana, zavrsena.</summary>
    [MaxLength(50)]
    public string status { get; set; } = "planirana";

    public Radnik? Vodic { get; set; }

    /// <summary>Događaji povezani s ovom turom (za prikaz u kalendaru).</summary>
    public ICollection<Dogadaj> Dogadaji { get; set; } = new List<Dogadaj>();
}
