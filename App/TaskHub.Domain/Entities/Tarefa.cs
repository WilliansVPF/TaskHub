using TaskHub.Domain.Enums;

namespace TaskHub.Domain.Entities;

public class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public Status Status { get; set; }

    public string IdUsuario { get; set; }
    public int? IdProjeto { get; set; }

    public IEnumerable<Responsavel> Responsaveis { get; set; } = null!;
    public ApplicationUser Usuario { get; set; } = null!;
    public IEnumerable<Lista> Listas { get; set; } = null!;
    public Projeto Projeto { get; set; } = null!;

    public Tarefa(string titulo, string descricao, DateTime? dataInicio, DateTime? dataFim, string idUsuario, int? idProjeto, Status status = Status.Incompleto)
    {
        Titulo = titulo;
        Descricao = descricao;
        DataInicio = dataInicio;
        DataFim = dataFim;
        IdUsuario = idUsuario;
        IdProjeto = idProjeto;
        Status = status;
    }
}