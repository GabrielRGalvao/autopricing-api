using AutoPricing.Api.DTOs;
using AutoPricing.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoPricing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly VehicleService _vehicleService;

    public VehicleController()
    {
        _vehicleService = new VehicleService();
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
}