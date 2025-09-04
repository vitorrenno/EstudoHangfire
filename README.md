# 🚀 Projeto de Estudos com Hangfire e ASP.NET Core

![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet)
![C#](https://img.shields.io/badge/C%23-12-green)
![Hangfire](https://img.shields.io/badge/Hangfire-1.8-orange)
![Arquitetura](https://img.shields.io/badge/Arquitetura-Limpa-informational)

Bem-vindo a este repositório de estudos! O objetivo principal deste projeto é explorar de forma prática a utilização do **Hangfire** para o gerenciamento de tarefas em background, dentro de uma aplicação ASP.NET Core construída com uma **arquitetura limpa e escalável**.

---

## 📖 Conceitos Principais

Este projeto foi construído sobre dois pilares fundamentais: o uso do Hangfire como ferramenta e a aplicação de uma Arquitetura Limpa para organização do código.

### 1. O que é o Hangfire?

O Hangfire é uma biblioteca open-source para .NET que simplifica a criação, o processamento e o gerenciamento de tarefas em background (background jobs). Em vez de fazer uma tarefa demorada (como enviar um e-mail ou processar um relatório) travar a resposta de uma requisição web, podemos "enfileirar" essa tarefa para ser executada em segundo plano por um processo separado.

> **Por que usar?** Para melhorar a performance e a experiência do usuário em aplicações web, delegando operações lentas e pesadas para serem executadas de forma assíncrona, sem bloquear o fluxo principal.

Neste projeto, o Hangfire é configurado para usar o **SQL Server** como armazenamento, onde ele persiste o estado de todas as tarefas, garantindo que nenhuma seja perdida mesmo que a aplicação seja reiniciada.

### 2. Arquitetura do Projeto: Clean Architecture

Para garantir que o projeto seja organizado, testável e fácil de manter, foi utilizada uma abordagem baseada na **Arquitetura Limpa (Clean Architecture)**, separando o código em camadas de responsabilidade.

> **Por que usar?** Para criar um código com baixo acoplamento e alta coesão. A lógica de negócio (o "coração" da aplicação) não depende de detalhes de infraestrutura (como o banco de dados ou a API). Isso torna o sistema mais flexível a mudanças e muito mais fácil de testar.

O fluxo de dependência segue a regra:

```
┌──────────────────┐     ┌──────────────────────┐     ┌────────────────────────┐
│ EstudoHangfire.Api │ ──> │ EstudoHangfire.Application │ <── │ EstudoHangfire.Infrastructure │
└──────────────────┘     └──────────────────────┘     └────────────────────────┘
 (Apresentação)            (Lógica de Negócio)                (Infraestrutura)
```

- A **Api** depende da **Application**.
- A **Infrastructure** depende da **Application**.
- A **Application** não depende de ninguém.

---

## 📂 Estrutura de Pastas e Projetos

A solução está organizada em projetos distintos, cada um com uma responsabilidade clara, todos dentro da pasta `src/`.

```bash
EstudoHangfire/
├── EstudoHangfire.sln
└── src/
    ├── EstudoHangfire.Api/
    ├── EstudoHangfire.Application/
    └── EstudoHangfire.Infrastructure/
```

### `src/EstudoHangfire.Api`

- **Camada de Apresentação (Presentation Layer)**.
- **Responsabilidade:** Ponto de entrada da aplicação. Lida com requisições HTTP, rotas e serialização de dados (JSON). Não contém regras de negócio.
- **Pastas:**
  - `Controllers/`: Define os endpoints da API que recebem as requisições.
  - `DTOs/`: (Data Transfer Objects) Classes que modelam os dados de entrada e saída da API.
  - `Program.cs`: Arquivo de inicialização onde configuramos todos os serviços, injeção de dependência e o pipeline do ASP.NET Core, incluindo a inicialização do Hangfire e seu Dashboard.

### `src/EstudoHangfire.Application`

- **Camada de Lógica de Negócio (Application Layer)**.
- **Responsabilidade:** O núcleo da aplicação. Contém a lógica de negócio, as interfaces que definem os "contratos" e as definições dos Jobs do Hangfire.
- **Pastas:**
  - `Interfaces/`: Contratos para os serviços de infraestrutura (ex: `IEmailService`). Define "o que" fazer, mas não "como".
  - `Services/`: Orquestra a lógica de negócio e enfileira os jobs do Hangfire.
  - `Jobs/`: As classes que contêm a lógica a ser executada em background pelo Hangfire.

### `src/EstudoHangfire.Infrastructure`

- **Camada de Infraestrutura (Infrastructure Layer)**.
- **Responsabilidade:** Implementações concretas de tudo que é externo à aplicação: acesso a banco de dados, envio de e-mails, consumo de outras APIs, etc.
- **Pastas:**
  - `Services/`: Implementações das interfaces da camada de `Application` (ex: `SmtpEmailService` que implementa `IEmailService`).

---

## 🛠️ Como Executar o Projeto

Siga os passos abaixo para configurar e executar o ambiente de desenvolvimento.

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) ou superior.
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (Express, Developer ou LocalDB).

### Passos

1.  **Clone o repositório:**

    ```bash
    git clone [https://github.com/SEU_USUARIO/EstudoHangfire.git](https://github.com/SEU_USUARIO/EstudoHangfire.git)
    cd EstudoHangfire
    ```

2.  **Configure a Connection String:**

    - Abra o arquivo `src/EstudoHangfire.Api/appsettings.json`.
    - Altere a `HangfireConnection` para apontar para o seu banco de dados SQL Server. O padrão usa o LocalDB.

    ```json
    "ConnectionStrings": {
      "HangfireConnection": "Server=(localdb)\\mssqllocaldb;Database=HangfireDB;Trusted_Connection=True;"
    }
    ```

3.  **Crie o Banco de Dados:**

    - Certifique-se de que o banco de dados `HangfireDB` (ou o nome que você escolheu) exista no seu servidor SQL. O Hangfire criará as tabelas necessárias automaticamente na primeira execução.

4.  **Execute a Aplicação:**

    - Navegue até a pasta da API e execute o comando `dotnet run`.

    ```bash
    cd src/EstudoHangfire.Api
    dotnet run
    ```

5.  **Acesse as UIs:**
    - **Swagger (API):** `https://localhost:[PORTA]/swagger`
    - **Hangfire Dashboard:** `https://localhost:[PORTA]/hangfire`

---

## ✅ Objetivos de Aprendizado

Ao explorar este repositório, você poderá aprender sobre:

- [x] Configuração inicial do Hangfire em um projeto ASP.NET Core.
- [x] Implementação de uma arquitetura em camadas (Clean Architecture).
- [x] Separação de responsabilidades entre projetos.
- [x] Uso de Injeção de Dependência para desacoplar as camadas.
- [x] Como enfileirar diferentes tipos de jobs (Fire-and-forget, Delayed, etc.).
- [x] Uso do Dashboard do Hangfire para monitorar as tarefas.
