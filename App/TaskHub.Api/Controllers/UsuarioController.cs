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
        var user = await _usuarioService.RegistrarUsuarioAsync(dados);
        return CreatedAtAction(nameof(DetalheUsuario), new {id = user.Id}, user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> DetalheUsuario(string id)
    {
        var user = await _usuarioService.DetalheUsuarioAsync(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarUsuario(string id, EditarUsuarioDTO dados)
    {
        if (id != dados.Id) return BadRequest("O ID informado na URL é diferente do ID informado no corpo da requisição");
        var user = await _usuarioService.EditarUsuarioAsync(dados);
        if (user is null) return BadRequest("Email já cadastrado");
        return Ok(user);
    }

}