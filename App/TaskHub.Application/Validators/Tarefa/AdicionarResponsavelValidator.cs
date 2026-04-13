using FluentValidation;
using TaskHub.Application.DTOs.Tarefa;

namespace TaskHub.Application.Validatos.Tarefa;

public class AdicionarResponsavelValidator : AbstractValidator<AdicionarResponsavelDTO>
{
    public AdicionarResponsavelValidator()
    {
        RuleFor(r => r.ResponsavelId)
            .NotEmpty().WithMessage("O responsável a ser adicionado precisa ser informado.");
    }
}