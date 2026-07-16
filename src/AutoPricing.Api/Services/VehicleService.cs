using AutoPricing.Api.DTOs;
using AutoPricing.Api.Models;
using AutoPricing.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoPricing.Api.Services;

public class VehicleService
{
    private readonly VehicleDbContext _context;

    public VehicleService(VehicleDbContext context)
    {
        _context = context;
    }

    public void AddVehicle(CreateVehicleDto dto)
{
    var vehicle = new Vehicle
    {
        Brand = dto.Brand,
        Model = dto.Model,
        Year = dto.Year,
        Mileage = dto.Mileage,
        Price = dto.Price
    };

    _context.Vehicles.Add(vehicle);
    _context.SaveChanges();
}
    public List<Vehicle> GetAllVehicles()
    {
        return _context.Vehicles.ToList();
    }

    public Vehicle? GetVehicleById(int id)
    {
        return _context.Vehicles.FirstOrDefault(v => v.Id == id);
    }

    public bool UpdateVehicle(int id, UpdateVehicleDto dto)
    {
        var vehicle = GetVehicleById(id);

        if (vehicle is null)
        {
            return false;
        }

        vehicle.Brand = dto.Brand;
        vehicle.Model = dto.Model;
        vehicle.Year = dto.Year;
        vehicle.Mileage = dto.Mileage;
        vehicle.Price = dto.Price;

        _context.SaveChanges();

        return true;
    }

    public bool DeleteVehicle(int id)
    {
        var vehicle = GetVehicleById(id);

        if (vehicle is null)
        {
            return false;
        }

        _context.Vehicles.Remove(vehicle);
        _context.SaveChanges();

        return true;
    }
}