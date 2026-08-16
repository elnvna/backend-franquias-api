namespace Franquias.Api.DTOs;

public record UnidadeCriacaoDto(
    string NomeFantasia,
    string CNPJ,
    string Cidade,
    string UF,
    string Telefone,
    decimal PercentualRoyalty
);

public record UnidadeResponseDto(
    int Id,
    string NomeFantasia,
    string CNPJ,
    string Cidade,
    string UF,
    string Telefone,
    decimal PercentualRoyalty,
    bool Ativa,
    DateTime DataInicio
);