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

        builder.Property(x => x.Nome)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(256);

        builder.Property(x => x.Ativo)
            .HasColumnType("INT")
            .HasMaxLength(1)
            .HasDefaultValue(1);

    }
}