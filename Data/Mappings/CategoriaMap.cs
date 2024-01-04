using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetApi.Models;

namespace PetApi.Data.Mappings;
public class CategoriaMap : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        // Tabela
        builder.ToTable("dbo.Categoria");
        
        // Chave Primária
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired();
        builder.Property(x => x.Ativo)
            .IsRequired();
            





    }
}