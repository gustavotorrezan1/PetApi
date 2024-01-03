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

    protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlServer("Server=localhost,1433;Database=PetV1;User ID=sa;Password=1q2w3e4r@#$");

    //Local do mapping
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoriaMap());
    }
}


