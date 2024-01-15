using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            .HasMaxLength(256);

        builder.Property(x => x.Ativo)
           .HasColumnType("INT")
           .HasMaxLength(1)
           .HasDefaultValue(1);

        


    }
}