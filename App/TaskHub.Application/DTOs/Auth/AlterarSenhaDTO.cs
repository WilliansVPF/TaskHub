namespace TaskHub.Application.DTOs.Auth;

public class AlterarSenhaDTO
{
    public string SenhaAtual { get; set; } = string.Empty;
    public string NovaSenha { get; set; } = string.Empty;
    public string ConfirmarSenha { get; set; } = string.Empty;
}