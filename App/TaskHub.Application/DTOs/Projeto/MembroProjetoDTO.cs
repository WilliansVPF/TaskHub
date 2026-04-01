using TaskHub.Domain.Enums;

namespace TaskHub.Application.DTOs.Projeto;

public class MembroProjetoDTO
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public Privilegio Privilegio{ get; set; } 
}