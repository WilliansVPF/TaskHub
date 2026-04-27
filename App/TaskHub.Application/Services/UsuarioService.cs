using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskHub.Application.DTOs.User;
using TaskHub.Application.Mappers;
using TaskHub.Domain.Common;
using TaskHub.Domain.DomainServices;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;

namespace TaskHub.Application.Services;

public class UsuarioService
{
    private readonly IValidator<RegistrarUsuarioDTO> _registraUsuarioValidator;
    private readonly IValidator<EditarUsuarioDTO> _editarUsuarioValidator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UsuarioMapper _usuarioMapper;
    private readonly UserDomainService _userDomainService;

    public UsuarioService(IValidator<RegistrarUsuarioDTO> registraUsuarioValidator, UserManager<ApplicationUser> userManager, UsuarioMapper usuarioMapper, IValidator<EditarUsuarioDTO> editarUsuarioValidator, UserDomainService userDomainService)
    {
        _registraUsuarioValidator = registraUsuarioValidator;
        _userManager = userManager;
        _usuarioMapper = usuarioMapper;
        _editarUsuarioValidator = editarUsuarioValidator;
        _userDomainService = userDomainService;
    }

    public async Task<ResultData<DetalheUsuarioDTO>> RegistrarUsuarioAsync(RegistrarUsuarioDTO dados)
    {
        var validationResult = await _registraUsuarioValidator.ValidateAsync(dados);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return ResultData<DetalheUsuarioDTO>.Failure("Erro de validação", ResultStatus.BadRequest, errors);
        }

        var domainResult = await _userDomainService.VerificaEmail(dados.Email);
        if (!domainResult.IsSuccess) return ResultData<DetalheUsuarioDTO>.Failure(domainResult.Message!, domainResult.Status);

        var user = _usuarioMapper.RegistrarUsuarioDTOToApplicationUser(dados);

        var identityResult = await _userManager.CreateAsync(user, dados.Senha);
        if (!identityResult.Succeeded)
        {
            var errors = identityResult.Errors.Select(e => e.Description);
            return ResultData<DetalheUsuarioDTO>.Failure("Erro no registro", ResultStatus.BadRequest, errors);
        }

        var detalheUsuario = _usuarioMapper.ApplicationUserToDetalheUsuarioDTO(user);

        return ResultData<DetalheUsuarioDTO>.Success(detalheUsuario, ResultStatus.Created);
    }

    public async Task<ResultData<DetalheUsuarioDTO>> DetalheUsuarioAsync(string id)
    {
        var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) return ResultData<DetalheUsuarioDTO>.Failure("Usuário não encontrado na base de dados", ResultStatus.NotFound);

        var detalheUsuario = _usuarioMapper.ApplicationUserToDetalheUsuarioDTO(user);

        return ResultData<DetalheUsuarioDTO>.Success(detalheUsuario, ResultStatus.Ok);
    }

    public async Task<ResultData<DetalheUsuarioDTO>> EditarUsuarioAsync(string id, EditarUsuarioDTO dados)
    {
        var validationResult = await _editarUsuarioValidator.ValidateAsync(dados);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return ResultData<DetalheUsuarioDTO>.Failure("Erro de validação", ResultStatus.BadRequest, errors);
        }

        var domainResult = await _userDomainService.VerificaEmail(dados.Email, id);
        if (!domainResult.IsSuccess) return ResultData<DetalheUsuarioDTO>.Failure(domainResult.Message!, domainResult.Status);

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) return ResultData<DetalheUsuarioDTO>.Failure("Usuário não encontrado na base de dados", ResultStatus.NotFound);

        user.Nome = dados.Nome;
        user.Sobrenome = dados.Sobrenome;
        user.UserName = dados.UserName;
        user.Email = dados.Email;

        var identityResult = await _userManager.UpdateAsync(user);
        if (!identityResult.Succeeded)
        {
            var erros = identityResult.Errors.Select(e => e.Description);
            return ResultData<DetalheUsuarioDTO>.Failure("Erro ao atualizar", ResultStatus.BadRequest, erros);
        }

        var detalheUsuario = _usuarioMapper.ApplicationUserToDetalheUsuarioDTO(user);

        return ResultData<DetalheUsuarioDTO>.Success(detalheUsuario, ResultStatus.Ok);
    }

    public async Task<Result> DesabilitaUsuarioAsync(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) return ResultData<DetalheUsuarioDTO>.Failure("Usuário não encontrado na base de dados", ResultStatus.NotFound);

        user.Ativo = false;

        var identityResult = await _userManager.UpdateAsync(user);
        if (!identityResult.Succeeded)
        {
            var erros = identityResult.Errors.Select(e => e.Description);
            return Result.Failure("Erro ao atualizar", ResultStatus.BadRequest, erros);
        }

        return Result.Success(ResultStatus.NoContent);
    }

    public async Task<Result> HabilitaUsuarioAsync(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) return ResultData<DetalheUsuarioDTO>.Failure("Usuário não encontrado na base de dados", ResultStatus.NotFound);

        user.Ativo = true;

        var identityResult = await _userManager.UpdateAsync(user);
        if (!identityResult.Succeeded)
        {
            var erros = identityResult.Errors.Select(e => e.Description);
            return Result.Failure("Erro ao atualizar", ResultStatus.BadRequest, erros);
        }

        return Result.Success(ResultStatus.NoContent);
    }

}