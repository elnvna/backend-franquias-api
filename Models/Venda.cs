namespace Franquias.Api.Models;

public class Venda
{
    public int Id { get; set; }
    public int UnidadeId { get; set; }
    public UnidadeFranqueada Unidade { get; set; } = null!;
    public DateTime DataVenda { get; set; } = DateTime.UtcNow;
    public decimal ValorTotal { get; set; }

    public ICollection<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
}