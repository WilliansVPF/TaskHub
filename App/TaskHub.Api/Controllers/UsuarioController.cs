using Microsoft.AspNetCore.Mvc;
using TaskHub.Application.DTOs.User;
using TaskHub.Application.Services;

namespace NamespaceName;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{

    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarUsuario(RegistrarUsuarioDTO dados)
    {
        var user = await _usuarioService.RegistrarUsuario(dados);
        return Ok(user);
    }

}