using CBTW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CBTW.Infrastructure.Data;

public class MilotoDbContext : DbContext
{
    public MilotoDbContext(DbContextOptions<MilotoDbContext> options) : base(options) { }

    public DbSet<Draw> Draws => Set<Draw>();

    public DbSet<Prospect> Prospects => Set<Prospect>();

    public DbSet<ComparisonResult> ComparisonResults => Set<ComparisonResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Draw>(d =>
        {
            d.ToTable("Draws", schema: "public");
            d.HasKey(x => x.DrawId);
            d.Property(x => x.DrawId).HasColumnName("DrawId");
            d.Property(x => x.N1).HasColumnName("N1");
            d.Property(x => x.N2).HasColumnName("N2");
            d.Property(x => x.N3).HasColumnName("N3");
            d.Property(x => x.N4).HasColumnName("N4");
            d.Property(x => x.N5).HasColumnName("N5");
        });
        modelBuilder.Entity<Prospect>(b =>
        {
            b.ToTable("prospects", schema: "public");
            b.HasKey(p => new { p.DrawId, p.Position, p.ProspectType });
            b.Property(p => p.DrawId).HasColumnName("DrawId");
            b.Property(p => p.Position).HasColumnName("Position");
            b.Property(p => p.ProspectType).HasColumnName("ProspectType");
            b.Property(p => p.N1).HasColumnName("N1");
            b.Property(p => p.N2).HasColumnName("N2");
            b.Property(p => p.N3).HasColumnName("N3");
            b.Property(p => p.N4).HasColumnName("N4");
            b.Property(p => p.N5).HasColumnName("N5");
            b.Property(p => p.Weight).HasColumnName("Weight");
        });
        modelBuilder.Entity<ComparisonResult>(b =>
        {
            b.ToTable("comparison_results", schema: "public");
            b.HasKey(c => c.DrawId);
            b.Property(c => c.DrawId).HasColumnName("DrawId");
            b.Property(c => c.DrawNumbers).HasColumnName("DrawNumbers");
            b.Property(c => c.ProspectNumbers).HasColumnName("ProspectNumbers");
            b.Property(c => c.Hits).HasColumnName("Hits");
            b.Property(c => c.MatchedNumbers).HasColumnName("MatchedNumbers");
        });
        base.OnModelCreating(modelBuilder);
        
    }
}

