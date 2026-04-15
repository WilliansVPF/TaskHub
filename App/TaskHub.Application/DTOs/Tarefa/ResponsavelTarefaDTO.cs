namespace TaskHub.Application.DTOs.Tarefa;

public class ResponsavelTarefaDTO
{
    public string Id { get; set; }
    public string Nome { get; set; }

    public ResponsavelTarefaDTO(string id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}