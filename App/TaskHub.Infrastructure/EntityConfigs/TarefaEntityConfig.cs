using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Domain.Entities;

namespace TaskHub.Infrastructure.EntityConfigs;

public class TarefaEntityConfig : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("tarefa");

        builder.Property(t => t.Id)
            .HasColumnName("id");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Titulo)
            .HasColumnName("titulo")
            .HasColumnType("varchar(45)")
            .IsRequired();

        builder.Property(t => t.Descricao)
            .HasColumnName("descricao")
            .HasColumnType("varchar(100)");

        builder.Property(t => t.DataInicio)
            .HasColumnName("dataInicio");

        builder.Property(t => t.DataFim)
            .HasColumnName("dataFim");

        builder.Property(t => t.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.ToTable(t => t.HasCheckConstraint("CHK_Tarefa_Status", "\"status\" IN (1, 2, 3, 4)"));

        builder.Property(t => t.IdUsuario)
            .HasColumnName("idUsuario")
            .IsRequired();

        builder.Property(t => t.IdProjeto)
            .HasColumnName("idProjeto");

        builder.HasOne(t => t.Usuario)
            .WithMany(u => u.Tarefas)
            .HasForeignKey(t => t.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.Projeto)
            .WithMany(p => p.Tarefas)
            .HasForeignKey(t => t.IdProjeto)
            .OnDelete(DeleteBehavior.Cascade);
    }
}