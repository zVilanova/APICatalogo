# 📚 APICatalogo

API REST desenvolvida em ASP.NET Core para gerenciamento de catálogo de produtos e categorias.

O projeto foi criado com foco em aprendizado de desenvolvimento backend utilizando .NET, Entity Framework Core e integração com banco de dados MySQL.

---

## 🚀 Tecnologias Utilizadas

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- Swagger / Swashbuckle

---

## 📌 Funcionalidades

- CRUD de produtos
- CRUD de categorias
- Relacionamento entre entidades
- Integração com banco de dados MySQL
- Documentação automática com Swagger

---

## 🧩 Estrutura do Projeto

| Camada | Tecnologia |
|---|---|
| API | ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Banco de Dados | MySQL |
| Documentação | Swagger |

---

## 📡 Endpoints

### Categorias

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/categorias` | Lista categorias |
| GET | `/api/categorias/{id}` | Busca categoria por ID |
| POST | `/api/categorias` | Cria categoria |
| PUT | `/api/categorias/{id}` | Atualiza categoria |
| DELETE | `/api/categorias/{id}` | Remove categoria |

---

### Produtos

| Método | Endpoint | Descrição |
|---|---|---|
| GET | `/api/produtos` | Lista produtos |
| GET | `/api/produtos/{id}` | Busca produto por ID |
| POST | `/api/produtos` | Cria produto |
| PUT | `/api/produtos/{id}` | Atualiza produto |
| DELETE | `/api/produtos/{id}` | Remove produto |

---

## 🛠️ Como executar o projeto

### Clone o repositório

```bash
git clone https://github.com/zVilanova/APICatalogo.git
```

### Acesse a pasta do projeto

```bash
cd APICatalogo
```

### Configure a connection string

Edite o arquivo:

```bash
appsettings.json
```

### Execute as migrations

```bash
dotnet ef database update
```

### Execute o projeto

```bash
dotnet run
```

---

## 📖 Swagger

Após executar a aplicação, acesse:

```bash
https://localhost:7162/swagger
```

---

## 🎯 Objetivo do Projeto

O objetivo deste projeto foi praticar conceitos de desenvolvimento backend com ASP.NET Core, modelagem de entidades, relacionamento entre tabelas e construção de APIs REST.
