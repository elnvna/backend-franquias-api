using Franquias.Api.Models;
using Franquias.Api.Services;

namespace Franquias.Api.Data;

public static class SeedData
{
    public static async Task InicializarAsync(AppDbContext context)
    {
        if (context.Usuarios.Any()) return; // Banco já possui dados

        // 1. Criar Unidades
        var matriz = new UnidadeFranqueada
        {
            NomeFantasia = "Franquia Matriz SP",
            CNPJ = "11.222.333/0001-99",
            Cidade = "São Paulo",
            UF = "SP",
            Telefone = "(11) 99999-0000",
            PercentualRoyalty = 5.0m,
            Ativa = true
        };

        context.Unidades.Add(matriz);
        await context.SaveChangesAsync();

        // 2. Criar Usuários Padrão
        var admin = new Usuario
        {
            Nome = "Administrador Franqueadora",
            Email = "admin@franquia.com",
            SenhaHash = AuthService.GerarHashSenha("123456"),
            Perfil = PerfilUsuario.AdminFranqueadora,
            Ativo = true,
            UnidadeId = matriz.Id
        };

        context.Usuarios.Add(admin);

        // 3. Categorias e Produtos
        var catBebidas = new Categoria { Nome = "Bebidas" };
        context.Categorias.Add(catBebidas);
        await context.SaveChangesAsync();

        var prodCafe = new ProdutoServico
        {
            Nome = "Café Espresso Premium",
            Descricao = "Grãos selecionados 100% Arábica",
            PrecoBase = 8.50m,
            Ativo = true,
            CategoriaId = catBebidas.Id
        };

        context.Produtos.Add(prodCafe);
        await context.SaveChangesAsync();

        // 4. Estoque Inicial
        context.Estoques.Add(new Estoque
        {
            UnidadeId = matriz.Id,
            ProdutoId = prodCafe.Id,
            QuantidadeAtual = 100,
            QuantidadeMinima = 10
        });

        await context.SaveChangesAsync();
    }
}