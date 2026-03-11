[LICENSE__BADGE]: https://img.shields.io/github/license/Fernanda-Kipper/Readme-Templates?style=for-the-badge
[.Net]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[Postgres]: https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white

<h1 align="center" style="font-weight: bold;">TaskHub 💻</h1>

![license][LICENSE__BADGE]
![.NET]
![Postgres]

<details open="open">
<summary>Counteudo</summary>
  
- [1. Objetivo](#objetivo)
- [2. Tecnologias e Ferramentas](#tecnologia)
- [3. Estrutura da Solução (Planejada)](#estrutura)

---

<h2 id="objetivo">1. Objetivo</h2>

O objetivo do **TaskHub** é criar uma API completa de gerenciamento de tarefas e projetos, permitindo:

- Cadastro e autenticação de usuários (Identity + JWT);
- Criação e gerenciamento de projetos e tarefas;
- Definição de status, prioridade e responsáveis;
- Registro de atividades e histórico de alterações;
- Operações seguras e escaláveis via **Docker** e **Nginx**.

Além disso, o projeto busca **servir como estudo prático** para consolidação de conhecimentos em:

- Arquitetura limpa;
- Boas práticas de design;
- Documentação de API com Swagger.
<!-- - Testes automatizados; -->
<!-- - Integração contínua (CI/CD); -->

---

<h2 id="tecnologia">2. Tecnologias e Ferramentas</h2>

| Categoria | Tecnologia |
|------------|-------------|
| **Linguagem** | C# (.NET 10) |
| **Framework Web** | ASP.NET Core Web API |
| **ORM** | Entity Framework Core |
| **Banco de Dados** | PostgreSQL |
| **Validação** | FluentValidation |
| **Autenticação e Autorização** | ASP.NET Identity + JWT |
| **Containerização** | Docker |
| **Servidor Proxy / Balanceamento** | Nginx |
| **Documentação** | Swagger / OpenAPI |
| **Logs e Monitoramento** | Serilog *(planejado)* |
| **Testes** | xUnit + Moq *(planejado)* |

---

<h2 id="estrutura">3. Estrutura da Solução (Planejada)</h2>

A solução do **TaskHub** será dividida em múltiplos projetos para promover separação de responsabilidades e escalabilidade.

```
TaskHub.sln
│
├── TaskHub.Api              → Camada de apresentação (Controllers, Middlewares, etc.)
├── TaskHub.Application      → Casos de uso e regras de negócio (Services, DTOs, Mappers)
├── TaskHub.Domain           → Entidades e interfaces de domínio (Entities, Enums, Contracts)
├── TaskHub.Infrastructure   → Acesso a dados, configuração do EF Core e Identity
├── TaskHub.CrossCutting     → Validações, helpers, configurações comuns e dependências
└── TaskHub.Tests            → Testes unitários e de integração
```

Cada camada será isolada e seguirá o padrão **Clean Architecture**, garantindo baixo acoplamento e fácil manutenção.

---
