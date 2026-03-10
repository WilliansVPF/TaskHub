using TaskHub.Domain.Entities;

namespace TaskHub.Domain.Interfaces.Repositories;

public interface ITarefaRepository
{
    public Task<Tarefa> CadastrarTarefaAsync(Tarefa dados);

    public Task<Tarefa?> GetTarefaByIdAsync(int id);

    public Tarefa EditarTarefa(Tarefa dados);
}