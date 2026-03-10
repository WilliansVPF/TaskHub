using FluentValidation;
using NamespaceName;

namespace TaskHub.Application.Validatos.Tarefa;

public class EditarTarefaValidator : AbstractValidator<EditarTarefaDTO>
{
    public EditarTarefaValidator()
    {
        RuleFor(t => t.Titulo)
            .NotEmpty().WithMessage("É necessário informau um 'título'.")
            .Length(5, 45).WithMessage("O campo 'título' deve conter de 5 a 45 caracteres.");

        RuleFor(t => t.Descricao)
            .MaximumLength(100).WithMessage("o campo 'descrição' deve conter no maximo 100 caracteres.");

        RuleFor(t => t.DataFim)
            .GreaterThanOrEqualTo(DateTime.Today).When(t => t.DataFim.HasValue)
            .WithMessage("A data final não pode ser anterior à data atual.")
            .GreaterThanOrEqualTo(t => t.DataInicio).When(t => t.DataFim.HasValue)
            .WithMessage("A data final não pode ser anterior à data inicial.");

        RuleFor(t => t.Status)
            .NotEmpty().WithMessage("É necessário informar o 'status'");
    }
}