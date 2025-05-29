# Backend - alertar-app

Este diretório contém a API do aplicativo ALERTAR, desenvolvida com .NET seguindo os princípios SOLID e Clean Architecture.

## Arquitetura

O backend segue uma arquitetura em camadas:

- **AlertarApp.Domain**: Entidades e regras de negócio
- **AlertarApp.Application**: Casos de uso e lógica de aplicação
- **AlertarApp.Infrastructure**: Implementações técnicas (banco de dados, serviços externos)
- **AlertarApp.API**: Controladores REST e configuração da aplicação
- **AlertarApp.Tests**: Testes unitários e de integração

## Tecnologias

- **.NET 9.0**: Framework de desenvolvimento
- **Entity Framework Core**: ORM para acesso a dados
- **FluentValidation**: Validação de entrada
- **JWT**: Autenticação baseada em tokens
- **Swagger/OpenAPI**: Documentação da API
- **xUnit**: Framework de testes

## Funcionalidades principais

- **Autenticação**: Login com CPF/PIN e JWT
- **Gerenciamento de alertas**: Criar, visualizar, resolver e cancelar alertas
- **Compartilhamento de localização**: Enviar localização em tempo real
- **Contatos de confiança**: Cadastrar e gerenciar contatos para receber alertas

## Como rodar

### Requisitos

- .NET SDK 9.0 ou superior
- SQL Server (ou LocalDB)

### Comandos

```bash
# Restaurar dependências
dotnet restore

# Aplicar migrações do banco de dados
dotnet ef database update --project AlertarApp.Infrastructure --startup-project AlertarApp.API

# Compilar o projeto
dotnet build

# Executar a API
cd AlertarApp.API
dotnet run
```

A API estará disponível em `https://localhost:5001` ou `http://localhost:5000`, com a documentação Swagger em `/swagger`.

## Estrutura do banco de dados

O banco de dados possui as seguintes entidades principais:

- **Users**: Usuários do aplicativo
- **Alerts**: Alertas emitidos pelos usuários
- **TrustedContacts**: Contatos de confiança para receber notificações
