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
    public async Task<IActionResult> Create(CreateVehicleDto dto)
    {
        var createdVehicle = await _vehicleService
        .AddVehicleAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdVehicle.Id },
            createdVehicle);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] VehicleFilterDto filter)
    {
        var vehicles = await _vehicleService
            .GetVehiclesAsync(filter);

        return Ok(vehicles);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(int id)
    {
        var vehicle = await _vehicleService
            .GetVehicleByIdAsync(id);

        return Ok(vehicle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    UpdateVehicleDto dto)
    {
        await _vehicleService
            .UpdateVehicleAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _vehicleService
            .DeleteVehicleAsync(id);

        return NoContent();
    }
}