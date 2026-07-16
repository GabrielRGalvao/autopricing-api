using AutoPricing.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoPricing.Api.Data;

public class VehicleDbContext : DbContext
{
    public VehicleDbContext(DbContextOptions<VehicleDbContext> options)
        : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles { get; set; }
}