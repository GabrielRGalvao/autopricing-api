using AutoPricing.Api.Data;
using AutoPricing.Api.DTOs;
using AutoPricing.Api.Models;
using AutoPricing.Api.Responses;
using Microsoft.EntityFrameworkCore;
using AutoPricing.Api.Exceptions;

namespace AutoPricing.Api.Services;

public class VehicleService
{
    private readonly VehicleDbContext _context;

    public VehicleService(VehicleDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle> AddVehicleAsync(CreateVehicleDto dto)
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

        await _context.SaveChangesAsync();

        return vehicle;
    }

    public async Task<Vehicle> GetVehicleByIdAsync(int id)
    {
        var vehicle = await _context.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(vehicle => vehicle.Id == id);

        if (vehicle is null)
        {
            throw new NotFoundException(
                "Veículo não encontrado.");
        }

        return vehicle;
    }

    public async Task UpdateVehicleAsync(
    int id,
    UpdateVehicleDto dto)
    {
        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(vehicle => vehicle.Id == id);

        if (vehicle is null)
        {
            throw new NotFoundException(
                "Veículo não encontrado.");
        }

        vehicle.Brand = dto.Brand;
        vehicle.Model = dto.Model;
        vehicle.Year = dto.Year;
        vehicle.Mileage = dto.Mileage;
        vehicle.Price = dto.Price;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteVehicleAsync(int id)
    {
        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(vehicle => vehicle.Id == id);

        if (vehicle is null)
        {
            throw new NotFoundException(
                "Veículo não encontrado.");
        }

        _context.Vehicles.Remove(vehicle);

        await _context.SaveChangesAsync();
    }

    public async Task<PagedResponse<Vehicle>> GetVehiclesAsync(
        VehicleFilterDto filter)
    {
        var query = _context.Vehicles
            .AsNoTracking()
            .AsQueryable();

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

                _ => query.OrderBy(vehicle => vehicle.Id)
            };
        }
        else
        {
            query = query.OrderBy(vehicle => vehicle.Id);
        }

        var totalItems = await query.CountAsync();

        var totalPages = (int)Math.Ceiling(
            totalItems / (double)filter.PageSize);

        var items = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

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