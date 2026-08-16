using Franquias.Api.Data;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Api.Services;

public class VendaService
{
    private readonly AppDbContext _context;

    public VendaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<VendaResponseDto> RegistrarVendaAsync(VendaCriacaoDto dto)
    {
        // 1. Regra de Negócio: Unidade inativa não pode registrar venda
        var unidade = await _context.Unidades.FindAsync(dto.UnidadeId);
        if (unidade == null || !unidade.Ativa)
        {
            throw new InvalidOperationException("A unidade informada não existe ou está inativa.");
        }

        if (dto.Itens == null || !dto.Itens.Any())
        {
            throw new InvalidOperationException("Uma venda precisa ter ao menos um item.");
        }

        decimal valorTotalVenda = 0;
        var itensVendaList = new List<ItemVenda>();

        // 2. Processar itens, validar estoque e dar baixa
        foreach (var itemDto in dto.Itens)
        {
            var produto = await _context.Produtos.FindAsync(itemDto.ProdutoId);
            if (produto == null || !produto.Ativo)
            {
                throw new InvalidOperationException($"Produto ID {itemDto.ProdutoId} não cadastrado ou inativo.");
            }

            var estoque = await _context.Estoques
                .FirstOrDefaultAsync(e => e.UnidadeId == dto.UnidadeId && e.ProdutoId == itemDto.ProdutoId);

            if (estoque == null || estoque.QuantidadeAtual < itemDto.Quantidade)
            {
                throw new InvalidOperationException($"Estoque insuficiente para o produto '{produto.Nome}'.");
            }

            // Decrementa o estoque
            estoque.QuantidadeAtual -= itemDto.Quantidade;

            decimal subtotal = itemDto.Quantidade * produto.PrecoBase;
            valorTotalVenda += subtotal;

            itensVendaList.Add(new ItemVenda
            {
                ProdutoId = produto.Id,
                Quantidade = itemDto.Quantidade,
                PrecoUnitario = produto.PrecoBase,
                Subtotal = subtotal
            });
        }

        var venda = new Venda
        {
            UnidadeId = dto.UnidadeId,
            DataVenda = DateTime.UtcNow,
            ValorTotal = valorTotalVenda,
            Itens = itensVendaList
        };

        _context.Vendas.Add(venda);
        await _context.SaveChangesAsync();

        return new VendaResponseDto(
            venda.Id,
            venda.UnidadeId,
            venda.DataVenda,
            venda.ValorTotal,
            venda.Itens.Select(i => new ItemVendaResponseDto(
                _context.Produtos.Find(i.ProdutoId)?.Nome ?? "",
                i.Quantidade,
                i.PrecoUnitario,
                i.Subtotal)).ToList()
        );
    }
}