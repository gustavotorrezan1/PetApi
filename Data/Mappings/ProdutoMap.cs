using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetApi.Models;

namespace PetApi.Data.Mappings;
public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
       builder.Property(x => x.ProdutoId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
        
        // Chave Primária
        builder.HasKey(x => x.ProdutoId);

        builder.Property(x => x.Nome)
            .HasColumnType("NVARCHAR")  
            .HasMaxLength(256);

        builder.Property(x => x.PrecoCusto)
            .HasColumnType("FLOAT");
            
        builder.Property(x => x.PrecoVenda)
            .HasColumnType("FLOAT");

        builder.Property(x => x.CodBarras)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(30);

        builder.Property(x => x.Ativo)
            .HasColumnType("INT")
            .HasMaxLength(1)
            .HasDefaultValue(1);



    }
}