namespace TaskHub.Application.DTOs.Projeto;

public class CriarProjetoDTO
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }

    public CriarProjetoDTO(string titulo, string descricao)
    {
        Titulo = titulo;
        Descricao = descricao;
    }
}