using TaskHub.Domain.Entities;

namespace TaskHub.Domain.Interfaces.Repositories;

public interface IProjetoRepository
{
    Task<Projeto> CriarProjeto(Projeto dados);
    
    Task AdicionarMembro(MembroProjeto dados);
}