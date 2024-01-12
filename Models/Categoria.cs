namespace PetApi.Models;
public class Categoria
{
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public int Ativo { get; set; } = 1;
    
}