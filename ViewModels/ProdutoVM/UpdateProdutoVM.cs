namespace PetApi.ViewModels.CategoriaVM;

public class UpdateProdutoVM{
    public int Ativo { get; set; }
    public string? Nome { get; set; }
    public double PrecoCusto { get; set; }
    public double PrecoVenda { get; set; }
    public string? CodBarras { get; set; }
}