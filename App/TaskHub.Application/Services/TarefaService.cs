using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NamespaceName;
using TaskHub.Application.DTOs.Tarefa;
using TaskHub.Application.Mappers;
using TaskHub.Domain.Common;
using TaskHub.Domain.DomainServices;
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
    private readonly TarefaDomainService _tarefaDomainService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IProjetoRepository _projetoRepository;

    public TarefaService(IValidator<CadastrarTarefaDTO> cadastraTarefaValidator, TarefaMapper tarefaMapper, IUnitOfWork uOW, ITarefaRepository tarefaRepository, IValidator<EditarTarefaDTO> editarTarefaValidator, TarefaDomainService tarefaDomainService, UserManager<ApplicationUser> userManager, IProjetoRepository projetoRepository)
    {
        _cadastraTarefaValidator = cadastraTarefaValidator;
        _tarefaMapper = tarefaMapper;
        _uOW = uOW;
        _tarefaRepository = tarefaRepository;
        _editarTarefaValidator = editarTarefaValidator;
        _tarefaDomainService = tarefaDomainService;
        _userManager = userManager;
        _projetoRepository = projetoRepository;
    }

    public async Task<ResultData<DetalheTarefaDTO>> CadastrarTarefaAsync(string userId, CadastrarTarefaDTO dados)
    {
        var validationResult = await _cadastraTarefaValidator.ValidateAsync(dados);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return ResultData<DetalheTarefaDTO>.Failure("Erro de validação", ResultStatus.BadRequest, errors);
        }

        if (dados.IdProjeto != null && dados.IdProjeto != 0)
        {
            var projeto = await _projetoRepository.ProjetoExiste((int)dados.IdProjeto, userId);
            if (!projeto) return ResultData<DetalheTarefaDTO>.Failure("Projeto não encontrado!", ResultStatus.NotFound);
        }

        var tarefa = _tarefaMapper.CadastraTarefaDTOToTarefa(userId, dados);

        tarefa = await _tarefaRepository.CadastrarTarefaAsync(tarefa);
        await _uOW.SaveChagesAsync();
        _uOW.Dispose();
        
        var detalheTarefa = _tarefaMapper.TarefaToDetalheTarefaDTO(tarefa);
        
        return ResultData<DetalheTarefaDTO>.Success(detalheTarefa, ResultStatus.Created);
    }

    public async Task<ResultData<DetalheTarefaDTO>> DetalheTarefaAsync(int id, string userId)
    {
        var tarefa = await _tarefaRepository.GetTarefaByIdAsync(id);
        if (tarefa is null) return ResultData<DetalheTarefaDTO>.Failure("Tarefa não encontrada", ResultStatus.NotFound);

        bool ehMembro = false;
        if (tarefa.IdProjeto.HasValue) ehMembro = await _projetoRepository.VerificaMembroAsync(tarefa.IdProjeto.Value, userId);

        var domainResult = _tarefaDomainService.PodeVer(tarefa, ehMembro, userId);
        if (!domainResult.IsSuccess) return ResultData<DetalheTarefaDTO>.Failure(domainResult.Message!, domainResult.Status);

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

        tarefa = _tarefaRepository.AtualizarTarefa(tarefa);
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

        var usuario = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);

        MembroProjeto? membro = null;
        
        if (tarefa.IdProjeto is not null) membro = await _projetoRepository.GetMembroProjetoById(tarefa.IdProjeto, usuario!.Id);

        // if (tarefa.IdUsuario != userId) return Result.Failure("Usuário sem premissão para acessar esse recurso", ResultStatus.Forbidden);
        var podeDeletar = _tarefaDomainService.PodeExcluir(tarefa, usuario!, membro);
        if (!podeDeletar.IsSuccess) return podeDeletar;

        _tarefaRepository.DeletarTarefa(tarefa);
        await _uOW.SaveChagesAsync();
        _uOW.Dispose();

        return Result.Success(ResultStatus.NoContent);
    }

    public async Task<ResultData<DetalheTarefaDTO>> CompletarTarefaAsync(int id, string userId)
    {
        var tarefa = await _tarefaRepository.GetTarefaByIdAsync(id);
        if (tarefa is null) return ResultData<DetalheTarefaDTO>.Failure("Tarefa não encontrada", ResultStatus.NotFound);

        if (tarefa.IdUsuario != userId) return ResultData<DetalheTarefaDTO>.Failure("Usuário sem premissão para acessar esse recurso", ResultStatus.Forbidden);

        tarefa.Status = Status.Completa;

        tarefa = _tarefaRepository.AtualizarTarefa(tarefa);
        await _uOW.SaveChagesAsync();
        _uOW.Dispose();

        var detalheTarefa = _tarefaMapper.TarefaToDetalheTarefaDTO(tarefa);

        return ResultData<DetalheTarefaDTO>.Success(detalheTarefa, ResultStatus.Ok);
    }
}