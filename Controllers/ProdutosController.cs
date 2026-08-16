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
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoResponseDto>>> ObterTodos()
    {
        var produtos = await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Fornecedor)
            .Select(p => new ProdutoResponseDto(
                p.Id, p.Nome, p.Descricao, p.PrecoBase, p.Ativo, p.Categoria.Nome, p.Fornecedor != null ? p.Fornecedor.Nome : null))
            .ToListAsync();

        return Ok(produtos);
    }

    [HttpPost]
    [Authorize(Roles = "AdminFranqueadora")]
    public async Task<ActionResult<ProdutoResponseDto>> Criar([FromBody] ProdutoCriacaoDto dto)
    {
        var produto = new ProdutoServico
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            PrecoBase = dto.PrecoBase,
            CategoriaId = dto.CategoriaId,
            FornecedorId = dto.FornecedorId,
            Ativo = true
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterTodos), new { id = produto.Id }, dto);
    }
}