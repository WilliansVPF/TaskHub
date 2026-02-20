using TaskHub.Domain.Enums;

namespace TaskHub.Domain.Entities;

public class MembroProjeto
{
    public int Id { get; set; }
    public Privilegio Privilegio { get; set; }
    public int IdProjeto { get; set; }
    public string IdUsuario { get; set; }

    public ApplicationUser Usuario { get; set; } = null!;
    public Projeto Projeto { get; set; } = null!;

    public MembroProjeto(int idProjeto, string idUsuario, Privilegio privilegio)
    {
        IdProjeto = idProjeto;
        IdUsuario = idUsuario;
        Privilegio = privilegio;
    }
}