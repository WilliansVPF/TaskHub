using TaskHub.Domain.Enums;

namespace TaskHub.Application.DTOs.Tarefa;

public class EditarTarefaDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public Status Status { get; set; }

    public EditarTarefaDTO(int id, string titulo, string descricao, DateTime? dataInicio, DateTime? dataFim, Status status)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Status = status;
    }
}