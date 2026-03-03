namespace TaskHub.Application.DTOs.Auth;

public class LoginDTO
{
    public string UserName { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}