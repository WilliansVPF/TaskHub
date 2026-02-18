using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Domain.Entities;

namespace TaskHub.Infrastructure.EntityConfigs;

public class ItemListaEntityConfig : IEntityTypeConfiguration<ItemLista>
{
    public void Configure(EntityTypeBuilder<ItemLista> builder)
    {
        builder.ToTable("item_lista");

        builder.Property(il => il.Id)
            .HasColumnName("id");
        builder.HasKey(il => il.Id);

        builder.Property(il => il.Titulo)
            .HasColumnName("titulo")
            .HasColumnType("varchar(45)")
            .IsRequired();

        builder.Property(il => il.Status)
            .HasColumnName("status")
            .IsRequired();

        builder.ToTable(il => il.HasCheckConstraint("CHK_ItemLista_Status", "\"Status\" IN (1, 4)"));

        builder.Property(il => il.IdLista)
            .HasColumnName("idLista")
            .IsRequired();

        builder.HasOne(il => il.Lista)
            .WithMany(l => l.Itens)
            .HasForeignKey(il => il.IdLista)
            .OnDelete(DeleteBehavior.Cascade);
    }
}