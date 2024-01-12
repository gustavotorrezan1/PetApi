using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetApi.Models;

namespace PetApi.Data.Mappings;
public class CategoriaMap : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categoria");
        builder.HasKey(x => x.CategoriaId);

        builder.Property(x => x.Nome)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(256);

        builder.Property(x => x.Ativo)
            .HasColumnType("INT")
            .HasMaxLength(1)
            .HasDefaultValue(1);
    }
}