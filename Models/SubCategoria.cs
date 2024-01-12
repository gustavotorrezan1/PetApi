namespace PetApi.Models;
public class SubCategoria
{
    public int SubCategoriaId { get; set; }
    public string? Nome { get; set; }
    public int Ativo { get; set; }
    public Categoria? Categoria { get; set; }
}