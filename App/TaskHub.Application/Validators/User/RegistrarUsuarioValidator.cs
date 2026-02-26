using FluentValidation;
using TaskHub.Application.DTOs.User;

namespace TaskHub.Application.Validatos.User;

public class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioDTO>
{
    public RegistrarUsuarioValidator()
    {
        RuleFor(u => u.Nome)
            .NotEmpty().WithMessage("É necessário informar um 'nome'.")
            .Length(3, 45).WithMessage("O campo 'nome' deve conter de 3 a 45 caracteres.");

        RuleFor(u => u.Sobrenome)
            .NotEmpty().WithMessage("É necessário informar um 'sobrenome'.")
            .Length(3, 45).WithMessage("O campo 'sobrenome' deve conter de 3 a 45 caracteres.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("É necessário informar o 'e-mail'.")
            .EmailAddress().WithMessage("Formato de 'e-mail' inválido.");
            // .MustAsync(async (email, cancellation) =>
            // {
            //     var existe = await _userManager.Users.AnyAsync(u => u.Email == email);
            //     return !existe;
            // }).WithMessage("'E-mail' já cadastrado.");

        RuleFor(u => u.Senha)
            .NotEmpty().WithMessage("É necessário informaar a 'senha'.")
            .Length(8, 16).WithMessage("A 'senha' deve ter de 8 a 16 caracters.")
            .Matches(@"[0-9]").WithMessage("A 'senha' precisa conter ao menos um caracter númerico.")
            .Matches(@"[a-z]").WithMessage("A 'senha' precisa conter ao menos um caracter minúsculo.")
            .Matches(@"[A-Z]").WithMessage("A 'senha' precisa conter ao menos um caracter maiúsculo.")
            .Matches(@"^(?=.*[@$!%*#?&]).*$").WithMessage("A 'senha' precisa conter ao menos um dos caracteres especiais (@ $ ! % * # ? &).");

        RuleFor(u => u.ConfirmarSenha)
            .NotEmpty().WithMessage("É necessário informar a 'confirmação de senha'.")
            .Equal(u => u.Senha).WithMessage("O campo 'confirmar senha' deve ser igual ao campo 'senha'");
    }
}