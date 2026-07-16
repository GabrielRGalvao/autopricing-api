using AutoPricing.Api.DTOs;
using AutoPricing.Api.Models;

namespace AutoPricing.Api.Services;

public class VehicleService
{
    private static readonly List<Vehicle> _vehicles = new();

    public void AddVehicle(CreateVehicleDto dto)
    {
        var vehicle = new Vehicle
        {
            Id = _vehicles.Count + 1,
            Brand = dto.Brand,
            Model = dto.Model,
            Year = dto.Year,
            Mileage = dto.Mileage,
            Price = dto.Price
        };

        _vehicles.Add(vehicle);
    }

    public List<Vehicle> GetAllVehicles()
    {
        return _vehicles;
    }
}