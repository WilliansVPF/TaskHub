namespace TaskHub.Application.DTOs.User;

public class EditarUsuarioDTO
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public EditarUsuarioDTO(string id, string nome, string sobrenome, string userName, string email)
    {
        Id = id;
        Nome = nome;
        Sobrenome = sobrenome;
        UserName = userName;
        Email = email;
    }
}