using FluentValidation;
using TaskHub.Application.DTOs.Tarefa;

namespace TaskHub.Application.Validatos.Tarefa;

public class CadastraTarefaValidator : AbstractValidator<CadastrarTarefaDTO>
{
    public CadastraTarefaValidator()
    {
        RuleFor(t => t.Titulo)
            .NotEmpty().WithMessage("É necessário informau um 'título'.")
            .Length(5, 45).WithMessage("O campo 'título' deve conter de 5 a 45 caracteres.");

        RuleFor(t => t.Descricao)
            .MaximumLength(100).WithMessage("o campo 'descrição' deve conter no maximo 100 caracteres.");

        RuleFor(t => t.DataFim)
            .GreaterThanOrEqualTo(DateTime.Today).When(t => t.DataFim.HasValue)
            .WithMessage("A data final não pode ser anterior à data atual.");        
    }
}