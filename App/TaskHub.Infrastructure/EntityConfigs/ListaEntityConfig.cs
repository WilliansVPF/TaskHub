using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Domain.Entities;

namespace TaskHub.Infrastructure.EntityConfigs;

public class ListaEntityConfig : IEntityTypeConfiguration<Lista>
{
    public void Configure(EntityTypeBuilder<Lista> builder)
    {
        builder.ToTable("lista");

        builder.Property(l => l.Id)
            .HasColumnName("id");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Titulo)
            .HasColumnName("titulo")
            .HasColumnType("varchar(45)")
            .IsRequired();

        builder.Property(l => l.Descricao)
            .HasColumnName("descricao")
            .HasColumnType("varchar(100)");
        
        builder.Property(l => l.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.ToTable(l => l.HasCheckConstraint("CHK_Lista_Status", "\"Status\" IN (1, 4)"));

        builder.Property(l => l.IdTarefa)
            .HasColumnName("idTarefa")
            .IsRequired();

        builder.HasOne(l => l.Tarefa)
            .WithMany(t => t.Listas)
            .HasForeignKey(l => l.IdTarefa)
            .OnDelete(DeleteBehavior.Cascade);
    }
}