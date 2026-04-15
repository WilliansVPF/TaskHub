using TaskHub.Application.DTOs.Projeto;
using TaskHub.Application.DTOs.Tarefa;
using TaskHub.Domain.Entities;

namespace TaskHub.Application.Mappers;

public class ProjetoMapper
{
    public Projeto ToProjeto(CriarProjetoDTO dados, string userId)
    {
        var projeto = new Projeto(dados.Titulo, dados.Descricao, userId);
        return projeto;
    }

    public DetalheProjetoDTO ToDetalheProjetoDTO(Projeto dados)
    {
        int i = dados.MembroProjetos.Count();
        Console.WriteLine(i);
        var membro = dados.MembroProjetos.Select(m => new MembroProjetoDTO
        {
            Id = m.IdUsuario,
            Nome = m.Usuario.Nome,
            Privilegio = m.Privilegio
        }).ToList();

        var tarefas = dados.Tarefas.Select(t => new ResumoTarefaDTO(t.Id, t.Titulo, t.Status, t.IdProjeto)).ToList();

        var detalheProjeto = new DetalheProjetoDTO(dados.Id, dados.Titulo, dados.Descricao, membro, tarefas);
        return detalheProjeto;
    }

    public IEnumerable<ResumoProjetoDTO> ToListaResumoProjetoDTO(IEnumerable<MembroProjeto> dados)
    {
        var listaProjetos = dados.Select(p => new ResumoProjetoDTO
        {
            Id = p.Projeto.Id,
            Titulo = p.Projeto.Titulo,
            Descricao = p.Projeto.Descricao
        }).ToList();
        return listaProjetos;
    }
}