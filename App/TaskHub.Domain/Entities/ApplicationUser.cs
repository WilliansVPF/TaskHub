using Microsoft.AspNetCore.Identity;

namespace TaskHub.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public IEnumerable<Responsavel> Responsaveis { get; set; } = null!;   
    public IEnumerable<Tarefa> Tarefas { get; set; } = null!;
    public IEnumerable<Projeto> Projetos { get; set; } = null!;
    public IEnumerable<MembroProjeto> MembroProjetos { get; set; } = null!;
}