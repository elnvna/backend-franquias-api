namespace Franquias.Api.DTOs;

public record RankingUnidadeDto(
    int UnidadeId,
    string NomeFantasia,
    decimal FaturamentoTotal
);

public record EstoqueCriticoDto(
    int UnidadeId,
    string UnidadeNome,
    string ProdutoNome,
    int QuantidadeAtual,
    int QuantidadeMinima
);

public record IndicadoresGeraisDto(
    decimal FaturamentoTotalRede,
    decimal RoyaltiesTotaisGerados,
    int TotalVendas,
    int ChamadosAbertos
);