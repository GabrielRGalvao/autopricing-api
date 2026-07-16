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

    public Vehicle? GetVehicleById(int id)
    {
        return _vehicles.FirstOrDefault(vehicle => vehicle.Id == id);
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

        return true;
    }

    public bool DeleteVehicle(int id)
    {
        var vehicle = GetVehicleById(id);

        if (vehicle is null)
        {
            return false;
        }

        _vehicles.Remove(vehicle);

        return true;
    }
}