using Franquias.Api.Data;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Franquias.Api.Services;
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
    private readonly ViaCepService _viaCepService;

    public UnidadesController(
        AppDbContext context,
        ViaCepService viaCepService)
    {
        _context = context;
        _viaCepService = viaCepService;
    }

    // GET: api/Unidades
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnidadeResponseDto>>> ObterTodas()
    {
        var unidades = await _context.Unidades
            .Select(u => new UnidadeResponseDto(
                u.Id,
                u.NomeFantasia,
                u.CNPJ,
                u.CEP,
                u.Logradouro,
                u.Numero,
                u.Complemento,
                u.Bairro,
                u.Cidade,
                u.UF,
                u.Telefone,
                u.PercentualRoyalty,
                u.Ativa,
                u.DataInicio))
            .ToListAsync();

        return Ok(unidades);
    }

    // GET: api/Unidades/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UnidadeResponseDto>> ObterPorId(int id)
    {
        var unidade = await _context.Unidades
            .Where(u => u.Id == id)
            .Select(u => new UnidadeResponseDto(
                u.Id,
                u.NomeFantasia,
                u.CNPJ,
                u.CEP,
                u.Logradouro,
                u.Numero,
                u.Complemento,
                u.Bairro,
                u.Cidade,
                u.UF,
                u.Telefone,
                u.PercentualRoyalty,
                u.Ativa,
                u.DataInicio))
            .FirstOrDefaultAsync();

        if (unidade == null)
            return NotFound(new { mensagem = "Unidade não encontrada." });

        return Ok(unidade);
    }

    // POST: api/Unidades
    [HttpPost]
    [Authorize(Roles = "AdminFranqueadora")]
    public async Task<ActionResult<UnidadeResponseDto>> Criar(
        [FromBody] UnidadeCriacaoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.NomeFantasia))
            return BadRequest(new { mensagem = "Nome da unidade é obrigatório." });

        if (string.IsNullOrWhiteSpace(dto.CNPJ))
            return BadRequest(new { mensagem = "CNPJ é obrigatório." });

        if (string.IsNullOrWhiteSpace(dto.CEP))
            return BadRequest(new { mensagem = "CEP é obrigatório." });

        if (string.IsNullOrWhiteSpace(dto.Numero))
            return BadRequest(new { mensagem = "Número do endereço é obrigatório." });

        if (dto.PercentualRoyalty < 0 || dto.PercentualRoyalty > 100)
            return BadRequest(new
            {
                mensagem = "Percentual de royalty deve estar entre 0 e 100."
            });

        if (await _context.Unidades.AnyAsync(u => u.CNPJ == dto.CNPJ))
            return BadRequest(new
            {
                mensagem = "Já existe uma unidade cadastrada com este CNPJ."
            });

        ViaCepResponseDto? endereco;

        try
        {
            endereco = await _viaCepService.ConsultarCepAsync(dto.CEP);
        }
        catch (HttpRequestException)
        {
            return StatusCode(502, new
            {
                mensagem = "Não foi possível consultar o serviço ViaCEP."
            });
        }

        if (endereco == null)
            return BadRequest(new
            {
                mensagem = "CEP inválido ou não encontrado."
            });

        var unidade = new UnidadeFranqueada
        {
            NomeFantasia = dto.NomeFantasia.Trim(),
            CNPJ = dto.CNPJ.Trim(),
            CEP = endereco.CEP ?? dto.CEP,
            Logradouro = endereco.Logradouro ?? string.Empty,
            Numero = dto.Numero.Trim(),
            Complemento = dto.Complemento?.Trim() ?? string.Empty,
            Bairro = endereco.Bairro ?? string.Empty,
            Cidade = endereco.Cidade ?? string.Empty,
            UF = endereco.UF ?? string.Empty,
            Telefone = dto.Telefone.Trim(),
            PercentualRoyalty = dto.PercentualRoyalty,
            Ativa = true,
            DataInicio = DateTime.UtcNow
        };

        _context.Unidades.Add(unidade);
        await _context.SaveChangesAsync();

        var response = CriarResponse(unidade);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = unidade.Id },
            response);
    }

    // PUT: api/Unidades/1
    [HttpPut("{id:int}")]
    [Authorize(Roles = "AdminFranqueadora")]
    public async Task<ActionResult<UnidadeResponseDto>> Atualizar(
        int id,
        [FromBody] UnidadeAtualizacaoDto dto)
    {
        var unidade = await _context.Unidades
            .FirstOrDefaultAsync(u => u.Id == id);

        if (unidade == null)
            return NotFound(new { mensagem = "Unidade não encontrada." });

        if (string.IsNullOrWhiteSpace(dto.NomeFantasia))
            return BadRequest(new { mensagem = "Nome da unidade é obrigatório." });

        if (string.IsNullOrWhiteSpace(dto.CNPJ))
            return BadRequest(new { mensagem = "CNPJ é obrigatório." });

        if (string.IsNullOrWhiteSpace(dto.CEP))
            return BadRequest(new { mensagem = "CEP é obrigatório." });

        if (string.IsNullOrWhiteSpace(dto.Numero))
            return BadRequest(new { mensagem = "Número do endereço é obrigatório." });

        if (dto.PercentualRoyalty < 0 || dto.PercentualRoyalty > 100)
            return BadRequest(new
            {
                mensagem = "Percentual de royalty deve estar entre 0 e 100."
            });

        var cnpjDuplicado = await _context.Unidades
            .AnyAsync(u => u.CNPJ == dto.CNPJ && u.Id != id);

        if (cnpjDuplicado)
            return BadRequest(new
            {
                mensagem = "Já existe outra unidade cadastrada com este CNPJ."
            });

        ViaCepResponseDto? endereco;

        try
        {
            endereco = await _viaCepService.ConsultarCepAsync(dto.CEP);
        }
        catch (HttpRequestException)
        {
            return StatusCode(502, new
            {
                mensagem = "Não foi possível consultar o serviço ViaCEP."
            });
        }

        if (endereco == null)
            return BadRequest(new
            {
                mensagem = "CEP inválido ou não encontrado."
            });

        unidade.NomeFantasia = dto.NomeFantasia.Trim();
        unidade.CNPJ = dto.CNPJ.Trim();
        unidade.CEP = endereco.CEP ?? dto.CEP;
        unidade.Logradouro = endereco.Logradouro ?? string.Empty;
        unidade.Numero = dto.Numero.Trim();
        unidade.Complemento = dto.Complemento?.Trim() ?? string.Empty;
        unidade.Bairro = endereco.Bairro ?? string.Empty;
        unidade.Cidade = endereco.Cidade ?? string.Empty;
        unidade.UF = endereco.UF ?? string.Empty;
        unidade.Telefone = dto.Telefone.Trim();
        unidade.PercentualRoyalty = dto.PercentualRoyalty;
        unidade.Ativa = dto.Ativa;

        await _context.SaveChangesAsync();

        return Ok(CriarResponse(unidade));
    }

    // DELETE: api/Unidades/1
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "AdminFranqueadora")]
    public async Task<IActionResult> Excluir(int id)
    {
        var unidade = await _context.Unidades
            .FirstOrDefaultAsync(u => u.Id == id);

        if (unidade == null)
            return NotFound(new { mensagem = "Unidade não encontrada." });

        _context.Unidades.Remove(unidade);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static UnidadeResponseDto CriarResponse(
        UnidadeFranqueada unidade)
    {
        return new UnidadeResponseDto(
            unidade.Id,
            unidade.NomeFantasia,
            unidade.CNPJ,
            unidade.CEP,
            unidade.Logradouro,
            unidade.Numero,
            unidade.Complemento,
            unidade.Bairro,
            unidade.Cidade,
            unidade.UF,
            unidade.Telefone,
            unidade.PercentualRoyalty,
            unidade.Ativa,
            unidade.DataInicio);
    }
}