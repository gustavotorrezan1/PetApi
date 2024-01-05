using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetApi.Models;

namespace PetApi.Data.Mappings;
public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        // Chave Primária
        builder.HasKey(x => x.ProdutoId);
        builder.Property(x => x.ProdutoId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Nome)
            .HasColumnType("NVARCHAR")
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.CodBarras)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

        builder.Property(x => x.Ativo)
            .HasColumnType("BIT")
            .HasMaxLength(1)
            .IsRequired();
    }
}