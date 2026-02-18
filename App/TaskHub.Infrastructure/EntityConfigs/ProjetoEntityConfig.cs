using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Domain.Entities;

namespace TaskHub.Infrastructure.EntityConfigs;

public class ProjetoEntityConfig : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.ToTable("projeto");

        builder.Property(p => p.Id)
            .HasColumnName("id");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Titulo)
            .HasColumnName("titulo")
            .HasColumnType("varchar(45)")
            .IsRequired();
        
        builder.Property(p => p.Descricao)
            .HasColumnName("descricao")
            .HasColumnType("varchar(100)");

        builder.Property(p => p.IdUsuario)
            .HasColumnName("idUsuario")
            .IsRequired();

        builder.HasOne(p => p.Usuario)
            .WithMany(u => u.Projetos)
            .HasForeignKey(p => p.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);
    }
}