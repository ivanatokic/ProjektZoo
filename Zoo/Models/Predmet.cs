using System.ComponentModel.DataAnnotations;

namespace Zoo.Models;

/// <summary>
/// Predmet u nastambi: stijene, konstrukcije, ljuljačke, biljke itd.
/// </summary>
public class Predmet
{
    [Key]
    public int ID_predmeta { get; set; }

    public int ID_nastambe { get; set; }
    /// <summary>Tip predmeta: stijena, biljka, konstrukcija, ljuljačka, ograda, ...</summary>
    public string tip { get; set; } = null!;
    public string? naziv { get; set; }
    public string? opis { get; set; }

    public Nastamba Nastamba { get; set; } = null!;
}
