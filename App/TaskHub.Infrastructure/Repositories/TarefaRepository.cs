using Microsoft.EntityFrameworkCore;
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

    public Tarefa AtualizarTarefa(Tarefa dados)
    {
        _context.Update(dados);
        return dados;
    }

    public async Task<Tarefa?> GetTarefaByIdAsync(int id)
    {
        return await _context.Tarefas.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Tarefa>> ListTarefaByUserAsync(string userId)
    {
        var tarefas = await _context.Tarefas.Where(t => t.IdUsuario == userId).ToListAsync();
        return tarefas;
    }

    public void DeletarTarefa(Tarefa dados)
    {
        _context.Tarefas.Remove(dados);
    }

    public async Task AdicionarResponsavelAsync(Responsavel dados)
    {
        await _context.AddAsync(dados);
    }

    public async Task<bool> VerificaResponsavel(int id, string responsavelId)
    {
        var ehResponsavel= await _context.Responsaveis.AnyAsync(r => r.IdTarefa == id && r.IdUsuario == responsavelId);
        return ehResponsavel;
    }
}