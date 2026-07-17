using AutoPricing.Api.DTOs;
using FluentValidation;

namespace AutoPricing.Api.Validators;

public class CreateVehicleDtoValidator : AbstractValidator<CreateVehicleDto>
{
    public CreateVehicleDtoValidator()
    {
        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("A marca é obrigatória.")
            .MaximumLength(50)
            .WithMessage("A marca deve ter no máximo 50 caracteres.");

        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage("O modelo é obrigatório.")
            .MaximumLength(80)
            .WithMessage("O modelo deve ter no máximo 80 caracteres.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.Now.Year + 1)
            .WithMessage($"O ano deve estar entre 1900 e {DateTime.Now.Year + 1}.");

        RuleFor(x => x.Mileage)
            .GreaterThanOrEqualTo(0)
            .WithMessage("A quilometragem não pode ser negativa.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("O preço deve ser maior que zero.");
    }
}