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
        var result = await _usuarioService.RegistrarUsuarioAsync(dados);
        if (!result.IsSuccess) return result.ToActionResult();
        return CreatedAtAction(nameof(DetalheUsuario), new {id = result.Data!.Id}, result);
    }

    [HttpGet]
    public async Task<IActionResult> DetalheUsuario()
    {
        var id = User.GetUserId();
        var result = await _usuarioService.DetalheUsuarioAsync(id);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> EditarUsuario(EditarUsuarioDTO dados)
    {
        var id = User.GetUserId();
        var result = await _usuarioService.EditarUsuarioAsync(id, dados);
        return result.ToActionResult();
    }

    [HttpDelete("DesabilitaUsuario")]
    public async Task<IActionResult> DesabilitaUsuario()
    {
        var id = User.GetUserId();
        var result = await _usuarioService.DesabilitaUsuarioAsync(id);
        return result.ToActionResult();
    }

    [HttpPatch("HabilitaUsuario")]
    public async Task<IActionResult> HabilitaUsuario()
    {
        var id = User.GetUserId();
        var result = await _usuarioService.HabilitaUsuarioAsync(id);
        return result.ToActionResult();
    }

}