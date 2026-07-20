using AutoPricing.Api.DTOs;
using FluentValidation;

namespace AutoPricing.Api.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .MaximumLength(100)
            .WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório.")
            .EmailAddress()
            .WithMessage("O e-mail informado é inválido.")
            .MaximumLength(150)
            .WithMessage("O e-mail deve ter no máximo 150 caracteres.");

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("A senha é obrigatória.")
            .MinimumLength(6)
            .WithMessage("A senha deve ter pelo menos 6 caracteres.")
            .MaximumLength(100)
            .WithMessage("A senha deve ter no máximo 100 caracteres.");
    }
}