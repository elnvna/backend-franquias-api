using Franquias.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EstoquesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EstoquesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("unidade/{unidadeId}")]
    public async Task<IActionResult> ObterEstoquePorUnidade(int unidadeId)
    {
        var estoques = await _context.Estoques
            .Include(e => e.Produto)
            .Where(e => e.UnidadeId == unidadeId)
            .Select(e => new
            {
                e.ProdutoId,
                Produto = e.Produto.Nome,
                e.QuantidadeAtual,
                e.QuantidadeMinima,
                AbaixoDoMinimo = e.QuantidadeAtual < e.QuantidadeMinima
            })
            .ToListAsync();

        return Ok(estoques);
    }
}