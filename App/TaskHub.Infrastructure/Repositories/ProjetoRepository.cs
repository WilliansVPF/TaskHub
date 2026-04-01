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
}