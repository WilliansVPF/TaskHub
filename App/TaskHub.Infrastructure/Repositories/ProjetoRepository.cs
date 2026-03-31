using Microsoft.EntityFrameworkCore.Query;
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

    public async Task<Projeto> CriarProjeto(Projeto dados)
    {
        await _context.AddAsync(dados);
        return dados;        
    }

    public async Task AdicionarMembro(MembroProjeto dados)
    {
        await _context.AddAsync(dados);
    }
}