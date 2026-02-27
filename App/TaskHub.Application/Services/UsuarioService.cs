using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskHub.Application.DTOs.User;
using TaskHub.Application.Exceptions;
using TaskHub.Application.Mappers;
using TaskHub.Domain.Entities;

namespace TaskHub.Application.Services;

public class UsuarioService
{
    private readonly IValidator<RegistrarUsuarioDTO> _registraUsuarioValidator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UsuarioMapper _usuarioMapper;

    public UsuarioService(IValidator<RegistrarUsuarioDTO> registraUsuarioValidator, UserManager<ApplicationUser> userManager, UsuarioMapper usuarioMapper)
    {
        _registraUsuarioValidator = registraUsuarioValidator;
        _userManager = userManager;
        _usuarioMapper = usuarioMapper;
    }

    public async Task<DetalheUsuarioDTO> RegistrarUsuarioAsync(RegistrarUsuarioDTO dados)
    {
        _registraUsuarioValidator.ValidateAndThrow(dados);

        var user = _usuarioMapper.RegistrarUsuarioDTOToApplicationUser(dados);

        var identityResult = await _userManager.CreateAsync(user, dados.Senha);
        if (!identityResult.Succeeded) throw new IdentityCreationException(identityResult.Errors);

        var detalheUsuario = _usuarioMapper.ApplicationUserToDetalheUsuarioDTO(user);

        return detalheUsuario;
    }

    public async Task<DetalheUsuarioDTO> DetalheUsuarioAsync(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user is null) throw new EntityNotFoundException("Usuário não encontrado na base de dados");

        var detalheUsuario = _usuarioMapper.ApplicationUserToDetalheUsuarioDTO(user);

        return detalheUsuario;
    }

}