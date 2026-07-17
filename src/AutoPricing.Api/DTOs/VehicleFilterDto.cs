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

    public string? SortBy { get; set; }

    public bool Descending { get; set; } = false;

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}