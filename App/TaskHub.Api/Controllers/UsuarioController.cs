using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHub.Api.Extensions;
using TaskHub.Application.DTOs.User;
using TaskHub.Application.Services;

namespace TaskHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuarioController : ControllerBase
{

    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegistrarUsuario(RegistrarUsuarioDTO dados)
    {
        var user = await _usuarioService.RegistrarUsuarioAsync(dados);
        return CreatedAtAction(nameof(DetalheUsuario), new {id = user.Id}, user);
    }

    [HttpGet]
    public async Task<IActionResult> DetalheUsuario()
    {
        var id = User.GetUserId();
        var user = await _usuarioService.DetalheUsuarioAsync(id);
        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> EditarUsuario(EditarUsuarioDTO dados)
    {
        var id = User.GetUserId();

        var user = await _usuarioService.EditarUsuarioAsync(id, dados);
        return Ok(user);
    }

    [HttpDelete]
    public async Task<IActionResult> DesabilitaUsuario()
    {
        var id = User.GetUserId();

        await _usuarioService.DesabilitaUsuarioAsync(id);
        return Ok(new {message = "Sua conta foi desabilitada com sucesso"});
    }

    [HttpPatch]
    public async Task<IActionResult> HabilitaUsuario()
    {
        var id = User.GetUserId();

        await _usuarioService.HabilitaUsuarioAsync(id);
        return Ok(new {message = "Sua conta foi habilitada com sucesso"});
    }

}