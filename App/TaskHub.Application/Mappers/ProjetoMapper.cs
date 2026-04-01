using TaskHub.Application.DTOs.Projeto;
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
        var membro = dados.MembroProjetos.Select(m => new MembroProjetoDTO
        {
            Id = m.IdUsuario,
            Nome = m.Usuario.Nome,
            Privilegio = m.Privilegio
        }).ToList();
        var detalheProjeto = new DetalheProjetoDTO(dados.Id, dados.Titulo, dados.Descricao, membro);
        return detalheProjeto;
    }
}