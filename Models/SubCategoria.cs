namespace PetApi.Models;
public class SubCategoria
{
    public int SubCategoriaId { get; set; }
    public int Nome { get; set; }
    public bool Ativo { get; set; }
    public Categoria Categoria { get; set; }
}