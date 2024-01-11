namespace PetApi.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? Nome { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoVenda { get; set; }
        public string? CodBarras { get; set; }
        public int Ativo { get; set; } = 1;
        public UnidadeMedida? UnidadeMedida { get; set; }
        public SubCategoria? SubCategoria { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
