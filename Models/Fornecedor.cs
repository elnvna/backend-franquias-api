namespace Franquias.Api.Models;

public class Fornecedor
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
}