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
        var tarefa = await _tarefaService.CadastrarTarefa(userId, dados);
        return Ok(tarefa);
    }
}