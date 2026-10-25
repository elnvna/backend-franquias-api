namespace Franquias.Api.DTOs;

public record UnidadeCriacaoDto(
    string NomeFantasia,
    string CNPJ,
    string CEP,
    string Numero,
    string? Complemento,
    string Telefone,
    decimal PercentualRoyalty
);

public record UnidadeAtualizacaoDto(
    string NomeFantasia,
    string CNPJ,
    string CEP,
    string Numero,
    string? Complemento,
    string Telefone,
    decimal PercentualRoyalty,
    bool Ativa
);

public record UnidadeResponseDto(
    int Id,
    string NomeFantasia,
    string CNPJ,
    string CEP,
    string Logradouro,
    string Numero,
    string Complemento,
    string Bairro,
    string Cidade,
    string UF,
    string Telefone,
    decimal PercentualRoyalty,
    bool Ativa,
    DateTime DataInicio
);