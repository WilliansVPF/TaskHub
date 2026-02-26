using FluentValidation;
using Microsoft.AspNetCore.Identity;
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

    public async Task<DetalheUsuarioDTO> RegistrarUsuario(RegistrarUsuarioDTO dados)
    {
        _registraUsuarioValidator.ValidateAndThrow(dados);

        var user = _usuarioMapper.RegistrarUsuarioDTOToApplicationUser(dados);

        var identityResult = await _userManager.CreateAsync(user, dados.Senha);
        if (!identityResult.Succeeded) throw new IdentityCreationException(identityResult.Errors);

        var detalheUsuario = _usuarioMapper.ApplicationUserToDetalheUsuarioDTO(user);

        return detalheUsuario;
    }
}