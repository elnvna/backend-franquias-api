using Franquias.Api.Data;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UnidadesController : ControllerBase
{
    private readonly AppDbContext _context;

    public UnidadesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnidadeResponseDto>>> ObterTodas()
    {
        var unidades = await _context.Unidades
            .Select(u => new UnidadeResponseDto(
                u.Id, u.NomeFantasia, u.CNPJ, u.Cidade, u.UF, u.Telefone, u.PercentualRoyalty, u.Ativa, u.DataInicio))
            .ToListAsync();

        return Ok(unidades);
    }

    [HttpPost]
    [Authorize(Roles = "AdminFranqueadora")]
    public async Task<ActionResult<UnidadeResponseDto>> Criar([FromBody] UnidadeCriacaoDto dto)
    {
        if (await _context.Unidades.AnyAsync(u => u.CNPJ == dto.CNPJ))
            return BadRequest(new { mensagem = "Já existe uma unidade cadastrada com este CNPJ." });

        var unidade = new UnidadeFranqueada
        {
            NomeFantasia = dto.NomeFantasia,
            CNPJ = dto.CNPJ,
            Cidade = dto.Cidade,
            UF = dto.UF,
            Telefone = dto.Telefone,
            PercentualRoyalty = dto.PercentualRoyalty,
            Ativa = true,
            DataInicio = DateTime.UtcNow
        };

        _context.Unidades.Add(unidade);
        await _context.SaveChangesAsync();

        var response = new UnidadeResponseDto(
            unidade.Id, unidade.NomeFantasia, unidade.CNPJ, unidade.Cidade, unidade.UF, unidade.Telefone, unidade.PercentualRoyalty, unidade.Ativa, unidade.DataInicio);

        return CreatedAtAction(nameof(ObterTodas), new { id = unidade.Id }, response);
    }
}