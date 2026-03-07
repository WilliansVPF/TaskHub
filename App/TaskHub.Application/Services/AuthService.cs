using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskHub.Application.DTOs.Auth;
using TaskHub.Application.Exceptions;
using TaskHub.Domain.Entities;

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

    public async Task<string> LoginAsync(LoginDTO dados)
    {
        await _loginValidator.ValidateAndThrowAsync(dados);

        var user = await _userManager.FindByNameAsync(dados.UserName);
        if (user is null) throw new BadCredentialsException("Usuário ou Senha inválido(s).");

        if (!await _userManager.CheckPasswordAsync(user,dados.Senha)) throw new BadCredentialsException("Usuário ou Senha inválido(s).");

        var token = _tokenService.GerarToken(user);
        return token;
    }

    public async Task AlterarSenhaAsync(string id, AlterarSenhaDTO dados)
    {
        await _alterarSenhaValidator.ValidateAndThrowAsync(dados);

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) throw new ResourceNotFoundException("Usuário não encontrado na base de dados");

        // if (!await _userManager.CheckPasswordAsync(user,dados.SenhaAtual)) throw new BadCredentialsException("Senha atual inválida");

        var identityResult = await _userManager.ChangePasswordAsync(user, dados.SenhaAtual, dados.NovaSenha);
        if (!identityResult.Succeeded) throw new IdentityChangePasswordException(identityResult.Errors);
    }
}