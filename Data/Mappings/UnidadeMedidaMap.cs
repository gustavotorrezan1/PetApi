using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetApi.Models;

namespace PetApi.Data.Mappings;
public class UnidadeMedidaMap : IEntityTypeConfiguration<UnidadeMedida>
{
    public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
    {
        // Chave Primária
        builder.HasKey(x => x.UnidadeMedidaId);
        builder.Property(x => x.UnidadeMedidaId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Nome)
            .HasColumnType("NVARCHAR")
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.Sigla)
            .HasColumnType("NVARCHAR")
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(x => x.Ativo)
            .HasColumnType("BIT")
            .HasMaxLength(1)
            .IsRequired();
    }
}