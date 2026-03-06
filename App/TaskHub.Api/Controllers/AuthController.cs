using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHub.Api.Extensions;
using TaskHub.Application.DTOs.Auth;
using TaskHub.Application.Services;

namespace TaskHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDTO dados)
    {
        var token = await _authService.LoginAsync(dados);
        return Ok(new { token });
    }

    [HttpPatch]
    public async Task<IActionResult> AlterarSenha(AlterarSenhaDTO dados)
    {
        var id = User.GetUserId();
        await _authService.AlterarSenhaAsync(id, dados);
        return Ok("Senha alterada com sucesso");
    }
}