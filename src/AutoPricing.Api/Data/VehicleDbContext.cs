using AutoPricing.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoPricing.Api.Data;

public class VehicleDbContext : DbContext
{
    public VehicleDbContext(DbContextOptions<VehicleDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Vehicle>()
        .Property(vehicle => vehicle.Price)
        .HasPrecision(18, 2);

    modelBuilder.Entity<User>()
        .HasIndex(user => user.Email)
        .IsUnique();
}
}