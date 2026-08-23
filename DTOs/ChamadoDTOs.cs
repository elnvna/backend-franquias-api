namespace Franquias.Api.DTOs;

public record ChamadoCriacaoDto(
    int UnidadeId,
    string Titulo,
    string Descricao,
    int Prioridade
);