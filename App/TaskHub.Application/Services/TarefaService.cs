using FluentValidation;
using NamespaceName;
using TaskHub.Application.DTOs.Tarefa;
using TaskHub.Application.Mappers;
using TaskHub.Domain.Common;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;
using TaskHub.Domain.Interfaces;
using TaskHub.Domain.Interfaces.Repositories;

namespace TaskHub.Application.Services;

public class TarefaService
{
    private readonly IValidator<CadastrarTarefaDTO> _cadastraTarefaValidator;
    private readonly IValidator<EditarTarefaDTO> _editarTarefaValidator;
    private readonly TarefaMapper _tarefaMapper;
    private readonly IUnitOfWork _uOW;
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaService(IValidator<CadastrarTarefaDTO> cadastraTarefaValidator, TarefaMapper tarefaMapper, IUnitOfWork uOW, ITarefaRepository tarefaRepository, IValidator<EditarTarefaDTO> editarTarefaValidator)
    {
        _cadastraTarefaValidator = cadastraTarefaValidator;
        _tarefaMapper = tarefaMapper;
        _uOW = uOW;
        _tarefaRepository = tarefaRepository;
        _editarTarefaValidator = editarTarefaValidator;
    }

    public async Task<ResultData<DetalheTarefaDTO>> CadastrarTarefaAsync(string userId, CadastrarTarefaDTO dados)
    {
        var validationResult = await _cadastraTarefaValidator.ValidateAsync(dados);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return ResultData<DetalheTarefaDTO>.Failure("Erro de validação", ResultStatus.BadRequest, errors);
        }

        var tarefa = _tarefaMapper.CadastraTarefaDTOToTarefa(userId, dados);

        tarefa = await _tarefaRepository.CadastrarTarefaAsync(tarefa);
        var result = await _uOW.SaveChagesAsync();
        _uOW.Dispose();
        
        var detalheTarefa = _tarefaMapper.TarefaToDetalheTarefaDTO(tarefa);
        
        return ResultData<DetalheTarefaDTO>.Success(detalheTarefa, ResultStatus.Created);
    }

    public async Task<ResultData<DetalheTarefaDTO>> DetalheTarefaAsync(int id, string userId)
    {
        var tarefa = await _tarefaRepository.GetTarefaByIdAsync(id);
        if (tarefa is null) return ResultData<DetalheTarefaDTO>.Failure("Tarefa não encontrada", ResultStatus.NotFound);

        if (tarefa.IdUsuario != userId) return ResultData<DetalheTarefaDTO>.Failure("Usuário sem premissão para acessar esse recurso", ResultStatus.Forbidden);

        var detalheTarefa = _tarefaMapper.TarefaToDetalheTarefaDTO(tarefa);

        return ResultData<DetalheTarefaDTO>.Success(detalheTarefa, ResultStatus.Ok);
    }

    public async Task<ResultData<DetalheTarefaDTO>> EditarTarefaAsync(string userId, EditarTarefaDTO dados)
    {
        var validationResult = await _editarTarefaValidator.ValidateAsync(dados);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return ResultData<DetalheTarefaDTO>.Failure("Erro de validação", ResultStatus.BadRequest, errors);
        }

        var tarefa = await _tarefaRepository.GetTarefaByIdAsync(dados.Id);
        if (tarefa is null) return ResultData<DetalheTarefaDTO>.Failure("Tarefa não encontrada", ResultStatus.NotFound);

        if (tarefa.IdUsuario != userId) return ResultData<DetalheTarefaDTO>.Failure("Usuário sem premissão para acessar esse recurso", ResultStatus.Forbidden);

        tarefa.Titulo = dados.Titulo;
        tarefa.Descricao = dados.Descricao;
        tarefa.DataInicio = dados.DataInicio;
        tarefa.DataFim = dados.DataFim;
        tarefa.Status = dados.Status;

        tarefa = _tarefaRepository.EditarTarefa(tarefa);
        await _uOW.SaveChagesAsync();
        _uOW.Dispose();

        var detalheTarefa = _tarefaMapper.TarefaToDetalheTarefaDTO(tarefa);
        
        return ResultData<DetalheTarefaDTO>.Success(detalheTarefa, ResultStatus.Ok);
    }

    public async Task<ResultData<IEnumerable<ResumoTarefaDTO>>> ListTarefaByUserAsync(string userId)
    {
        var tarefas = await _tarefaRepository.ListTarefaByUserAsync(userId);
        var listaTarefas = _tarefaMapper.TarefaToResumoTarefaDTO(tarefas);
        return ResultData<IEnumerable<ResumoTarefaDTO>>.Success(listaTarefas, ResultStatus.Ok);
    }

    public async Task<Result> DeletarTarefaAsync(int id, string userId)
    {
        var tarefa = await _tarefaRepository.GetTarefaByIdAsync(id);
        if (tarefa is null) return Result.Failure("Tarefa não encontrada", ResultStatus.NotFound);

        if (tarefa.IdUsuario != userId) return Result.Failure("Usuário sem premissão para acessar esse recurso", ResultStatus.Forbidden);

        _tarefaRepository.DeletarTarefa(tarefa);
        await _uOW.SaveChagesAsync();
        _uOW.Dispose();

        return Result.Success(ResultStatus.NoContent);
    }
}