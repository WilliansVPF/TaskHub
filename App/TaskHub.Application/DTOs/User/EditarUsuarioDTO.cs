namespace TaskHub.Application.DTOs.User;

public class EditarUsuarioDTO
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public EditarUsuarioDTO(string nome, string sobrenome, string userName, string email)
    {
        Nome = nome;
        Sobrenome = sobrenome;
        UserName = userName;
        Email = email;
    }
}