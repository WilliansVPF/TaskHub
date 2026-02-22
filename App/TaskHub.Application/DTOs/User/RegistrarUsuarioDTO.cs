namespace TaskHub.Application.DTOs.User;

public class RegistrarUsuarioDTO
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string ConfirmarSenha { get; set; }

    public RegistrarUsuarioDTO(string nome, string sobrenome, string userName, string email, string senha, string confirmarSenha)
    {
        Nome = nome;
        Sobrenome = sobrenome;
        UserName = userName;
        Email = email;
        Senha = senha;
        ConfirmarSenha = confirmarSenha;
    }
}