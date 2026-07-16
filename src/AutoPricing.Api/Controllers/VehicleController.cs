using AutoPricing.Api.DTOs;
using AutoPricing.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoPricing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly VehicleService _vehicleService;

    public VehicleController(VehicleService vehicleService)
{
    _vehicleService = vehicleService;
}

    [HttpPost]
    public IActionResult CreateVehicle(CreateVehicleDto dto)
    {
        _vehicleService.AddVehicle(dto);

        return Ok(new
        {
            message = "Vehicle created successfully."
        });
    }

    [HttpGet]
    public IActionResult GetVehicles()
    {
        var vehicles = _vehicleService.GetAllVehicles();

        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public IActionResult GetVehicleById(int id)
    {
        var vehicle = _vehicleService.GetVehicleById(id);

        if (vehicle is null)
        {
            return NotFound(new
            {
                message = $"Vehicle with id {id} not found."
            });
        }

        return Ok(vehicle);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateVehicle(int id, UpdateVehicleDto dto)
    {
        var updated = _vehicleService.UpdateVehicle(id, dto);

        if (!updated)
        {
            return NotFound(new
            {
                message = $"Vehicle with id {id} not found."
            });
        }

        return Ok(new
        {
            message = "Vehicle updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteVehicle(int id)
    {
        var deleted = _vehicleService.DeleteVehicle(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = $"Vehicle with id {id} not found."
            });
        }

        return Ok(new
        {
            message = "Vehicle deleted successfully."
        });
    }
}