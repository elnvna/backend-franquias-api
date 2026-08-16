namespace Franquias.Api.DTOs;

public record ItemVendaCriacaoDto(int ProdutoId, int Quantidade);

public record VendaCriacaoDto(
    int UnidadeId,
    List<ItemVendaCriacaoDto> Itens
);

public record ItemVendaResponseDto(
    string ProdutoNome,
    int Quantidade,
    decimal PrecoUnitario,
    decimal Subtotal
);

public record VendaResponseDto(
    int Id,
    int UnidadeId,
    DateTime DataVenda,
    decimal ValorTotal,
    List<ItemVendaResponseDto> Itens
);