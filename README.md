# 🏢 Franquias.Api

API REST desenvolvida em C# com .NET 8 para gestão centralizada de franqueados, produtos, vendas, estoque, suporte e relatórios.

Projeto desenvolvido para a disciplina de Desenvolvimento Back-end (2026) - UNINTER.

---

## 📌 Visão Geral

A aplicação foi criada para centralizar as operações essenciais de uma rede de franquias, permitindo:

- cadastro e gestão de unidades franqueadas;
- autenticação de usuários com JWT;
- controle de produtos e estoque;
- registro de vendas e itens vendidos;
- acompanhamento de chamados de suporte;
- geração de relatórios e indicadores operacionais.

---

## 🚀 Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT (autenticação)
- Swagger / OpenAPI

---

## 🧩 Estrutura do Projeto

```text
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
│   ├── ChamadoDTOs.cs
│   ├── ProdutoDTOs.cs
│   ├── RelatorioDTOs.cs
│   ├── UnidadeDTOs.cs
│   ├── VendaDTOs.cs
│   └── ViaCepDTOs.cs
├── Migrations/
│   ├── 20260816162228_InitialCreate.cs
│   ├── 20260816162228_InitialCreate.Designer.cs
│   ├── 20261018172049_AdicionarEnderecoUnidade.cs
│   ├── 20261018172049_AdicionarEnderecoUnidade.Designer.cs
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
├── Repositories/
├── Services/
│   ├── AuthService.cs
│   ├── VendaService.cs
│   └── ViaCepService.cs
├── appsettings.json
├── appsettings.Development.json
├── Franquias.Api.csproj
├── Franquias.Api.http
├── Franquias.Api.slnx
├── Program.cs
├── README.md
├── WeatherForecast.cs
├── .gitignore
├── .gitattributes
└── obj/
```

---

## 🛠️ Pré-requisitos

Antes de executar o projeto, certifique-se de ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- Git

---

## ▶️ Como Executar

### 1. Clonar o repositório

```bash
git clone https://github.com/elnvna/backend-franquias-api.git
cd backend-franquias-api
```

### 2. Restaurar dependências

```bash
dotnet restore
```

### 3. Configurar a conexão com o banco

Edite o arquivo `appsettings.json` e ajuste a string de conexão do PostgreSQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=franquias_db;Username=postgres;Password=sua_senha"
  }
}
```

### 4. Aplicar as migrações

```bash
dotnet ef database update
```

### 5. Executar a API

```bash
dotnet run
```

A aplicação estará disponível em:

- https://localhost:5001
- http://localhost:5000

> Caso a porta seja diferente, confira as configurações em `Properties/launchSettings.json`.

---

## 📚 API e Funcionalidades

### Autenticação
- login de usuários;
- geração e validação de JWT.

### Produtos
- cadastro, consulta e atualização de produtos.

### Estoque
- controle de entradas, saídas e saldo de itens.

### Vendas
- registro de vendas e itens relacionados.

### Unidades
- gerenciamento de unidades franqueadas.

### Chamados
- suporte e atendimentos internos.

### Relatórios
- geração de dados operacionais e indicadores.

---

## 🧪 Documentação da API

Ao executar a aplicação, a documentação Swagger estará disponível em:

```text
https://localhost:5001/swagger
```
