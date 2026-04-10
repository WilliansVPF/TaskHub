using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskHub.Application.DTOs.Projeto;
using TaskHub.Application.Mappers;
using TaskHub.Domain.Common;
using TaskHub.Domain.DomainServices;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Enums;
using TaskHub.Domain.Interfaces;
using TaskHub.Domain.Interfaces.Repositories;

namespace TaskHub.Application.Services;

public class ProjetoService
{
    private readonly IValidator<CriarProjetoDTO> _criarProjetoValidator;
    private readonly ProjetoMapper _projetoMapper;
    private readonly IProjetoRepository _projetoRepository;
    private readonly IUnitOfWork _uOW;
    private readonly IValidator<AdicionarMembroDTO> _adicionarMembroValidator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ProjetoDomainService _projetoDomainService;

    public ProjetoService(IValidator<CriarProjetoDTO> criarProjetoValidator, ProjetoMapper projetoMapper, IProjetoRepository projetoRepository, IUnitOfWork uOW, IValidator<AdicionarMembroDTO> adicionarMembroValidator, UserManager<ApplicationUser> userManager, ProjetoDomainService projetoDomainService)
    {
        _criarProjetoValidator = criarProjetoValidator;
        _projetoMapper = projetoMapper;
        _projetoRepository = projetoRepository;
        _uOW = uOW;
        _adicionarMembroValidator = adicionarMembroValidator;
        _userManager = userManager;
        _projetoDomainService = projetoDomainService;
    }

    public async Task<ResultData<DetalheProjetoDTO>> CriarProjetoAsync(CriarProjetoDTO dados, string userId)
    {
        var validationResult = await _criarProjetoValidator.ValidateAsync(dados);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return ResultData<DetalheProjetoDTO>.Failure("Erro de validação", ResultStatus.BadRequest, errors);
        }

        var projeto = _projetoMapper.ToProjeto(dados, userId);

        await _uOW.BeginTransactionAsync();

        try
        {
            projeto = await _projetoRepository.CriarProjetoAsync(projeto);
            await _uOW.SaveChagesAsync();

            var membro = new MembroProjeto(projeto.Id, projeto.IdUsuario, Privilegio.Dono);

            await _projetoRepository.AdicionarMembroAsync(membro);
            
            await _uOW.CommitAsync();

        }
        catch
        {
            await _uOW.RollbackAsync();
            throw;
        }
        
        projeto = await _projetoRepository.GetProjetoByIdAsync(projeto.Id, userId);
        var detalheProjeto = _projetoMapper.ToDetalheProjetoDTO(projeto!);

        return ResultData<DetalheProjetoDTO>.Success(detalheProjeto, ResultStatus.Created);
    }

    public async Task<ResultData<DetalheProjetoDTO>> DetalheProjetoAsync(int id, string userId)
    {
        var projeto = await _projetoRepository.GetProjetoByIdAsync(id, userId);
        if (projeto is null) return ResultData<DetalheProjetoDTO>.Failure("Projeto não encontrado!", ResultStatus.NotFound);

        var detalheProjeto = _projetoMapper.ToDetalheProjetoDTO(projeto);

        return ResultData<DetalheProjetoDTO>.Success(detalheProjeto, ResultStatus.Ok);
    }

    public async Task<ResultData<IEnumerable<ResumoProjetoDTO>>> ListarProjetosByUserAsync(string userId)
    {
        var projetos = await _projetoRepository.ListarProjetoByUserAsync(userId);
        var listaProjetos = _projetoMapper.ToListaResumoProjetoDTO(projetos);
        return ResultData<IEnumerable<ResumoProjetoDTO>>.Success(listaProjetos, ResultStatus.Ok);
    }

    public async Task<ResultData<MembroProjeto>> AdicionarMembroAsync(int id, string userId, AdicionarMembroDTO dados)
    {
        var validationResult = await _adicionarMembroValidator.ValidateAsync(dados);
        if (!validationResult.IsValid) return ResultData<MembroProjeto>.Failure("Erro de validação", ResultStatus.BadRequest);

        if (!await _userManager.Users.AnyAsync(u => u.Id == dados.IdUsuario)) return ResultData<MembroProjeto>.Failure("Usuario a ser adicionado não encontrado", ResultStatus.NotFound);

        var projeto = await _projetoRepository.GetProjetoComMembrosEspecificosAsync(id, userId, dados.IdUsuario);
        if (projeto is null) return ResultData<MembroProjeto>.Failure("Projeto não encontrado", ResultStatus.NotFound);

        var membroQueAdiciona = projeto.MembroProjetos.FirstOrDefault(m => m.IdUsuario == userId);
        var membroASerAdicionado = projeto.MembroProjetos.FirstOrDefault(m => m.IdUsuario == dados.IdUsuario);
        
        var podeAdicionar = _projetoDomainService.PodeAdicionaMembro(membroQueAdiciona, membroASerAdicionado, dados.Privilegio);
        if (!podeAdicionar.IsSuccess) return ResultData<MembroProjeto>.Failure(podeAdicionar.Message!, podeAdicionar.Status);

        var membro = new MembroProjeto(projeto.Id, dados.IdUsuario, dados.Privilegio);
        await _projetoRepository.AdicionarMembroAsync(membro);
        await _uOW.SaveChagesAsync();

        return ResultData<MembroProjeto>.Success(membro,ResultStatus.Ok);
    }
}