using CountriesAPI.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CountriesAPI.DataLayer;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : DbContext(options)
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
                    .HasMany(c => c.Cities)
                    .WithOne(c => c.Country)
                    .HasForeignKey(c => c.CountryId)
                    .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}