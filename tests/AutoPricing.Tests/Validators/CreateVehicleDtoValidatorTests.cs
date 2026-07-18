using AutoPricing.Api.DTOs;
using AutoPricing.Api.Validators;
using FluentValidation.TestHelper;

namespace AutoPricing.Tests.Validators;

public class CreateVehicleDtoValidatorTests
{
    private readonly CreateVehicleDtoValidator _validator = new();

    [Fact]
    public void Validate_WhenDtoIsValid_ShouldNotHaveValidationErrors()
    {
        var dto = new CreateVehicleDto
        {
            Brand = "Honda",
            Model = "Civic",
            Year = 2022,
            Mileage = 35000,
            Price = 128900
        };

        var result = _validator.TestValidate(dto);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenBrandIsEmpty_ShouldHaveValidationError()
    {
        var dto = CreateValidDto();
        dto.Brand = string.Empty;

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Brand)
            .WithErrorMessage("A marca é obrigatória.");
    }

    [Fact]
    public void Validate_WhenModelIsEmpty_ShouldHaveValidationError()
    {
        var dto = CreateValidDto();
        dto.Model = string.Empty;

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Model)
            .WithErrorMessage("O modelo é obrigatório.");
    }

    [Fact]
    public void Validate_WhenYearIsInvalid_ShouldHaveValidationError()
    {
        var dto = CreateValidDto();
        dto.Year = 1899;

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Year);
    }

    [Fact]
    public void Validate_WhenMileageIsNegative_ShouldHaveValidationError()
    {
        var dto = CreateValidDto();
        dto.Mileage = -1;

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Mileage)
            .WithErrorMessage("A quilometragem não pode ser negativa.");
    }

    [Fact]
    public void Validate_WhenPriceIsZero_ShouldHaveValidationError()
    {
        var dto = CreateValidDto();
        dto.Price = 0;

        var result = _validator.TestValidate(dto);

        result.ShouldHaveValidationErrorFor(x => x.Price)
            .WithErrorMessage("O preço deve ser maior que zero.");
    }

    private static CreateVehicleDto CreateValidDto()
    {
        return new CreateVehicleDto
        {
            Brand = "Toyota",
            Model = "Corolla",
            Year = 2023,
            Mileage = 20000,
            Price = 120000
        };
    }
}