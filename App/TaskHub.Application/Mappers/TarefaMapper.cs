using TaskHub.Application.DTOs.Tarefa;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;

namespace TaskHub.Application.Mappers;

public class TarefaMapper
{
    public Tarefa CadastraTarefaDTOToTarefa(string userId, CadastrarTarefaDTO dados)
    {
        DateTime? dataInicio = (dados.InicioImediato == true) ? DateTime.Today : null;
        Status status = (dados.InicioImediato) ? Status.Execução : Status.Incompleto;

        var tarefa = new Tarefa(dados.Titulo, dados.Descricao, dataInicio, dados.DataFim, userId, dados.IdProjeto, status);

        return tarefa;
    }

    public DetalheTarefaDTO TarefaToDetalheTarefaDTO(Tarefa dados)
    {
        var detalheTarefa = new DetalheTarefaDTO(dados.Id, dados.Titulo, dados.Descricao, dados.DataInicio, dados.DataFim, dados.Status, dados.IdUsuario, dados.IdProjeto);
        return detalheTarefa;
    }

    public IEnumerable<ResumoTarefaDTO> TarefaToResumoTarefaDTO(IEnumerable<Tarefa> dados)
    {
        var listaTarefa = new List<ResumoTarefaDTO>();
        foreach (var tarefa in dados)
        {
            var resumoTarefa = new ResumoTarefaDTO(tarefa.Id, tarefa.Titulo, tarefa.Status);
            listaTarefa.Add(resumoTarefa);
        }
        return listaTarefa;
    }
}