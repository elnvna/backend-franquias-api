namespace Franquias.Api.Models;

public class UnidadeFranqueada
{
    public int Id { get; set; }
    public string NomeFantasia { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string UF { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public decimal PercentualRoyalty { get; set; }
    public bool Ativa { get; set; } = true;
    public DateTime DataInicio { get; set; } = DateTime.UtcNow;

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public ICollection<Estoque> Estoques { get; set; } = new List<Estoque>();
    public ICollection<Venda> Vendas { get; set; } = new List<Venda>();
    public ICollection<Royalty> Royalties { get; set; } = new List<Royalty>();
    public ICollection<ChamadoSuporte> Chamados { get; set; } = new List<ChamadoSuporte>();
}