using Franquias.Api.Data;
using Franquias.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChamadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ChamadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var chamados = await _context.Chamados
            .Include(c => c.Unidade)
            .Select(c => new
            {
                c.Id,
                Unidade = c.Unidade.NomeFantasia,
                c.Titulo,
                Prioridade = c.Prioridade.ToString(),
                Status = c.Status.ToString(),
                c.DataAbertura
            })
            .ToListAsync();

        return Ok(chamados);
    }

    [HttpPost]
    public async Task<IActionResult> AbrirChamado([FromBody] ChamadoSuporte chamado)
    {
        _context.Chamados.Add(chamado);
        await _context.SaveChangesAsync();
        return Ok(chamado);
    }
}