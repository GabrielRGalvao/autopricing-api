# AutoPricing API

API REST desenvolvida em ASP.NET Core 8 para gerenciamento e sugestão de preços de veículos usados.

Este projeto foi no desenvolvimento Back-end com C#, Entity Framework Core, SQL Server e Docker, simulando um cenário próximo ao de aplicações utilizadas no mercado.

## Tecnologias

- ASP.NET Core 8
- C#
- Entity Framework Core
- SQL Server
- Docker
- Swagger
- Git

## Funcionalidades

- ✅ Cadastro de veículos
- ✅ Listagem de veículos
- ✅ Busca por ID
- ✅ Atualização de veículos
- ✅ Remoção de veículos
- ✅ Persistência de dados com SQL Server
- ✅ Entity Framework Core + Migrations

## Executando o projeto

```bash
docker compose up -d

cd src/AutoPricing.Api

dotnet ef database update

dotnet run
```

A documentação da API estará disponível em:

```
http://localhost:5276/swagger
```

## Roadmap

Próximas funcionalidades:

- Validação de dados
- Testes unitários
- Paginação
- Filtros
- Ordenação
- Sugestão automática de preço de veículos

---
