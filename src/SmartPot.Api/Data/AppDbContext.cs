using Microsoft.EntityFrameworkCore;
using SmartPot.Api.Models;

namespace SmartPot.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PlantData> PlantReadings => Set<PlantData>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PlantData>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DeviceId).IsRequired().HasMaxLength(64);
            entity.Property(e => e.SoilMoisture).IsRequired();
            entity.Property(e => e.Temperature).IsRequired();
            entity.HasIndex(e => e.DeviceId);
            entity.HasIndex(e => e.RecordedAt);
        });
    }
}
