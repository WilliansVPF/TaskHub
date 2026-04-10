using TaskHub.Domain.Enums;

namespace TaskHub.Application.DTOs.Projeto;

public class AdicionarMembroDTO
{
    public string IdUsuario { get; set; } = string.Empty;
    public Privilegio Privilegio { get; set; }
} 