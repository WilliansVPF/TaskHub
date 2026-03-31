using FluentValidation;
using TaskHub.Application.DTOs.Projeto;

namespace TaskHub.Application.Validatos.Projeto;

public class CriarProjetoValidator : AbstractValidator<CriarProjetoDTO>
{
    public CriarProjetoValidator()
    {
        RuleFor(p => p.Titulo)
            .NotEmpty().WithMessage("É necessário informar um 'título'.")
            .Length(5, 45).WithMessage("O campo 'título' deve conter de 5 a 45 caracteres.");

        RuleFor(p => p.Descricao)
            .MaximumLength(100).WithMessage("o campo 'descrição' deve conter no maximo 100 caracteres.");

    }
}