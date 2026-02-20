using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskHub.Domain.Entities;
using TaskHub.Infrastructure.EntityConfigs;

namespace TaskHub.Infrastructure.Contexts;

public class TaskHubContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ItemLista> ItemListas { get; set; }
    public DbSet<Lista> Listas { get; set; }
    public DbSet<MembroProjeto> MembroProjetos { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Responsavel> Responsaveis { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Username=root;Password=root;Host=localhost;Port=5432;Database=TaskHubDB;Pooling=true;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfig());
        builder.ApplyConfiguration(new ItemListaEntityConfig());
        builder.ApplyConfiguration(new ListaEntityConfig());
        builder.ApplyConfiguration(new MembroProjetoEntityConfig());
        builder.ApplyConfiguration(new ProjetoEntityConfig());
        builder.ApplyConfiguration(new ResponsavelEntityConfig());
        builder.ApplyConfiguration(new TarefaEntityConfig());
    }

}