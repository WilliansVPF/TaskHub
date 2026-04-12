using Microsoft.EntityFrameworkCore;
using TaskHub.Domain.Entities;
using TaskHub.Domain.Interfaces.Repositories;
using TaskHub.Infrastructure.Contexts;

namespace TaskHub.Infrastructure.Repositories;

public class ProjetoRepository : IProjetoRepository
{
    private readonly TaskHubContext _context;

    public ProjetoRepository(TaskHubContext context)
    {
        _context = context;
    }

    public async Task<Projeto> CriarProjetoAsync(Projeto dados)
    {
        await _context.AddAsync(dados);
        return dados;        
    }

    public async Task AdicionarMembroAsync(MembroProjeto dados)
    {
        await _context.AddAsync(dados);
    }

    public async Task<Projeto?> GetProjetoByIdAsync(int id, string userId)
    {
        var projeto = await _context.Projetos.AsNoTracking().Include(p => p.MembroProjetos) // Opcional: só se você for usar a lista de membros no front
                                                            .ThenInclude(m => m.Usuario)
                                                            .FirstOrDefaultAsync(p => p.Id == id && p.MembroProjetos.Any(m => m.IdUsuario == userId));
        return projeto;
    }

    public async Task<bool> ProjetoExiste(int id, string userId)
    {
        var projeto = await _context.Projetos.AsNoTracking().AnyAsync(p => p.Id == id && p.MembroProjetos.Any(m => m.IdUsuario == userId));
        return projeto;
    }

    public async Task<IEnumerable<MembroProjeto>> ListarProjetoByUserAsync(string userId)
    {
        var listaProjeto = await _context.MembroProjetos.AsNoTracking().Include(m => m.Projeto).Where(m => m.IdUsuario == userId).ToListAsync();
        // var listaProjeto = await _context.Projetos.AsNoTracking().Include(p => p.MembroProjetos).Where(m => m.IdUsuario == userId).ToListAsync();
        return listaProjeto;
    }

    public async Task<MembroProjeto?> GetMembroProjetoById(int? projetoId, string userId)
    {
        var membro = await _context.MembroProjetos.AsNoTracking().FirstOrDefaultAsync(m => m.IdProjeto == projetoId && m.IdUsuario == userId);
        return membro;
    }

    public async Task<Projeto?> GetProjetoComMembrosEspecificosAsync(int id, string usuarioQueAdicionaId, string usuarioASerAdicionadoId)
    {
        var projeto = await _context.Projetos.AsNoTracking().Include(p => p.MembroProjetos.Where(m => m.IdUsuario == usuarioQueAdicionaId || m.IdUsuario == usuarioASerAdicionadoId))
                                            .FirstOrDefaultAsync(p => p.Id == id && p.MembroProjetos
                                            .Any(m => m.IdUsuario == usuarioQueAdicionaId || m.IdUsuario == usuarioASerAdicionadoId));
        return projeto;
    }

    public async Task<bool> VerificaMembroAsync(int id, string userId)
    {
        var ehMembro = await _context.MembroProjetos.AsNoTracking().AnyAsync(m => m.IdProjeto == id && m.IdUsuario == userId);
        return ehMembro;
    }
}