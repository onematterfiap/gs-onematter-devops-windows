# OneMatter - Portal do Recrutador

================================

> **Global Solution 2025/2 - FIAP** > **Tema:** O Futuro do Trabalho
>
> **Alinhamento ODS:** ODS 10 (Redução das Desigualdades)

O **OneMatter** é uma plataforma de recrutamento ético projetada para combater o viés inconsciente nos processos seletivos. Este repositório contém o **Portal do Recrutador**, uma aplicação web desenvolvida em **ASP.NET Core MVC** que permite a gestão de vagas e a realização de uma triagem inicial 100% anônima.

---

## Equipe

-   **Arthur Thomas Mariano de Souza (RM 561061)**
-   **Davi Cavalcanti Jorge (RM 559873)**
-   **Mateus da Silveira Lima (RM 559728)**

---

## Visão Geral do Projeto

O objetivo principal deste portal é separar a avaliação técnica da identidade pessoal do candidato.

1.  **Gestão de Vagas:** O recrutador cria e gere oportunidades de emprego.
2.  **Triagem Anônima (Core):** O sistema oculta propositalmente dados sensíveis (Nome, Género, Foto) durante a primeira fase. O recrutador vê apenas um ID neutro (ex: "Candidato #123"), as _Skills_ e a _Experiência_.
3.  **Aprovação por Mérito:** Apenas se o candidato for aprovado com base nas suas competências é que avança para a próxima fase (Teste Prático IoT).

---

## Decisões de Arquitetura

A solução foi construída seguindo o padrão **MVC (Model-View-Controller)** com .NET 8, focada na separação de responsabilidades e segurança.

-   **Framework:** ASP.NET Core 8.0 MVC.
-   **Banco de Dados:** Oracle Database (via `Oracle.EntityFrameworkCore`).
-   **ORM:** Entity Framework Core (Code-First Migrations).
-   **Autenticação:** ASP.NET Core Identity (Gestão de utilizadores e acessos).
-   **Front-end:** Razor Views, Bootstrap 5 e Identidade Visual Personalizada.

### Estrutura da Solução

-   **Models (Domínio):** Entidades ricas (`Job`, `Candidate`, `JobApplication`) com regras de negócio e invariantes.
-   **ViewModels (Apresentação):** Inclui o `AnonymousCandidateViewModel`, chave para a funcionalidade de privacidade.
-   **Controllers:** `JobsController` (CRUD de vagas) e `ApplicationsController` (lógica de anonimização e aprovação).

---

# DEPLOY NO AZURE (Containerização)

A aplicação está empacotada em um contêiner Docker para garantir portabilidade e facilitar o deploy em serviços de hospedagem modernos como o Azure App Service ou Azure Container Instances (ACI).

## URL DE ACESSO PÚBLICO

A aplicação está acessível no IP público do ambiente Azure na porta `8080`.

**URL Base:** **`http://68.211.123.73:8080`**

## Configuração de Contêineres

A estratégia de deploy utiliza um **Build Multi-Stage** no `Dockerfile` para gerar uma imagem de execução leve e um `docker-compose.yml` para injetar variáveis de ambiente críticas.

### Detalhes do Dockerfile

-   **Build Otimizado:** Utiliza o `dotnet sdk` para compilar e o `dotnet aspnet` (JRE) para o runtime, resultando em uma imagem final reduzida.
-   **Segurança:** O contêiner é executado com um usuário sem privilégios (`USER userapp`).
-   **Exposição de Porta:** A porta `8080` é exposta, conforme definido na URL de deploy.

```Dockerfile

# ... (Fase de Build)

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
ENTRYPOINT ["dotnet", "OneMatter.dll"]
```

### Detalhes do Docker Compose

O `docker-compose.yml` é essencial para injetar a **Connection String do Oracle** como variável de ambiente no contêiner, uma prática de segurança recomendada (Secret Injection):

```yaml
version: '3.8'
services:
onematter-portal:
image: onematter-portal-recrutador
build:
context: .
dockerfile: Dockerfile
ports: - "8080:8080"
environment: # A String de Conexão é injetada aqui (ou via User Secrets no Azure) - ConnectionStrings\_\_DefaultConnection=User Id=SEU_USER;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL
```

---

## Configuração e Execução Local

### Pré-requisitos

-   .NET SDK 8.0
-   Acesso a uma instância do Oracle Database.

### Como Configurar a Conexão

Por questões de segurança, a String de Conexão com o Oracle **NÃO** está commitada. Configure-a usando as seguintes opções:

### Opção 1: User Secrets (Recomendado)

No terminal, na pasta do projeto, execute:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "User Id=SEU_USER;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL"
```

---

### Opção 2: Arquivo `appsettings.Development.json`

Crie um arquivo chamado **appsettings.Development.json** na raiz do projeto e cole:

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "User Id=SEU_USER;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL"
    }
}
```

---

## Como Rodar o Projeto

Siga estes passos para executar a aplicação pela primeira vez:

### 1. Clonar o Repositório

```bash
git clone https://github.com/onematterfiap/gs-onematter-dotnet.git
cd gs-onematter-dotnet
```

### 2. Aplicar Migrations (Criar Banco de Dados)

O projeto utiliza EF Core Migrations. Execute o comando abaixo para criar as tabelas (`Jobs`, `Candidates`, `JobApplications`, `AspNetUsers`, etc.) no seu banco Oracle:

```bash
dotnet ef database update
```

### 3. Executar a Aplicação

```bash
dotnet run
```

Acesse o portal em: `http://localhost:5000` ou `https://localhost:7000` (conforme indicado no terminal).

---

## Navegação e Funcionalidades

O portal é dividido em áreas protegidas por autenticação.

### 1. Acesso e Autenticação

-   **Registro/Login:** Acesse através dos botões no canto superior direito ou na Landing Page.
-   **Rota:** /Identity/Account/Login

### 2. Gestão de Vagas (CRUD)

-   **Listar Vagas:** Menu "Vagas" ou botão "Gerir as Minhas Vagas".
    -   **Rota:** GET /Jobs
-   **Criar Vaga:** Botão "Publicar Nova Vaga".
    -   **Rota:** GET/POST /Jobs/Create
-   **Editar/Excluir:** Disponível nas ações de cada vaga.

### 3. Triagem Anônima (O Core do Projeto)

-   **Ver Candidatos:** Na lista de vagas, clique no botão verde **"Ver Candidatos"**.
    -   **Rota:** GET /Applications/Index/{id} (onde {id} é o ID da Vaga).
-   **Visualização:** O recrutador vê apenas "Candidato #ID", Skills e Experiência.
-   **Aprovar:** Ao clicar em "Aprovar para Teste", o sistema altera o status do candidato e o libera para a fase de IoT.
    -   **Rota:** POST /Applications/Approve

---

## Exemplos de Uso

### Fluxo Principal:

1.  **Login:** O recrutador entra na plataforma.

2.  **Dashboard:** Vê a Landing Page com os valores da empresa.

3.  **Criar Vaga:** Registra uma nova vaga "Desenvolvedor Java Senior".

4.  **Triagem:**

    -   O recrutador clica em "Ver Candidatos" na vaga criada.

    -   O sistema apresenta uma lista:
        -   _Candidato #21_: "C#, .NET Core, SQL, Azure" (Status: Aprovado_Etapa1)
        -   _Candidato #24_: "Java, Spring Boot, Angular, MySQL" (Status: Pendente) -> **[APROVAR]**
    -   O recrutador analisa as skills (sem ver nomes) e aprova o #24.
    -   O status muda para "Aprovado para Teste".

_Global Solution 2025 - FIAP_
