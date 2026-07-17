namespace AutoPricing.Api.DTOs;

public class VehicleFilterDto
{
    public string? Brand { get; set; }

    public string? Model { get; set; }

    public int? Year { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int? MinMileage { get; set; }

    public int? MaxMileage { get; set; }
}