using TaskHub.Domain.Enums;

namespace TaskHub.Application.DTOs.Tarefa;

public class ResumoTarefaDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public Status Status { get; set; }

    public ResumoTarefaDTO(int id, string titulo, Status status)
    {
        Id = id;
        Titulo = titulo;
        Status = status;
    }
}