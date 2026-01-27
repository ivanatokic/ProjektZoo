using System.ComponentModel.DataAnnotations;

namespace Zoo.Models;

/// <summary>
/// Kalendar događaja (npr. show lavova, posebne ture, radionice).
/// Svaki događaj može biti vezan uz konkretnu turu, ali i ne mora.
/// </summary>
public class Dogadaj
{
    [Key]
    public int ID_dogadaja { get; set; }

    /// <summary>Naziv događaja prikazan u kalendaru.</summary>
    [MaxLength(100)]
    public string naziv { get; set; } = null!;

    /// <summary>Detaljniji opis događaja.</summary>
    public string? opis { get; set; }

    /// <summary>Početak događaja (datum i vrijeme).</summary>
    public DateTime pocetak { get; set; }

    /// <summary>Kraj događaja (datum i vrijeme).</summary>
    public DateTime? kraj { get; set; }

    /// <summary>Tip događaja (npr. show, radionica, specijalna tura...).</summary>
    [MaxLength(50)]
    public string? tip { get; set; }

    /// <summary>Opcionalna veza na turu ako je događaj dio ture.</summary>
    public int? ID_ture { get; set; }

    public Tura? Tura { get; set; }
}

