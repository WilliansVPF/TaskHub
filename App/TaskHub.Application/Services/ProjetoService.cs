using FluentValidation;
using TaskHub.Application.DTOs.Projeto;
using TaskHub.Application.Mappers;
using TaskHub.Domain.Common;
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

    public ProjetoService(IValidator<CriarProjetoDTO> criarProjetoValidator, ProjetoMapper projetoMapper, IProjetoRepository projetoRepository, IUnitOfWork uOW)
    {
        _criarProjetoValidator = criarProjetoValidator;
        _projetoMapper = projetoMapper;
        _projetoRepository = projetoRepository;
        _uOW = uOW;
    }

    public async Task<ResultData<DetalheProjetoDTO>> CriarProjeto(CriarProjetoDTO dados, string userId)
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
}