using TaskHub.Domain.Enums;

namespace TaskHub.Domain.Entities;

public class ItemLista
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public Status Status { get; set; }

    public int IdLista { get; set; }

    public Lista Lista { get; set; } = null!;

    public ItemLista(string titulo, int idLista, Status status = Status.Incompleto)
    {
        Titulo = titulo;
        IdLista = idLista;
        Status = status;
    }
}