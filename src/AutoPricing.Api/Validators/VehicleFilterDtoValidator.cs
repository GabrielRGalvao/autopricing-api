using AutoPricing.Api.DTOs;
using FluentValidation;

namespace AutoPricing.Api.Validators;

public class VehicleFilterDtoValidator : AbstractValidator<VehicleFilterDto>
{
    public VehicleFilterDtoValidator()
    {
        RuleFor(x => x.Brand)
            .MaximumLength(50)
            .WithMessage("A marca deve ter no máximo 50 caracteres.");

        RuleFor(x => x.Model)
            .MaximumLength(80)
            .WithMessage("O modelo deve ter no máximo 80 caracteres.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.Now.Year + 1)
            .When(x => x.Year.HasValue)
            .WithMessage(
                $"O ano deve estar entre 1900 e {DateTime.Now.Year + 1}.");

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinPrice.HasValue)
            .WithMessage("O preço mínimo não pode ser negativo.");

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxPrice.HasValue)
            .WithMessage("O preço máximo não pode ser negativo.");

        RuleFor(x => x.MinMileage)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinMileage.HasValue)
            .WithMessage("A quilometragem mínima não pode ser negativa.");

        RuleFor(x => x.MaxMileage)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MaxMileage.HasValue)
            .WithMessage("A quilometragem máxima não pode ser negativa.");

        RuleFor(x => x)
            .Must(filter =>
                !filter.MinPrice.HasValue ||
                !filter.MaxPrice.HasValue ||
                filter.MinPrice <= filter.MaxPrice)
            .WithMessage(
                "O preço mínimo não pode ser maior que o preço máximo.");

        RuleFor(x => x)
            .Must(filter =>
                !filter.MinMileage.HasValue ||
                !filter.MaxMileage.HasValue ||
                filter.MinMileage <= filter.MaxMileage)
            .WithMessage(
                "A quilometragem mínima não pode ser maior que a quilometragem máxima.");
    }
}