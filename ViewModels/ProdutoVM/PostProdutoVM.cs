namespace PetApi.ViewModels.ProdutoVM;

public class PostProdutoVM{

    public string? Nome { get; set; }
    public double PrecoCusto { get; set; }
    public double PrecoVenda { get; set; }   
    public int UnidadeMedidaId { get; set; }
    public int SubCategoriaId { get; set; }
    public int CategoriaId { get; set; }
}