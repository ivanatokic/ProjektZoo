using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace Zoo.Data;

public class ZooContext : DbContext
{
    public ZooContext(DbContextOptions<ZooContext> options) : base(options)
    {
    }

    public DbSet<Incident> Incident { get; set; } = null!;
    public DbSet<IncidentNastamba> IncidentNastamba { get; set; } = null!;
    public DbSet<IncidentZivotinja> IncidentZivotinja { get; set; } = null!;
    public DbSet<Jedinka> Jedinka { get; set; } = null!;
    public DbSet<Nastamba> Nastamba { get; set; } = null!;
    public DbSet<Obaveza> Obaveza { get; set; } = null!;
    public DbSet<Obrazovanje> Obrazovanje { get; set; } = null!;
    public DbSet<Radnik> Radnik { get; set; } = null!;
    public DbSet<Raspored> Raspored { get; set; } = null!;
    public DbSet<Skupina> Skupina { get; set; } = null!;
    public DbSet<Tura> Tura { get; set; } = null!;
    public DbSet<Vrsta> Vrsta { get; set; } = null!;
    public DbSet<Zooloski> Zooloski { get; set; } = null!;
    public DbSet<Trosak> Troskovi { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // složeni ključevi
        modelBuilder.Entity<IncidentNastamba>()
            .HasKey(x => new { x.ID_incidenta, x.ID_nastambe });

        modelBuilder.Entity<IncidentZivotinja>()
            .HasKey(x => new { x.ID_incidenta, x.ID_vrste });

        // veze Jedinka – Vrsta / Nastamba
        modelBuilder.Entity<Jedinka>()
            .HasOne(j => j.Vrsta)
            .WithMany(v => v.Jedinke)
            .HasForeignKey(j => j.ID_vrste);

        modelBuilder.Entity<Jedinka>()
            .HasOne(j => j.Nastamba)
            .WithMany(n => n.Jedinke)
            .HasForeignKey(j => j.ID_nastambe);

        // veze Skupina – Vrsta / Nastamba
        modelBuilder.Entity<Skupina>()
            .HasOne(s => s.Vrsta)
            .WithMany(v => v.Skupine)
            .HasForeignKey(s => s.ID_vrste);

        modelBuilder.Entity<Skupina>()
            .HasOne(s => s.Nastamba)
            .WithMany(n => n.Skupine)
            .HasForeignKey(s => s.ID_nastambe);

        // Obaveza – Radnik / Jedinka / Skupina
        modelBuilder.Entity<Obaveza>()
            .HasOne(o => o.Radnik)
            .WithMany(r => r.Obaveze)
            .HasForeignKey(o => o.ID_radnika);

        modelBuilder.Entity<Obaveza>()
            .HasOne(o => o.Jedinka)
            .WithMany(j => j.Obaveze)
            .HasForeignKey(o => o.ID_jedinke);

        modelBuilder.Entity<Obaveza>()
            .HasOne(o => o.Skupina)
            .WithMany(s => s.Obaveze)
            .HasForeignKey(o => o.ID_skupine);

        // Raspored – Radnik
        modelBuilder.Entity<Raspored>()
            .HasOne(r => r.Radnik)
            .WithMany(rn => rn.Rasporedi)
            .HasForeignKey(r => r.ID_radnika);

        // Tura – Radnik (vodic)
        modelBuilder.Entity<Tura>()
            .HasOne(t => t.Vodic)
            .WithMany(r => r.Ture)
            .HasForeignKey(t => t.ID_vodica);

        // Radnik – Obrazovanje / Zooloski
        modelBuilder.Entity<Radnik>()
            .HasOne(r => r.Obrazovanje)
            .WithMany(o => o.Radnici)
            .HasForeignKey(r => r.ID_obrazovanja);

        modelBuilder.Entity<Radnik>()
            .HasOne(r => r.Zoo)
            .WithMany(z => z.Radnici)
            .HasForeignKey(r => r.ID_zoo);

        // IncidentNastamba – veze
        modelBuilder.Entity<IncidentNastamba>()
            .HasOne(inas => inas.Incident)
            .WithMany(i => i.IncidentNastambe)
            .HasForeignKey(inas => inas.ID_incidenta);

        modelBuilder.Entity<IncidentNastamba>()
            .HasOne(inas => inas.Nastamba)
            .WithMany(n => n.IncidentNastambe)
            .HasForeignKey(inas => inas.ID_nastambe);

        // IncidentZivotinja – veze
        modelBuilder.Entity<IncidentZivotinja>()
            .HasOne(iz => iz.Incident)
            .WithMany(i => i.IncidentZivotinje)
            .HasForeignKey(iz => iz.ID_incidenta);

        modelBuilder.Entity<IncidentZivotinja>()
            .HasOne(iz => iz.Vrsta)
            .WithMany(v => v.IncidentZivotinje)
            .HasForeignKey(iz => iz.ID_vrste);
    }
}
