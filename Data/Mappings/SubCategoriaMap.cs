using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetApi.Models;

namespace PetApi.Data.Mappings;
public class SubCategoriaMap : IEntityTypeConfiguration<SubCategoria>
{
    public void Configure(EntityTypeBuilder<SubCategoria> builder)
    {
        builder.HasKey(x => x.SubCategoriaId);
        builder.Property(x => x.SubCategoriaId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Nome)
            .HasColumnType("NVARCHAR")
            .IsRequired();

        builder.Property(x => x.Ativo)
            .HasMaxLength(1)
            .IsRequired();

        builder
            .HasOne(x => x.CategoriaId)
            .WithOne(x => x.Categoria)
    }
}