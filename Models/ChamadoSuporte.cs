namespace Franquias.Api.Models;

public class ChamadoSuporte
{
    public int Id { get; set; }
    public int UnidadeId { get; set; }
    public UnidadeFranqueada Unidade { get; set; } = null!;
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public PrioridadeChamado Prioridade { get; set; }
    public StatusChamado Status { get; set; } = StatusChamado.Aberto;
    public DateTime DataAbertura { get; set; } = DateTime.UtcNow;
}