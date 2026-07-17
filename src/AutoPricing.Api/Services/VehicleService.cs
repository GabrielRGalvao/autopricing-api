using AutoPricing.Api.DTOs;
using AutoPricing.Api.Models;
using AutoPricing.Api.Data;
using AutoPricing.Api.Responses;

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
    public PagedResponse<Vehicle> GetVehicles(VehicleFilterDto filter)
    {
        var query = _context.Vehicles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Brand))
        {
            query = query.Where(vehicle =>
                vehicle.Brand.Contains(filter.Brand));
        }

        if (!string.IsNullOrWhiteSpace(filter.Model))
        {
            query = query.Where(vehicle =>
                vehicle.Model.Contains(filter.Model));
        }

        if (filter.Year.HasValue)
        {
            query = query.Where(vehicle =>
                vehicle.Year == filter.Year.Value);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(vehicle =>
                vehicle.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(vehicle =>
                vehicle.Price <= filter.MaxPrice.Value);
        }

        if (filter.MinMileage.HasValue)
        {
            query = query.Where(vehicle =>
                vehicle.Mileage >= filter.MinMileage.Value);
        }

        if (filter.MaxMileage.HasValue)
        {
            query = query.Where(vehicle =>
                vehicle.Mileage <= filter.MaxMileage.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.SortBy))
        {
            query = filter.SortBy.ToLower() switch
            {
                "brand" => filter.Descending
                    ? query.OrderByDescending(vehicle => vehicle.Brand)
                    : query.OrderBy(vehicle => vehicle.Brand),

                "model" => filter.Descending
                    ? query.OrderByDescending(vehicle => vehicle.Model)
                    : query.OrderBy(vehicle => vehicle.Model),

                "year" => filter.Descending
                    ? query.OrderByDescending(vehicle => vehicle.Year)
                    : query.OrderBy(vehicle => vehicle.Year),

                "price" => filter.Descending
                    ? query.OrderByDescending(vehicle => vehicle.Price)
                    : query.OrderBy(vehicle => vehicle.Price),

                "mileage" => filter.Descending
                    ? query.OrderByDescending(vehicle => vehicle.Mileage)
                    : query.OrderBy(vehicle => vehicle.Mileage),

                _ => query
            };
        }

        var totalItems = query.Count();

        var totalPages = (int)Math.Ceiling(
            totalItems / (double)filter.PageSize);

        var items = query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        return new PagedResponse<Vehicle>
        {
            Items = items,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
    }


}