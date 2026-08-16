using Franquias.Api.Data;
using Franquias.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RelatoriosController : ControllerBase
{
    private readonly AppDbContext _context;

    public RelatoriosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("ranking-unidades")]
    public async Task<ActionResult<IEnumerable<RankingUnidadeDto>>> ObterRankingUnidades()
    {
        var vendas = await _context.Vendas
            .Include(v => v.Unidade)
            .ToListAsync();

        var ranking = vendas
            .GroupBy(v => new { v.UnidadeId, v.Unidade.NomeFantasia })
            .Select(g => new RankingUnidadeDto(
                g.Key.UnidadeId,
                g.Key.NomeFantasia,
                g.Sum(v => v.ValorTotal)
            ))
            .OrderByDescending(r => r.FaturamentoTotal)
            .ToList();

        return Ok(ranking);
    }

    [HttpGet("estoque-critico")]
    public async Task<ActionResult<IEnumerable<EstoqueCriticoDto>>> ObterEstoqueCritico()
    {
        var critico = await _context.Estoques
            .Include(e => e.Unidade)
            .Include(e => e.Produto)
            .Where(e => e.QuantidadeAtual <= e.QuantidadeMinima)
            .Select(e => new EstoqueCriticoDto(
                e.UnidadeId,
                e.Unidade.NomeFantasia,
                e.Produto.Nome,
                e.QuantidadeAtual,
                e.QuantidadeMinima
            ))
            .ToListAsync();

        return Ok(critico);
    }
}