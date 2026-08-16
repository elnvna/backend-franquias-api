namespace Franquias.Api.Models;

public class Royalty
{
    public int Id { get; set; }
    public int UnidadeId { get; set; }
    public UnidadeFranqueada Unidade { get; set; } = null!;
    public string MesAnoReferencia { get; set; } = string.Empty;
    public decimal FaturamentoPeriodo { get; set; }
    public decimal ValorRoyalty { get; set; }
    public bool Pago { get; set; } = false;
}