using TaskHub.Domain.Enums;

namespace TaskHub.Domain.Entities;

public class Lista
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Status Status { get; set; }

    public int IdTarefa { get; set; }

    public Tarefa Tarefa { get; set; } = null!;
    public IEnumerable<ItemLista> Itens { get; set; } = null!;

    public Lista(string titulo, string descricao, int idTarefa, Status status = Status.Incompleto)
    {
        Titulo = titulo;
        Descricao = descricao;
        IdTarefa = idTarefa;
        Status = status;
    }
}