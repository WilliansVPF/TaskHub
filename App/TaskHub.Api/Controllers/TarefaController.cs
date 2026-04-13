using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskHub.Api.Extensions;
using TaskHub.Application.DTOs.Tarefa;
using TaskHub.Application.Services;

namespace TaskHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TarefaController : ControllerBase
{
    private readonly TarefaService _tarefaService;

    public TarefaController(TarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarTarefa(CadastrarTarefaDTO dados)
    {
        var userId = User.GetUserId();        
        var result = await _tarefaService.CadastrarTarefaAsync(userId, dados);
        if (!result.IsSuccess) return result.ToActionResult();

        return CreatedAtAction(nameof(DetalheTarefa), new {id = result.Data!.Id}, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> DetalheTarefa(int id)
    {
        var userId = User.GetUserId();
        var result = await _tarefaService.DetalheTarefaAsync(id, userId);
        return result.ToActionResult();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarTarefa(int id, EditarTarefaDTO dados)
    {
        if (id != dados.Id) return BadRequest("O id informada na URL é diferente do id informado no corpo da requisição.");
        var userId = User.GetUserId();
        var result = await _tarefaService.EditarTarefaAsync(userId, dados);
        return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarTarefa(int id)
    {
        var userId = User.GetUserId();
        var result = await _tarefaService.DeletarTarefaAsync(id, userId);
        return result.ToActionResult();
    }

    [HttpPatch("CompletarTarefa/{id}")]
    public async Task<IActionResult> CompletarTarefa(int id)
    {
        var userId = User.GetUserId();
        var result = await _tarefaService.CompletarTarefaAsync(id, userId);
        return result.ToActionResult();
    }

    [HttpPost("{id}/Responsavel")]
    public async Task<IActionResult> AdicionarResponsavel(int id, AdicionarResponsavelDTO dados)
    {
        var userId = User.GetUserId();
        var result = await _tarefaService.AdicionaResponsavelAsync(id, userId, dados);
        return result.ToActionResult();
    }
}