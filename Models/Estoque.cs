namespace Franquias.Api.Models;

public class Estoque
{
    public int Id { get; set; }
    public int UnidadeId { get; set; }
    public UnidadeFranqueada Unidade { get; set; } = null!;

    public int ProdutoId { get; set; }
    public ProdutoServico Produto { get; set; } = null!;

    public int QuantidadeAtual { get; set; }
    public int QuantidadeMinima { get; set; }
}