using Microsoft.AspNetCore.Mvc;
using TaskHub.Application.DTOs.Auth;
using TaskHub.Application.Services;

namespace TaskHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO dados)
    {
        var token = await _authService.LoginAsync(dados);
        return Ok(new {token = token});
    }
}