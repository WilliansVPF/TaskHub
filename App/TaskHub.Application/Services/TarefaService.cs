using FluentValidation;
using Microsoft.AspNetCore.Identity;
using TaskHub.Application.DTOs.Tarefa;
using TaskHub.Application.Exceptions;
using TaskHub.Application.Mappers;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Interfaces;
using TaskHub.Domain.Interfaces.Repositories;
using TaskHub.Infrastructure.Contexts;

namespace TaskHub.Application.Services;

public class TarefaService
{
    private readonly IValidator<CadastrarTarefaDTO> _cadastraTarefaValidator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly TarefaMapper _tarefaMapper;
    private readonly IUnitOfWork _uOW;
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaService(IValidator<CadastrarTarefaDTO> cadastraTarefaValidator, UserManager<ApplicationUser> userManager, TarefaMapper tarefaMapper, IUnitOfWork uOW, ITarefaRepository tarefaRepository)
    {
        _cadastraTarefaValidator = cadastraTarefaValidator;
        _userManager = userManager;
        _tarefaMapper = tarefaMapper;
        _uOW = uOW;
        _tarefaRepository = tarefaRepository;
    }

    public async Task<DetalheTarefaDTO> CadastrarTarefa(string userId, CadastrarTarefaDTO dados)
    {
        await _cadastraTarefaValidator.ValidateAndThrowAsync(dados);

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) throw new ResourceNotFoundException("Usuário não encontrado");

        var tarefa = _tarefaMapper.CadastraTarefaDTOToTarefa(userId, dados);

        tarefa = await _tarefaRepository.CadastrarTarefaAsync(tarefa);
        await _uOW.SaveChagesAsync();
        _uOW.Dispose();
        
        var detalheTarefa = _tarefaMapper.TarefaToDetalheTarefaDTO(tarefa);
        
        return detalheTarefa;
    }

    public async Task<DetalheTarefaDTO> DetalheTarefaAsync(int id, string userId)
    {
        var tarefa = await _tarefaRepository.GetTarefaByIdAsync(id);

        if (tarefa is null) throw new ResourceNotFoundException("Tarefa não encontrada");

        if (tarefa.IdUsuario != userId) throw new ResourceNotFoundException("Tarefa não encontrada");

        var detalheTarefa = _tarefaMapper.TarefaToDetalheTarefaDTO(tarefa);

        return detalheTarefa;
    }
}