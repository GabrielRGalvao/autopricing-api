using AutoPricing.Api.DTOs;
using AutoPricing.Api.Validators;
using FluentValidation.TestHelper;

namespace AutoPricing.Tests.Validators;

public class VehicleFilterDtoValidatorTests
{
    private readonly VehicleFilterDtoValidator _validator = new();

    [Fact]
    public void Validate_WhenFilterIsValid_ShouldNotHaveValidationErrors()
    {
        var filter = new VehicleFilterDto
        {
            Brand = "Honda",
            Model = "Civic",
            Year = 2022,
            MinPrice = 80000,
            MaxPrice = 150000,
            MinMileage = 10000,
            MaxMileage = 50000,
            SortBy = "price",
            Page = 1,
            PageSize = 10
        };

        var result = _validator.TestValidate(filter);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WhenMinPriceIsGreaterThanMaxPrice_ShouldHaveValidationError()
    {
        var filter = new VehicleFilterDto
        {
            MinPrice = 150000,
            MaxPrice = 100000
        };

        var result = _validator.TestValidate(filter);

        Assert.Contains(
            result.Errors,
            error => error.ErrorMessage ==
                "O preço mínimo não pode ser maior que o preço máximo.");
    }

    [Fact]
    public void Validate_WhenMinMileageIsGreaterThanMaxMileage_ShouldHaveValidationError()
    {
        var filter = new VehicleFilterDto
        {
            MinMileage = 50000,
            MaxMileage = 10000
        };

        var result = _validator.TestValidate(filter);

        Assert.Contains(
            result.Errors,
            error => error.ErrorMessage ==
                "A quilometragem mínima não pode ser maior que a quilometragem máxima.");
    }

    [Fact]
    public void Validate_WhenSortByIsInvalid_ShouldHaveValidationError()
    {
        var filter = new VehicleFilterDto
        {
            SortBy = "createdAt"
        };

        var result = _validator.TestValidate(filter);

        result.ShouldHaveValidationErrorFor(x => x.SortBy)
            .WithErrorMessage(
                "O campo de ordenação deve ser brand, model, year, price ou mileage.");
    }

    [Fact]
    public void Validate_WhenPageIsLessThanOne_ShouldHaveValidationError()
    {
        var filter = new VehicleFilterDto
        {
            Page = 0
        };

        var result = _validator.TestValidate(filter);

        result.ShouldHaveValidationErrorFor(x => x.Page)
            .WithErrorMessage("A página deve ser maior ou igual a 1.");
    }

    [Fact]
    public void Validate_WhenPageSizeIsGreaterThanFifty_ShouldHaveValidationError()
    {
        var filter = new VehicleFilterDto
        {
            PageSize = 51
        };

        var result = _validator.TestValidate(filter);

        result.ShouldHaveValidationErrorFor(x => x.PageSize)
            .WithErrorMessage(
                "A quantidade de itens por página deve estar entre 1 e 50.");
    }

    [Fact]
    public void Validate_WhenOptionalFiltersAreNull_ShouldNotHaveValidationErrors()
    {
        var filter = new VehicleFilterDto();

        var result = _validator.TestValidate(filter);

        result.ShouldNotHaveAnyValidationErrors();
    }
}