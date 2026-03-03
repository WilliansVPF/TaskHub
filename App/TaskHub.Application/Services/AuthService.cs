using FluentValidation;
using Microsoft.AspNetCore.Identity;
using TaskHub.Application.DTOs.Auth;
using TaskHub.Application.Exceptions;
using TaskHub.Domain.Entities;

namespace TaskHub.Application.Services;

public class AuthService
{
    private readonly TokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<LoginDTO> _loginValidator;

    public AuthService(TokenService tokenService, UserManager<ApplicationUser> userManager, IValidator<LoginDTO> loginValidator)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _loginValidator = loginValidator;
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
}