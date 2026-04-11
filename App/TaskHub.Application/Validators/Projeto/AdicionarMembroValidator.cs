using FluentValidation;
using TaskHub.Application.DTOs.Projeto;

namespace TaskHub.Application.Validatos.Projeto;

public class AdicionarMembroValidator : AbstractValidator<AdicionarMembroDTO>
{
    public AdicionarMembroValidator()
    {
        RuleFor(m => m.IdUsuario)
            .NotEmpty().WithMessage("O 'id' do usuário precisa ser informado.");

        RuleFor(m => m.Privilegio)
            .NotEmpty().WithMessage("O 'privilégio' do usuário precisa ser informado.")
            .IsInEnum().WithMessage("Privilégio inválido.");
    }
}