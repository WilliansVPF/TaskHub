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
    private readonly IValidator<EditarUsuarioDTO> _editarUsuarioValidator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UsuarioMapper _usuarioMapper;

    public UsuarioService(IValidator<RegistrarUsuarioDTO> registraUsuarioValidator, UserManager<ApplicationUser> userManager, UsuarioMapper usuarioMapper, IValidator<EditarUsuarioDTO> editarUsuarioValidator)
    {
        _registraUsuarioValidator = registraUsuarioValidator;
        _userManager = userManager;
        _usuarioMapper = usuarioMapper;
        _editarUsuarioValidator = editarUsuarioValidator;
    }

    public async Task<DetalheUsuarioDTO> RegistrarUsuarioAsync(RegistrarUsuarioDTO dados)
    {
        _registraUsuarioValidator.ValidateAndThrow(dados);

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == dados.Email);
        if (user is not null) throw new DataConflictException("E-mail já cadastrado na base de dados");

        user = _usuarioMapper.RegistrarUsuarioDTOToApplicationUser(dados);

        var identityResult = await _userManager.CreateAsync(user, dados.Senha);
        if (!identityResult.Succeeded) throw new IdentityCreationException(identityResult.Errors);

        var detalheUsuario = _usuarioMapper.ApplicationUserToDetalheUsuarioDTO(user);

        return detalheUsuario;
    }

    public async Task<DetalheUsuarioDTO> DetalheUsuarioAsync(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user is null) throw new ResourceNotFoundException("Usuário não encontrado na base de dados");

        var detalheUsuario = _usuarioMapper.ApplicationUserToDetalheUsuarioDTO(user);

        return detalheUsuario;
    }

    public async Task<DetalheUsuarioDTO> EditarUsuarioAsync(string id, EditarUsuarioDTO dados)
    {
        _editarUsuarioValidator.ValidateAndThrow(dados);

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == dados.Email && u.Id != id);
        if (user is not null) throw new DataConflictException("E-mail já cadastrado na base de dados");
        user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user is null) throw new ResourceNotFoundException("Usuário não encontrado na base de dados");

        user.Nome = dados.Nome;
        user.Sobrenome = dados.Sobrenome;
        user.UserName = dados.UserName;
        user.Email = dados.Email;

        var identityResult = await _userManager.UpdateAsync(user);
        if (!identityResult.Succeeded) throw new IdentityCreationException(identityResult.Errors);

        var detalheUsuario = _usuarioMapper.ApplicationUserToDetalheUsuarioDTO(user);

        return detalheUsuario;
    }

    public async Task DesabilitaUsuarioAsync(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) throw new ResourceNotFoundException("Usuário não encontrado na base de dados");

        user.Ativo = false;

        var identityResult = await _userManager.UpdateAsync(user);
        if (!identityResult.Succeeded) throw new IdentityCreationException(identityResult.Errors);
    }

    public async Task HabilitaUsuarioAsync(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) throw new ResourceNotFoundException("Usuário não encontrado na base de dados");

        user.Ativo = true;

        var identityResult = await _userManager.UpdateAsync(user);
        if (!identityResult.Succeeded) throw new IdentityCreationException(identityResult.Errors);
    }

}