using Microsoft.EntityFrameworkCore;
using PetApi.Data.Mappings;
using PetApi.Models;
namespace PetApi.Data;

public class PetDbContext : DbContext
{
    public PetDbContext(DbContextOptions<PetDbContext> options)
           : base(options)
    {
    }
    // Local das classe
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<SubCategoria> SubCategorias { get; set; }
    public DbSet<UnidadeMedida> UnidadeMedidas { get; set; }

    //Local do mapping
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoriaMap());
    }
}


