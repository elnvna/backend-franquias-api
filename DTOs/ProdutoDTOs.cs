namespace Franquias.Api.DTOs;

public record ProdutoCriacaoDto(
    string Nome,
    string Descricao,
    decimal PrecoBase,
    int CategoriaId,
    int? FornecedorId
);

public record ProdutoResponseDto(
    int Id,
    string Nome,
    string Descricao,
    decimal PrecoBase,
    bool Ativo,
    string CategoriaNome,
    string? FornecedorNome
);