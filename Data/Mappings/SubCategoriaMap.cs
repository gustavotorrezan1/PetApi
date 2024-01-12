using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetApi.Models;

namespace PetApi.Data.Mappings;
public class SubCategoriaMap : IEntityTypeConfiguration<SubCategoria>
{
    public void Configure(EntityTypeBuilder<SubCategoria> builder)
    {
        builder.ToTable("SubCategoria"); // Define qual a tabela
        builder.HasKey(x => x.SubCategoriaId);
  
    }
}