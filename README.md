# AutoPricing API

API REST desenvolvida em ASP.NET Core 8 para gerenciamento de veículos usados.

O projeto foi desenvolvido com foco em boas práticas no desenvolvimento Back-end, utilizando arquitetura em camadas, autenticação JWT, Entity Framework Core e SQL Server.

---

## Tecnologias utilizadas

- ASP.NET Core 8
- C#
- Entity Framework Core
- SQL Server
- JWT Authentication
- FluentValidation
- Swagger / OpenAPI
- Docker
- Git

---

## Funcionalidades

### Veículos

- Cadastro de veículos
- Listagem de veículos
- Busca por ID
- Atualização
- Remoção

### Recursos da API

- Autenticação JWT
- Cadastro e login de usuários
- Endpoints protegidos
- Validação de dados com FluentValidation
- Middleware global para tratamento de exceções
- Paginação
- Filtros
- Ordenação dinâmica
- Persistência com SQL Server
- Migrations com Entity Framework Core

---

## Como executar

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

---

## Arquitetura

O projeto está organizado em camadas para manter a separação de responsabilidades:

```
Controllers
Services
DTOs
Models
Validators
Middleware
Exceptions
Data
Configurations
```

---

## Tecnologias aplicadas

- Dependency Injection
- Entity Framework Core
- JWT Authentication
- FluentValidation
- Middleware
- REST API
- SQL Server
- Docker
- Swagger