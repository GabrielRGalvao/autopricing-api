namespace AutoPricing.Api.DTOs;

public class VehicleResponseDto
{
    public int Id { get; set; }

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }

    public int Mileage { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }
}