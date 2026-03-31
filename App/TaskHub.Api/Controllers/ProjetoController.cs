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
        var result = await _projetoService.CriarProjeto(dados, userId);
        if (!result.IsSuccess) return result.ToActionResult();
        return Ok(result);
    }
}