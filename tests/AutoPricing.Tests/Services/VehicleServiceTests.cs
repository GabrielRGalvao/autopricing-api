using AutoPricing.Api.Data;
using AutoPricing.Api.DTOs;
using AutoPricing.Api.Exceptions;
using AutoPricing.Api.Models;
using AutoPricing.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace AutoPricing.Tests.Services;

public class VehicleServiceTests
{
    private static VehicleDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<VehicleDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new VehicleDbContext(options);
    }

    [Fact]
    public async Task AddVehicleAsync_ShouldSaveAndReturnVehicle()
    {
        await using var context = CreateContext();

        var service = new VehicleService(context);

        var dto = new CreateVehicleDto
        {
            Brand = "Honda",
            Model = "Civic",
            Year = 2022,
            Mileage = 35000,
            Price = 128900
        };

        var result = await service.AddVehicleAsync(dto);

        Assert.NotEqual(0, result.Id);
        Assert.Equal("Honda", result.Brand);
        Assert.Equal("Civic", result.Model);
        Assert.Equal(1, await context.Vehicles.CountAsync());
    }

    [Fact]
    public async Task GetVehicleByIdAsync_WhenVehicleDoesNotExist_ShouldThrowNotFoundException()
    {
        await using var context = CreateContext();

        var service = new VehicleService(context);

        await Assert.ThrowsAsync<NotFoundException>(
            () => service.GetVehicleByIdAsync(999));
    }

    [Fact]
    public async Task GetVehiclesAsync_ShouldReturnCorrectPagination()
    {
        await using var context = CreateContext();

        for (int i = 1; i <= 6; i++)
        {
            context.Vehicles.Add(new Vehicle
            {
                Brand = $"Marca {i}",
                Model = $"Modelo {i}",
                Year = 2020 + i,
                Mileage = i * 10000,
                Price = i * 10000
            });
        }

        await context.SaveChangesAsync();

        var service = new VehicleService(context);

        var result = await service.GetVehiclesAsync(new VehicleFilterDto
        {
            Page = 2,
            PageSize = 2,
            SortBy = "price"
        });

        Assert.Equal(2, result.Page);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(6, result.TotalItems);
        Assert.Equal(3, result.TotalPages);
        Assert.Equal(2, result.Items.Count);
    }
}