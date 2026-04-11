using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskHub.Application.DTOs.Tarefa;

public class CadastrarTarefaDTO
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }

    [DefaultValue(false)]
    public bool InicioImediato { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DataFim { get; set; }
    
    [DefaultValue("null")]
    public int? IdProjeto { get; set; }

    public CadastrarTarefaDTO(string titulo, string descricao, DateTime? dataFim, int? idProjeto, bool inicioImediato = false)
    {
        Titulo = titulo;
        Descricao = descricao;
        InicioImediato = inicioImediato;
        DataFim = dataFim;
        IdProjeto = idProjeto;
    }
}