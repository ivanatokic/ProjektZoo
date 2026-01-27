namespace Zoo.Models;

public class CostCreateDto
{
    public int? ID_jedinke { get; set; }
    public int? ID_skupine { get; set; }

    public string kategorija { get; set; } = null!;
    public decimal iznos { get; set; }
    public DateTime datum { get; set; }
    public string? opis { get; set; }
}

public class CostSummaryDto
{
    public decimal ukupno { get; set; }
    public Dictionary<string, decimal> po_kategorijama { get; set; } = new();
}

/// <summary>Stavka izračuna po kategoriji (za endpoint PoKategoriji).</summary>
public class IzracunPoKategorijiItemDto
{
    public string kategorija { get; set; } = null!;
    public decimal ukupno { get; set; }
    public int broj { get; set; }
}