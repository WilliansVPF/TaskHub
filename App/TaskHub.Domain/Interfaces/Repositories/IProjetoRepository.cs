using TaskHub.Domain.Entities;

namespace TaskHub.Domain.Interfaces.Repositories;

public interface IProjetoRepository
{
    Task<Projeto> CriarProjetoAsync(Projeto dados);
    
    Task AdicionarMembroAsync(MembroProjeto dados);

    Task<Projeto?> GetProjetoByIdAsync(int id, string userId);

    Task<IEnumerable<MembroProjeto>> ListarProjetoByUserAsync(string userId);

    Task<MembroProjeto?> GetMembroProjetoById(int? projetoId, string userId);
}