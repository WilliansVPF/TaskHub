using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Domain.Entities;

namespace TaskHub.Infrastructure.EntityConfigs;

public class MembroProjetoEntityConfig : IEntityTypeConfiguration<MembroProjeto>
{
    public void Configure(EntityTypeBuilder<MembroProjeto> builder)
    {
        builder.ToTable("membros_projeto");

        builder.Property(mp => mp.IdProjeto)
            .HasColumnName("idProjeto");
        builder.HasKey(mp => mp.IdProjeto);

        builder.Property(mp => mp.IdUsuario)
            .HasColumnName("idUsuario");
        builder.HasKey(mp => mp.IdUsuario);

        builder.Property(mp => mp.Privilegio)
            .HasColumnName("privilegio")
            .IsRequired();

        builder.ToTable(mp => mp.HasCheckConstraint("CHK_MembroProjeto_Privilegio", "\"privilegio\" IN (1, 2, 3)"));

        builder.HasOne(mp => mp.Projeto)
            .WithMany(p => p.MembroProjetos)
            .HasForeignKey(mp => mp.IdProjeto)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mp => mp.Usuario)
            .WithMany(u => u.MembroProjetos)
            .HasForeignKey(mp => mp.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);
    }
}