[LICENSE__BADGE]: https://img.shields.io/github/license/Fernanda-Kipper/Readme-Templates?style=for-the-badge
[.Net]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[Postgres]: https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white
[Docker]: https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white

<h1 align="center" style="font-weight: bold;">TaskHub 💻</h1>

![license][LICENSE__BADGE]
![.NET]
![Postgres]
![Docker]

<details open="open">
<summary>Counteudo</summary>
  
- [1. Objetivo](#objetivo)
- [2. Tecnologias e Ferramentas](#tecnologia)
- [3. Estrutura da Solução (Planejada)](#estrutura)
- [4. Como Executar?](#executar)
    - [4.1 - Pré-requisitos](#requisitos)
    - [4.2 - Clonar o repositório](#clonar)
    - [4.3 - Executando a Aplicação](#executando)
    - [4.4 - Banco de Dados](#banco)
    - [4.5 - Acessando a documentação da API](#documentacao)
    - [4.6 - Parando a Aplicação](#parar)
    - [4.7 - Removendo Containers e Volumes](#remover)
    - [Observação](#obs)

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
├── TaskHub.Application      → Casos de uso e regras de negócio (Services, DTOs, Mappers), Validators
├── TaskHub.Domain           → Entidades e interfaces de domínio (Entities, Enums, Contracts)
├── TaskHub.Infrastructure   → Acesso a dados, configuração do EF Core e Identity
└── TaskHub.Tests            → Testes unitários e de integração
```

Cada camada será isolada e seguirá o padrão **Clean Architecture**, garantindo baixo acoplamento e fácil manutenção.

---

<h2 id="executar">4. Como Executar?</h2>

Esta aplicação foi containerizada utilizando Docker para facilitar a execução do projeto sem a necessidade de instalar manualmente todas as dependências no ambiente local.

Com isso, basta ter o Docker instalado para executar toda a aplicação (API + banco de dados + migrations).

<h3 id="requisitos">4.1 - Pré-requisitos</h3>

Antes de iniciar, certifique-se de que possui instalado em sua máquina:

- Docker
- Docker Compose

Verifique se estão instalados executando:

```bash
docker --version
docker compose version
```
<h3 id="clonar">4.2 - Clonar o Repositório</h3>

Clone o repositório utilizando o Git:

```bash
git clone https://github.com/WilliansVPF/TaskHub.git
```

Entre na pasta do projeto:

```bash
cd TaskHub
```

Alternativamente, você também pode baixar o projeto diretamente pelo GitHub:

&emsp; 1. Clique em Code <br>
&emsp; 2. Clique em Download ZIP <br>
&emsp; 3. Extraia o arquivo <br>
&emsp; 4. Abra um terminal dentro da pasta do projeto <br>

<h3 id="executando">4.3 - Executando a Aplicação</h3>

Com o Docker em execução, execute o comando abaixo na raiz do projeto:

```bash
docker compose --profile prod up --build
```

Esse comando irá:

&emsp; 1. Criar o container do **PostgreSQL** <br>
&emsp; 2. Aguardar o banco ficar disponível <br>
&emsp; 3. Executar automaticamente as **migrations do Entity Framework** <br>
&emsp; 4. Subir a **API** <br>

Todo o ambiente será criado automaticamente.

<h3 id="banco">4.4 - Banco de Dados</h3>

O projeto utiliza PostgreSQL executando em container.

Configurações principais:

| Configuração | Valor |
|------------|-------------|
| Banco | TaskHubDB |
| Usuário | root |
| Senha | root |
| Porta | 5432 |

Os dados do banco são persistidos através de **volume Docker**.

<h3 id="documentacao">4.5 - Acessando a documentação da API</h3>

Após a aplicação subir, a API estará disponível em:

```bash
http://localhost:5000
```
A documentação interativa do Swagger pode ser acessada em:

```bash
http://localhost:5000/swagger
```

Através dela é possível:

- visualizar todos os endpoints
- testar as requisições diretamente pelo navegador
- visualizar os modelos de dados

<h3 id="parar">4.6 - Parando a Aplicação</h3>

Para parar os containers pressione:

```bash
CTRL + C
```

<h3 id="remover">4.7 - Removendo Containers e Volumes</h3>

Caso queira parar e remover completamente os containers criados:

```bash
docker compose --profile prod down
```

Se quiser remover também os volumes (apagando os dados do banco):

```bash
docker compose --profile prod down -v
```

<h4 id="obs">Observação</h4>

Ao subir novamente o ambiente, as migrations serão executadas automaticamente garantindo que o banco esteja sempre atualizado.
