# 🏢 Franquias.Api - Sistema de Gestão Centralizada de Franquias

API REST desenvolvida em **C# com .NET 8** e **PostgreSQL** para gestão centralizada de unidades franqueadas, controle de estoque, vendas, royalties e suporte.

Projeto desenvolvido para a disciplina de **Desenvolvimento Back-end (2026)** - UNINTER.

---

## 🚀 Tecnologias Utilizadas

- **.NET 8** (ASP.NET Core Web API)
- **PostgreSQL** (Banco de Dados Relacional)
- **Entity Framework Core 8** (ORM Code-First)
- **JWT (JSON Web Tokens)** (Autenticação e Autorização)
- **Swagger / OpenAPI** (Documentação Interativa)

---

## 🛠️ Como Executar o Projeto

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) instalado e rodando

### Passo a Passo

1. **Clonar o Repositório:**
   ```bash
   git clone [https://github.com/elnvna/backend-franquias-api.git](https://github.com/elnvna/backend-franquias-api.git)
   cd backend-franquias-api

### Estrutura

   Franquias.Api/
├── Controllers/
│   ├── AuthController.cs
│   ├── ChamadosController.cs
│   ├── EstoquesController.cs
│   ├── ProdutosController.cs
│   ├── RelatoriosController.cs
│   ├── UnidadesController.cs
│   └── VendasController.cs
├── Data/
│   ├── AppDbContext.cs
│   └── SeedData.cs
├── DTOs/
│   ├── AuthDTOs.cs
│   ├── ProdutoDTOs.cs
│   ├── RelatorioDTOs.cs
│   ├── UnidadeDTOs.cs
│   └── VendaDTOs.cs
├── Migrations/
│   ├── 20260816162228_InitialCreate.cs
│   ├── 20260816162228_InitialCreate.Designer.cs
│   └── AppDbContextModelSnapshot.cs
├── Models/
│   ├── Categoria.cs
│   ├── ChamadoSuporte.cs
│   ├── Enums.cs
│   ├── Estoque.cs
│   ├── Fornecedor.cs
│   ├── ItemVenda.cs
│   ├── ProdutoServico.cs
│   ├── Royalty.cs
│   ├── UnidadeFranqueada.cs
│   ├── Usuario.cs
│   └── Venda.cs
├── Properties/
│   └── launchSettings.json
├── Services/
│   ├── AuthService.cs
│   └── VendaService.cs
├── .gitattributes
├── .gitignore
├── Program.cs
├── README.md
├── appsettings.json
├── appsettings.Development.json
├── Franquias.Api.csproj
├── Franquias.Api.http
└── Franquias.Api.slnx