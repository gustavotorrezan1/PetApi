using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetApi.Models;

namespace PetApi.Data.Mappings;
public class UnidadeMedidaMap : IEntityTypeConfiguration<UnidadeMedida>
{
    public void Configure(EntityTypeBuilder<UnidadeMedida> builder)
    {
        // Chave Primária
        builder.ToTable("UnidadeMedida");
        builder.HasKey(x => x.UnidadeMedidaId);
        
    }
}