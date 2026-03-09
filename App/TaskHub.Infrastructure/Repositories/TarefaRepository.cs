using TaskHub.Domain.Entities;
using TaskHub.Domain.Interfaces.Repositories;
using TaskHub.Infrastructure.Contexts;

namespace TaskHub.Infrastructure.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly TaskHubContext _context;

    public TarefaRepository(TaskHubContext context)
    {
        _context = context;
    }

    public async Task<Tarefa> CadastrarTarefaAsync(Tarefa dados)
    {
        await _context.AddAsync(dados);
        return dados;
    }
}