namespace TaskHub.Domain.Entities;

public class Projeto
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }

    public string IdUsuario { get; set; }

    public ApplicationUser Usuario { get; set; } = null!;
    public IEnumerable<Tarefa> Tarefas { get; set; } = null!;
    public IEnumerable<MembroProjeto> MembroProjetos { get; set; } = null!;

    public Projeto(string titulo, string descricao, string idUsuario)
    {
        Titulo = titulo;
        Descricao = descricao;
        IdUsuario = idUsuario;
    }

}