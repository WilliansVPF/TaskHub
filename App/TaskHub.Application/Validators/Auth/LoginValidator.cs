using FluentValidation;
using TaskHub.Application.DTOs.Auth;

namespace TaskHub.Application.Validatos.Auth;

public class LoginValidator : AbstractValidator<LoginDTO>
{
    public LoginValidator()
    {
        RuleFor(l => l.UserName)
            .NotEmpty().WithMessage("O 'username' deve ser informado.");

        RuleFor(l => l.Senha)
            .NotEmpty().WithMessage("A 'senha' deve ser informada.");
    }
}