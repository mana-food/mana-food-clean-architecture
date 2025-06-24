# Maná Food

## Estrutura de Pastas

```
mana-food-clean-architecture/
├── Core/
│   ├── ManaFood.Application/      # Camada de aplicação (casos de uso, serviços, validações, configurações)
│   │   ├── Configurations/
│   │   ├── Shared/               
│   │   └── UseCases/
│   └── ManaFood.Domain/           # Camada de domínio (entidades, interfaces, regras de negócio)
│   │   ├── Entidades/        
│   │   └── Interfaces/
├── Infrastructure/
│   └── ManaFood.Infrastructure/   # Infraestrutura (acesso a dados, repositórios, contexto do banco)
│       ├── Configurations/
│       └── Database/
│           ├── Configuration/
│           ├── Context/
│           └── Repositories/
├── Presentation/
│   └── ManaFood.WebAPI/           # Camada de apresentação (controllers, configuração da API)
│       ├── Controllers/
│       ├── Properties/
│       ├── appsettings.json
│       └── appsettings.Development.json
├── README.md
├── .gitignore
├── LICENSE
└── ManaFood.sln
```

## Descrição dos Principais Diretórios

- **Core/ManaFood.Application/**: Implementa os casos de uso da aplicação, validações, comportamentos compartilhados e configurações específicas da camada de aplicação.
- **Core/ManaFood.Domain/**: Contém as entidades de domínio, interfaces e regras de negócio puras, sem dependências externas.
- **Infrastructure/ManaFood.Infrastructure/**: Responsável pela implementação da infraestrutura, como acesso a banco de dados, repositórios, contexto do Entity Framework e configurações relacionadas à persistência.
- **Presentation/ManaFood.WebAPI/**: Camada de apresentação, onde ficam os controllers da API, configurações do ASP.NET Core, arquivos de configuração (appsettings) e propriedades do projeto.

---

## Como executar o projeto

### 1. Clonando o repositório

```sh
git clone https://github.com/mana-food/mana-food-clean-architecture.git
cd mana-food-clean-architecture
```

### 2. Executando localmente

Certifique-se de ter o [.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) instalado e um banco de dados MySQL rodando.

1. Execute a aplicação:

    ```sh
    dotnet run --project Presentation/ManaFood.WebAPI/ManaFood.WebAPI.csproj
    ```

### 3. Gerar migrations com EF Core

1. Instale a ferramenta do EF:

    ```sh
    dotnet tool install --global dotnet-ef
    ```

2. Gere a migration:

    ```sh
    dotnet ef migrations add NOME_DA_SUA_MIGRATION --project Infrastructure/ManaFood.Infrastructure --startup-project Presentation/ManaFood.WebAPI
    ```

### 4. Documentação Complementar

#### Documentação Notion:
```sh
https://chartreuse-fountain-62d.notion.site/203ce57501598031b488df683ec4c8dd?v=203ce57501598002923d000c738029fd&source=copy_link
```

#### Documentação MIRO:
```sh
https://miro.com/app/board/uXjVIHWEfCI=/
```

#### Vídeo Explicativo:
```sh
https://drive.google.com/file/d/1oNyHA0dApnlPXia9Ov5fdcj2vpKXEQVb/view?usp=sharing
```
