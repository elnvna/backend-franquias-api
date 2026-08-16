using Microsoft.EntityFrameworkCore;
using Franquias.Api.Models;

namespace Franquias.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<UnidadeFranqueada> Unidades => Set<UnidadeFranqueada>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<ProdutoServico> Produtos => Set<ProdutoServico>();
    public DbSet<Estoque> Estoques => Set<Estoque>();
    public DbSet<Venda> Vendas => Set<Venda>();
    public DbSet<ItemVenda> ItensVenda => Set<ItemVenda>();
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();
    public DbSet<Royalty> Royalties => Set<Royalty>();
    public DbSet<ChamadoSuporte> Chamados => Set<ChamadoSuporte>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Índices únicos para evitar duplicidades
        modelBuilder.Entity<UnidadeFranqueada>()
            .HasIndex(u => u.CNPJ)
            .IsUnique();

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Fornecedor>()
            .HasIndex(f => f.CNPJ)
            .IsUnique();
    }
}