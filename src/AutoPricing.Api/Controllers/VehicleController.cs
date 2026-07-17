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
    public IActionResult Create(CreateVehicleDto dto)
    {
        _vehicleService.AddVehicle(dto);

        return Ok(new
        {
            message = "Veículo criado com sucesso."
        });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var vehicles = _vehicleService.GetAllVehicles();

        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var vehicle = _vehicleService.GetVehicleById(id);

        if (vehicle is null)
        {
            return NotFound(new
            {
                message = $"Veículo com ID {id} não encontrado."
            });
        }

        return Ok(vehicle);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateVehicleDto dto)
    {
        var updated = _vehicleService.UpdateVehicle(id, dto);

        if (!updated)
        {
            return NotFound(new
            {
                message = $"Veículo com ID {id} não encontrado."
            });
        }

        return Ok(new
        {
            message = "Veículo atualizado com sucesso."
        });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _vehicleService.DeleteVehicle(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = $"Veículo com ID {id} não encontrado."
            });
        }

        return Ok(new
        {
            message = "Veículo removido com sucesso."
        });
    }
}