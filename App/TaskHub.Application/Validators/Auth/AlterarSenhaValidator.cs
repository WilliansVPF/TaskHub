using FluentValidation;
using TaskHub.Application.DTOs.Auth;

namespace TaskHub.Application.Validatos.Auth;

public class AlterarSenhaValidator : AbstractValidator<AlterarSenhaDTO>
{
    public AlterarSenhaValidator()
    {
        RuleFor(a => a.SenhaAtual)
            .NotEmpty().WithMessage("É necessário informar a 'senha atual'.");

        RuleFor(a => a.NovaSenha)
            .NotEmpty().WithMessage("É necessário informar a 'senha'.")
            .Length(8, 16).WithMessage("A 'senha' deve ter de 8 a 16 caracters.")
            .Matches(@"[0-9]").WithMessage("A 'senha' precisa conter ao menos um caracter númerico.")
            .Matches(@"[a-z]").WithMessage("A 'senha' precisa conter ao menos um caracter minúsculo.")
            .Matches(@"[A-Z]").WithMessage("A 'senha' precisa conter ao menos um caracter maiúsculo.")
            .Matches(@"^(?=.*[@$!%*#?&]).*$").WithMessage("A 'senha' precisa conter ao menos um dos caracteres especiais (@ $ ! % * # ? &).")
            .NotEqual(a => a.SenhaAtual).WithMessage("A 'nova senha' deve ser diferente da 'senha atual'.");

        RuleFor(a => a.ConfirmarSenha)
            .NotEmpty().WithMessage("É necessário informar a 'confirmação de senha'.")
            .Equal(a => a.NovaSenha).WithMessage("O campo 'confirmar senha' deve ser igual ao campo 'senha'");
    }
}