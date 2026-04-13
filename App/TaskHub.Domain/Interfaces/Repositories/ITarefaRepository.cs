using TaskHub.Domain.Entities;

namespace TaskHub.Domain.Interfaces.Repositories;

public interface ITarefaRepository
{
    Task<Tarefa> CadastrarTarefaAsync(Tarefa dados);

    Task<Tarefa?> GetTarefaByIdAsync(int id);

    Tarefa AtualizarTarefa(Tarefa dados);

    Task<IEnumerable<Tarefa>> ListTarefaByUserAsync(string userId);

    void DeletarTarefa(Tarefa dados);

    Task AdicionarResponsavelAsync(Responsavel dados);

    Task<bool> VerificaResponsavel(int id, string responsavelId);
}