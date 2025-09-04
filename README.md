🚀 Projeto de Estudos com Hangfire e ASP.NET CoreBem-vindo a este repositório de estudos! O objetivo principal deste projeto é explorar de forma prática a utilização do Hangfire para o gerenciamento de tarefas em background, dentro de uma aplicação ASP.NET Core construída com uma arquitetura limpa e escalável.📖 Conceitos PrincipaisEste projeto foi construído sobre dois pilares fundamentais: o uso do Hangfire como ferramenta e a aplicação de uma Arquitetura Limpa para organização do código.1. O que é o Hangfire?O Hangfire é uma biblioteca open-source para .NET que simplifica a criação, o processamento e o gerenciamento de tarefas em background (background jobs). Em vez de fazer uma tarefa demorada (como gerar um relatório) travar a resposta de uma requisição web, podemos "enfileirar" essa tarefa para ser executada em segundo plano.Por que usar? Para melhorar a performance e a experiência do usuário em aplicações web, delegando operações lentas e pesadas para serem executadas de forma assíncrona, sem bloquear o fluxo principal.Neste projeto, o Hangfire é configurado para usar o SQL Server como armazenamento, onde ele persiste o estado de todas as tarefas, garantindo que nenhuma seja perdida mesmo que a aplicação seja reiniciada.2. Arquitetura do Projeto: Clean ArchitecturePara garantir que o projeto seja organizado, testável e fácil de manter, foi utilizada uma abordagem baseada na Arquitetura Limpa (Clean Architecture), separando o código em camadas de responsabilidade.Por que usar? Para criar um código com baixo acoplamento e alta coesão. A lógica de negócio (o "coração" da aplicação) não depende de detalhes de infraestrutura. Isso torna o sistema mais flexível a mudanças e muito mais fácil de testar.O fluxo de dependência segue a regra:┌──────────────────┐     ┌──────────────────────┐     ┌────────────────────────┐

│ EstudoHangfire.Api │ ──> │ EstudoHangfire.Application │ <── │ EstudoHangfire.Infrastructure │

└──────────────────┘     └──────────────────────┘     └────────────────────────┘

 (Apresentação)            (Lógica de Negócio)                (Infraestrutura)

A Api depende da Application.A Infrastructure depende da Application.A Application não depende de ninguém.📂 Estrutura de Pastas e ProjetosA solução está organizada em projetos distintos, cada um com uma responsabilidade clara, todos dentro da pasta src/.EstudoHangfire/

├── EstudoHangfire.sln

└── src/

    ├── EstudoHangfire.Api/

    ├── EstudoHangfire.Application/

    └── EstudoHangfire.Infrastructure/

src/EstudoHangfire.ApiCamada de Apresentação (Presentation Layer).Responsabilidade: Ponto de entrada da aplicação. Lida com requisições HTTP e rotas.Pastas:Controllers/: Define os endpoints da API.DTOs/: (Data Transfer Objects) Classes que modelam os dados de entrada e saída da API.Program.cs: Arquivo de inicialização onde configuramos os serviços, injeção de dependência e o pipeline do ASP.NET Core, incluindo o Hangfire e seu Dashboard.src/EstudoHangfire.ApplicationCamada de Lógica de Negócio (Application Layer).Responsabilidade: O núcleo da aplicação. Contém a lógica de negócio, as interfaces e as definições dos Jobs do Hangfire.Pastas:Interfaces/: Contratos para os serviços de infraestrutura (ex: INotificacaoService). Define "o que" fazer, mas não "como".Services/: Orquestra a lógica de negócio e enfileira os jobs.Jobs/: As classes com a lógica a ser executada em background.src/EstudoHangfire.InfrastructureCamada de Infraestrutura (Infrastructure Layer).Responsabilidade: Implementações concretas de tudo que é externo à aplicação (acesso a banco de dados, consumo de APIs, etc.).Pastas:Services/: Implementações das interfaces da camada Application.🛠️ Como Executar o ProjetoSiga os passos abaixo para configurar e executar o ambiente de desenvolvimento.Pré-requisitos.NET 9 SDK ou superior.SQL Server (Express, Developer ou LocalDB).PassosClone o repositório:git clone \[https://github.com/SEU\_USUARIO/EstudoHangfire.git](https://github.com/SEU\_USUARIO/EstudoHangfire.git)

cd EstudoHangfire

Configure a Connection String:Abra o arquivo src/EstudoHangfire.Api/appsettings.json.Altere a HangfireConnection para apontar para o seu banco de dados SQL Server."ConnectionStrings": {

  "HangfireConnection": "Server=(localdb)\\\\mssqllocaldb;Database=HangfireDB;Trusted\_Connection=True;"

}

Crie o Banco de Dados:Certifique-se de que o banco de dados HangfireDB (ou o nome que você escolheu) exista. O Hangfire criará as tabelas necessárias na primeira execução.Execute a Aplicação:cd src/EstudoHangfire.Api

dotnet run

Acesse as UIs:OpenAPI (UI da API): https://localhost:\[PORTA]/swaggerHangfire Dashboard: https://localhost:\[PORTA]/hangfire✅ Objetivos de AprendizadoAo explorar este repositório, você poderá aprender sobre:\[x] Configuração inicial do Hangfire em um projeto ASP.NET Core.\[x] Implementação de uma arquitetura em camadas (Clean Architecture).\[x] Separação de responsabilidades entre projetos.\[x] Uso de Injeção de Dependência para desacoplar as camadas.\[x] Como enfileirar diferentes tipos de jobs (Fire-and-forget, Delayed, etc.).\[x] Uso do Dashboard do Hangfire para monitorar as tarefas.

