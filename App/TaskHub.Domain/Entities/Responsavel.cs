namespace TaskHub.Domain.Entities;

public class Responsavel
{
    public string IdUsuario { get; set; }
    public int IdTarefa { get; set; }

    public ApplicationUser Usuario { get; set; } = null!;
    public Tarefa Tarefa { get; set; } = null!;

    public Responsavel(string idUsuario, int idTarefa)
    {
        IdUsuario = idUsuario;
        IdTarefa = idTarefa;
    }
}