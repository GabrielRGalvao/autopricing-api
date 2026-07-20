using AutoPricing.Api.DTOs;
using FluentValidation;

namespace AutoPricing.Api.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório.")
            .EmailAddress()
            .WithMessage("O e-mail informado é inválido.");

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("A senha é obrigatória.");
    }
}