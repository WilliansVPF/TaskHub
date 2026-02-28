using TaskHub.Application.DTOs.User;
using TaskHub.Domain.Entities;

namespace TaskHub.Application.Mappers;

public class UsuarioMapper
{
    public ApplicationUser RegistrarUsuarioDTOToApplicationUser (RegistrarUsuarioDTO dados)
    {
        var user = new ApplicationUser();

        user.Nome = dados.Nome;
        user.Sobrenome = dados.Sobrenome;
        user.UserName = dados.UserName;
        user.Email = dados.Email;

        return user;
    }

    public DetalheUsuarioDTO ApplicationUserToDetalheUsuarioDTO (ApplicationUser dados)
    {
        var user = new DetalheUsuarioDTO(dados.Id, dados.Nome, dados.Sobrenome, dados.UserName!, dados.Email!);

        return user;
    }

    // public ApplicationUser EditarUsuarioDTOToApplicatioUser(EditarUsuarioDTO dados)
    // {
    //     var user = new ApplicationUser();

    //     user.Id = 
    // }
}