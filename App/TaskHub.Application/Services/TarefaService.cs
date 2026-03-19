using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NamespaceName;
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
    private readonly IValidator<EditarTarefaDTO> _editarTarefaValidator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly TarefaMapper _tarefaMapper;
    private readonly IUnitOfWork _uOW;
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaService(IValidator<CadastrarTarefaDTO> cadastraTarefaValidator, UserManager<ApplicationUser> userManager, TarefaMapper tarefaMapper, IUnitOfWork uOW, ITarefaRepository tarefaRepository, IValidator<EditarTarefaDTO> editarTarefaValidator)
    {
        _cadastraTarefaValidator = cadastraTarefaValidator;
        _userManager = userManager;
        _tarefaMapper = tarefaMapper;
        _uOW = uOW;
        _tarefaRepository = tarefaRepository;
        _editarTarefaValidator = editarTarefaValidator;
    }

    public async Task<DetalheTarefaDTO> CadastrarTarefa(string userId, CadastrarTarefaDTO dados)
    {
        await _cadastraTarefaValidator.ValidateAndThrowAsync(dados);

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

        if (tarefa.IdUsuario != userId) throw new ForbiddenException("Usuário sem premissão para acessar esse recurso");

        var detalheTarefa = _tarefaMapper.TarefaToDetalheTarefaDTO(tarefa);

        return detalheTarefa;
    }

    public async Task<DetalheTarefaDTO> EditarTarefaAsync(string userId, EditarTarefaDTO dados)
    {
        await _editarTarefaValidator.ValidateAndThrowAsync(dados);

        var tarefa = await _tarefaRepository.GetTarefaByIdAsync(dados.Id);

        if (tarefa is null) throw new ResourceNotFoundException("Tarefa não encontrada");

        if (tarefa.IdUsuario != userId) throw new ResourceNotFoundException("Tarefa não encontrada");

        tarefa.Titulo = dados.Titulo;
        tarefa.Descricao = dados.Descricao;
        tarefa.DataInicio = dados.DataInicio;
        tarefa.DataFim = dados.DataFim;
        tarefa.Status = dados.Status;

        tarefa = _tarefaRepository.EditarTarefa(tarefa);
        await _uOW.SaveChagesAsync();
        _uOW.Dispose();

        var detalheTarefa = _tarefaMapper.TarefaToDetalheTarefaDTO(tarefa);
        
        return detalheTarefa;
    }
}