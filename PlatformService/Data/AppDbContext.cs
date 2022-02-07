using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public class AppDbContext : DbContext
{
    public DbSet<Platform> Platforms { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>()
                    .HasNoDiscriminator()
                    .ToContainer(nameof(Platforms))
                    .HasPartitionKey(da => da.Id)
                    .HasKey(da => new { da.Id });
    }
}
