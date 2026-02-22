using DominicaAddressAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DominicaAddressAPI.Data;

public class DominicaDbContext : DbContext
{
    public DominicaDbContext(DbContextOptions<DominicaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Parish> Parishes => Set<Parish>();
    public DbSet<Settlement> Settlements => Set<Settlement>();
    public DbSet<Street> Streets => Set<Street>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Parish>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(10);
        });

        modelBuilder.Entity<Settlement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired();
            entity.HasOne(e => e.Parish)
                  .WithMany(p => p.Settlements)
                  .HasForeignKey(e => e.ParishId);
            entity.HasIndex(e => e.ParishId);
            entity.HasIndex(e => e.Type);
        });

        modelBuilder.Entity<Street>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasOne(e => e.Settlement)
                  .WithMany(s => s.Streets)
                  .HasForeignKey(e => e.SettlementId);
            entity.HasIndex(e => e.SettlementId);
        });
    }
}
