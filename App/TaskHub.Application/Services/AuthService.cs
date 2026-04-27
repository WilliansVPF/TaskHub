using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskHub.Application.DTOs.Auth;
using TaskHub.Domain.Common;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;

namespace TaskHub.Application.Services;

public class AuthService
{
    private readonly TokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<LoginDTO> _loginValidator;
    private readonly IValidator<AlterarSenhaDTO> _alterarSenhaValidator;

    public AuthService(TokenService tokenService, UserManager<ApplicationUser> userManager, IValidator<LoginDTO> loginValidator, IValidator<AlterarSenhaDTO> alterarSenhaValidator)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _loginValidator = loginValidator;
        _alterarSenhaValidator = alterarSenhaValidator;
    }

    public async Task<ResultData<string>> LoginAsync(LoginDTO dados)
    {
        var validationResult = await _loginValidator.ValidateAsync(dados);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return ResultData<string>.Failure("Erro de validação", ResultStatus.BadRequest, errors);
        }

        var user = await _userManager.FindByNameAsync(dados.UserName);
        if (user is null) return ResultData<string>.Failure("Usuário ou Senha inválido(s).", ResultStatus.Unauthorized);

        if (!await _userManager.CheckPasswordAsync(user,dados.Senha)) return ResultData<string>.Failure("Usuário ou Senha inválido(s).", ResultStatus.Unauthorized);

        var token = _tokenService.GerarToken(user);
        return ResultData<string>.Success(token, ResultStatus.Ok);
    }

    public async Task<Result> AlterarSenhaAsync(string id, AlterarSenhaDTO dados)
    {
        var validationResult = await _alterarSenhaValidator.ValidateAsync(dados);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return Result.Failure("Erro de validação", ResultStatus.BadRequest, errors);
        }

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) return Result.Failure("Usuário não encontrado na base de dados", ResultStatus.NotFound);

        var identityResult = await _userManager.ChangePasswordAsync(user, dados.SenhaAtual, dados.NovaSenha);
        if (!identityResult.Succeeded)
        {
            var erros = identityResult.Errors.Select(e => e.Description);
            return Result.Failure("Erro ao alterar senha", ResultStatus.BadRequest, erros);
        }

        return Result.Success(ResultStatus.NoContent);     
    }
}