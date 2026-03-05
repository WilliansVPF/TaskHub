using FluentValidation;
using TaskHub.Application.DTOs.User;

namespace TaskHub.Application.Validatos.User;

public class EditarUsuarioValidator : AbstractValidator<EditarUsuarioDTO>
{
    public EditarUsuarioValidator()
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
    }
}