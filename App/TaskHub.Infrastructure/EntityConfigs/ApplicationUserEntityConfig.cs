using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Domain.Entities;

namespace TaskHub.Infrastructure.EntityConfigs;

public class ApplicationUserEntityConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.Nome)
            .HasColumnName("nome")
            .HasColumnType("varchar(45)")
            .IsRequired();

        builder.Property(u => u.Sobrenome)
            .HasColumnName("sobrenome")
            .HasColumnType("varchar(45)")
            .IsRequired();

        builder.Property(u => u.Ativo)
            .HasColumnName("ativo")
            .IsRequired();

        builder.Property(u => u.DataCadastro)
            .HasColumnName("dataCadastro")
            .IsRequired();
    }
}