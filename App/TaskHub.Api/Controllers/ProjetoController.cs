using Microsoft.AspNetCore.Mvc;
using TaskHub.Api.Extensions;
using TaskHub.Application.DTOs.Projeto;
using TaskHub.Application.Services;

namespace TaskHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjetoController : ControllerBase
{
    private readonly ProjetoService _projetoService;

    public ProjetoController(ProjetoService projetoService)
    {
        _projetoService = projetoService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarProjeto(CriarProjetoDTO dados)
    {
        var userId = User.GetUserId();
        var result = await _projetoService.CriarProjetoAsync(dados, userId);
        if (!result.IsSuccess) return result.ToActionResult();
        return CreatedAtAction(nameof(DetalheProjeto), new {id = result.Data!.Id}, result);
    }

    [HttpGet("{id}/DetalheProjeto")]
    public async Task<IActionResult> DetalheProjeto(int id)
    {
        var userId = User.GetUserId();
        var result = await _projetoService.DetalheProjetoAsync(id, userId);
        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> ListarProjetos()
    {
        var userId = User.GetUserId();
        var result = await _projetoService.ListarProjetosByUserAsync(userId);
        return result.ToActionResult();
    }

    [HttpPost("{id}/Membro")]
    public async Task<IActionResult> AdicionarMembro(int id, AdicionarMembroDTO dados)
    {
        var userId = User.GetUserId();
        var result = await _projetoService.AdicionarMembroAsync(id, userId, dados);
        if (!result.IsSuccess) return result.ToActionResult();
        return Ok(result);
    }
}