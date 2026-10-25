using System.Text.Json.Serialization;

namespace Franquias.Api.DTOs;

public class ViaCepResponseDto
{
    [JsonPropertyName("cep")]
    public string? CEP { get; set; }

    [JsonPropertyName("logradouro")]
    public string? Logradouro { get; set; }

    [JsonPropertyName("bairro")]
    public string? Bairro { get; set; }

    [JsonPropertyName("localidade")]
    public string? Cidade { get; set; }

    [JsonPropertyName("uf")]
    public string? UF { get; set; }

    [JsonPropertyName("erro")]
    public bool Erro { get; set; }
}