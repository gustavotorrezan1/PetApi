namespace PetApi.Models;
public class SubCategoria
{
    public int SubCategoriaId { get; set; }
    public int Nome { get; set; }
    public int Ativo { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}