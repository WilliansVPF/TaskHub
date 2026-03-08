using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Domain.Entities;

namespace TaskHub.Infrastructure.EntityConfigs;

public class ResponsavelEntityConfig : IEntityTypeConfiguration<Responsavel>
{
    public void Configure(EntityTypeBuilder<Responsavel> builder)
    {
        builder.ToTable("responsavel");

        builder.Property(r => r.IdUsuario)
            .HasColumnName("idUsuario");
        builder.HasKey(r => r.IdUsuario);

        builder.Property(r => r.IdTarefa)
            .HasColumnName("idTarefa");
        builder.HasKey(r => r.IdTarefa);

        builder.HasOne(r => r.Usuario)
            .WithMany(u => u.Responsaveis)
            .HasForeignKey(r => r.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(r => r.Tarefa)
            .WithMany(t => t.Responsaveis)
            .HasForeignKey(r => r.IdTarefa)
            .OnDelete(DeleteBehavior.Cascade);
    }
}