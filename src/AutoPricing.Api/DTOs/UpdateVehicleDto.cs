namespace AutoPricing.Api.DTOs;

public class UpdateVehicleDto
{
    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }

    public int Mileage { get; set; }

    public decimal Price { get; set; }
}