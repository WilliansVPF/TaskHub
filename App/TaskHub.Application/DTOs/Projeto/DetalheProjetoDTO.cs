using TaskHub.Domain.Entities;

namespace TaskHub.Application.DTOs.Projeto;

public class DetalheProjetoDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public IEnumerable<MembroProjetoDTO> Membros { get; set; }

    public DetalheProjetoDTO(int id, string titulo, string descricao, IEnumerable<MembroProjetoDTO> membros)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        Membros = membros;
    }
}