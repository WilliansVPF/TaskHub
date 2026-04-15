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
        if (dados.IdProjeto == 0) dados.IdProjeto = null;

        var tarefa = new Tarefa(dados.Titulo, dados.Descricao, dataInicio, dados.DataFim, userId, dados.IdProjeto, status);

        return tarefa;
    }

    public DetalheTarefaDTO TarefaToDetalheTarefaDTO(Tarefa dados)
    {
        var responsaveis = dados.Responsaveis.Select(r => new ResponsavelTarefaDTO(r.IdUsuario, r.Usuario.Nome));
        var detalheTarefa = new DetalheTarefaDTO(dados.Id, dados.Titulo, dados.Descricao, dados.DataInicio, dados.DataFim, dados.Status, dados.IdUsuario, dados.IdProjeto, responsaveis);
        return detalheTarefa;
    }

    public IEnumerable<ResumoTarefaDTO> TarefaToResumoTarefaDTO(IEnumerable<Tarefa> dados)
    {
        var listaTarefa = new List<ResumoTarefaDTO>();
        foreach (var tarefa in dados)
        {
            var resumoTarefa = new ResumoTarefaDTO(tarefa.Id, tarefa.Titulo, tarefa.Status, tarefa.IdProjeto);
            listaTarefa.Add(resumoTarefa);
        }
        return listaTarefa;
    }
}