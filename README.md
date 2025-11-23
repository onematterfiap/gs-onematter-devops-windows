OneMatter - Portal do Recrutador
================================

> **Global Solution 2025/2 - FIAP** > **Tema:** O Futuro do Trabalho
>
> **Alinhamento ODS:** ODS 10 (Redução das Desigualdades)

O **OneMatter** é uma plataforma de recrutamento ético projetada para combater o viés inconsciente nos processos seletivos. Este repositório contém o **Portal do Recrutador**, uma aplicação web desenvolvida em **ASP.NET Core MVC** que permite a gestão de vagas e a realização de uma triagem inicial 100% anônima.

Equipe
------

-   **Arthur Thomas Mariano de Souza (RM 561061)**

-   **Davi Cavalcanti Jorge (RM 559873)**

-   **Mateus da Silveira Lima (RM 559728)**

Visão Geral
-----------

O objetivo principal deste portal é separar a avaliação técnica da identidade pessoal do candidato.

1.  **Gestão de Vagas:** O recrutador cria e gere oportunidades de emprego.

2.  **Triagem Anônima (Core):** O sistema oculta propositalmente dados sensíveis (Nome, Género, Foto) durante a primeira fase. O recrutador vê apenas um ID neutro (ex: "Candidato #123"), as *Skills* e a *Experiência*.

3.  **Aprovação por Mérito:** Apenas se o candidato for aprovado com base nas suas competências é que avança para a próxima fase (Teste Prático IoT).

Decisões de Arquitetura
-----------------------

A solução foi construída seguindo o padrão **MVC (Model-View-Controller)** com .NET 8, focada na separação de responsabilidades e segurança.

-   **Framework:** ASP.NET Core 8.0 MVC.

-   **Banco de Dados:** Oracle Database (via Oracle.EntityFrameworkCore).

-   **ORM:** Entity Framework Core (Code-First Migrations).

-   **Autenticação:** ASP.NET Core Identity (Gestão de utilizadores e acessos).

-   **Front-end:** Razor Views + Bootstrap 5 + Identidade Visual Personalizada ("Inter" font & Paleta OneMatter).

### Estrutura da Solução

-   **Models (Domínio):** Entidades ricas (Job, Candidate, JobApplication) com regras de negócio e invariantes (ex: não permitir editar vagas fechadas).

-   **ViewModels (Camada de Apresentação):**

    -   AnonymousCandidateViewModel: O componente chave que garante a privacidade, transportando apenas dados técnicos para a View.

    -   CreateJobViewModel: Para validação de entrada de dados (Data Annotations).

-   **Controllers:**

    -   JobsController: Implementa o CRUD completo de vagas.

    -   ApplicationsController: Gere a lógica de anonimização (query com .Select()) e a aprovação de candidatos.

Configuração e Variáveis de Ambiente
------------------------------------

Por questões de segurança, a **String de Conexão** com o banco de dados Oracle **NÃO** está commitada neste repositório. Você deve configurá-la localmente.

### Pré-requisitos

-   .NET SDK 8.0

-   Acesso a uma instância do Oracle Database.

### Como Configurar a Conexão

Você tem duas opções para configurar o acesso ao banco:


### Opção 1: User Secrets (Recomendado)

No terminal, na pasta do projeto, execute:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "User Id=SEU_USER;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL"
```

---

### Opção 2: Arquivo `appsettings.Development.json`

Crie um arquivo chamado **appsettings.Development.json** na raiz do projeto (ao lado do `appsettings.json`) e cole:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=SEU_USER;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL"
  }
}
```
Aqui está a seção "Como Rodar o Projeto" corrigida, com a sintaxe Markdown limpa e os comandos bash arrumados:

-----

## Como Rodar o Projeto

Siga estes passos para executar a aplicação pela primeira vez:

### 1\. Clonar o Repositório

```bash
git clone https://github.com/onematterfiap/gs-onematter-dotnet.git
cd gs-onematter-dotnet
```

### 2\. Aplicar Migrations (Criar Banco de Dados)

O projeto utiliza EF Core Migrations. Execute o comando abaixo para criar as tabelas (`Jobs`, `Candidates`, `JobApplications`, `AspNetUsers`, etc.) no seu banco Oracle:

```bash
dotnet ef database update
```

### 3\. Executar a Aplicação

```bash
dotnet run
```

Acesse o portal em: `http://localhost:5000` ou `https://localhost:7000` (conforme indicado no terminal).

Navegação e Funcionalidades
------------------------------

O portal é dividido em áreas protegidas por autenticação.

### 1\. Acesso e Autenticação

-   **Registro/Login:** Acesse através dos botões no canto superior direito ou na Landing Page.

-   **Rota:** /Identity/Account/Login

### 2\. Gestão de Vagas (CRUD)

-   **Listar Vagas:** Menu "Vagas" ou botão "Gerir as Minhas Vagas".

    -   **Rota:** GET /Jobs

-   **Criar Vaga:** Botão "Publicar Nova Vaga".

    -   **Rota:** GET/POST /Jobs/Create

-   **Editar/Excluir:** Disponível nas ações de cada vaga.

### 3\. Triagem Anónima (O Core do Projeto)

-   **Ver Candidatos:** Na lista de vagas, clique no botão verde **"Ver Candidatos"**.

    -   **Rota:** GET /Applications/Index/{id} (onde {id} é o ID da Vaga).

-   **Visualização:** O recrutador vê apenas "Candidato #ID", Skills e Experiência.

-   **Aprovar:** Ao clicar em "Aprovar para Teste", o sistema altera o status do candidato e liberta-o para a fase de IoT.

    -   **Rota:** POST /Applications/Approve

Exemplos de Uso
------------------

### Fluxo Principal:

1.  **Login:** O recrutador entra na plataforma.

![Login](https://github.com/user-attachments/assets/e400e155-c9fc-4a25-9bec-aada9d6e3b1e)

2.  **Dashboard:** Vê a Landing Page com os valores da empresa.

![Dashboard](https://github.com/user-attachments/assets/2f99fb0e-0c1f-4a1e-99cf-cc577167fb45)

3.  **Criar Vaga:** Registra uma nova vaga "Desenvolvedor Java Senior".

![Criar](https://github.com/user-attachments/assets/a3bd9e9f-8f1f-4e5d-9a8b-4875289d1c00)

7.  **Triagem:**

    -   O recrutador clica em "Ver Candidatos" na vaga criada.
      ![Ver Candidatos](https://github.com/user-attachments/assets/2ff08c69-1f11-477f-8389-efba486718a1)

    -   O sistema apresenta uma lista:

        -   *Candidato #21*: "C#, .NET Core, SQL, Azure" (Status: Aprovado_Etapa1)

        -   *Candidato #24*: "Java, Spring Boot, Angular, MySQL" (Status: Pendente) -> **[APROVAR]**

    -   O recrutador analisa as skills (sem ver nomes) e aprova o #24.

    -   O status muda para "Aprovado para Teste".

*Global Solution 2025 - FIAP*
