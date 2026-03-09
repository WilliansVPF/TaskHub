using TaskHub.Domain.Enums;

namespace TaskHub.Application.DTOs.Tarefa;

public class CadastrarTarefaDTO
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public bool InicioImediato { get; set; }
    public DateTime? DataFim { get; set; }
    public int? IdProjeto { get; set; }

    public CadastrarTarefaDTO(string titulo, string descricao, DateTime? dataFim, int? idProjeto, bool inicioImediato = false)
    {
        Titulo = titulo;
        Descricao = descricao;
        InicioImediato = inicioImediato;
        DataFim = dataFim;
        IdProjeto = idProjeto;
    }
}