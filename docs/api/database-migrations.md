# Guia de Migrações de Banco de Dados

Este documento descreve como criar e aplicar migrações no banco de dados PostgreSQL do Alertar App usando Entity Framework Core. Note que esta documentação se aplica apenas ao banco de dados principal PostgreSQL; as estruturas do Firebase Realtime Database e SQLite são gerenciadas separadamente conforme descrito no documento de [Arquitetura Híbrida de Bancos](hybrid-database-architecture.md).

## Ferramentas Necessárias

- .NET SDK 9.0 ou superior
- Entity Framework Core CLI

Para instalar a ferramenta de linha de comando do Entity Framework Core:

```bash
dotnet tool install --global dotnet-ef
```

## Estrutura do Projeto

As migrações do Entity Framework Core são gerenciadas pelo projeto `AlertarApp.Infrastructure`, que contém:

- `Data/Context/AlertarDbContext.cs`: Contexto do EF Core
- `Data/Configurations/*.cs`: Configurações de mapeamento das entidades
- `Migrations/*.cs`: Arquivos de migração gerados automaticamente

## Criando uma Nova Migração

Para criar uma nova migração quando houver mudanças na estrutura do banco de dados:

```bash
# Navegue até a raiz do projeto
cd c:\Users\Adriellison.ferreira\Desktop\alertar-app\backend

# Crie uma nova migração
dotnet ef migrations add NomeDaMigracao --project AlertarApp.Infrastructure --startup-project AlertarApp.API
```

Substitua `NomeDaMigracao` por um nome descritivo que represente as alterações feitas (ex: `AddUserLastLoginField`).

## Aplicando Migrações

Para aplicar as migrações ao banco de dados:

```bash
# Navegue até a raiz do projeto
cd c:\Users\Adriellison.ferreira\Desktop\alertar-app\backend

# Aplique todas as migrações pendentes
dotnet ef database update --project AlertarApp.Infrastructure --startup-project AlertarApp.API
```

## Revertendo Migrações

Para reverter para uma migração específica:

```bash
# Reverter para uma migração específica (ou para antes da primeira)
dotnet ef database update NomeDaMigracao --project AlertarApp.Infrastructure --startup-project AlertarApp.API
```

Para reverter a migração mais recente:

```bash
# Reverter uma migração para trás
dotnet ef database update PreviousMigrationName --project AlertarApp.Infrastructure --startup-project AlertarApp.API
```

## Criando Migração Inicial

Para criar a migração inicial que estabelece o esquema do banco de dados:

```bash
dotnet ef migrations add InitialCreate --project AlertarApp.Infrastructure --startup-project AlertarApp.API
```

## Boas Práticas

1. **Versionamento**: Sempre inclua os arquivos de migração no controle de versão.
2. **Ambiente de Desenvolvimento**: Crie e teste migrações em ambiente de desenvolvimento antes de aplicá-las em produção.
3. **Backup**: Faça backup do banco de dados antes de aplicar migrações em ambiente de produção.
4. **Testes**: Verifique se as migrações não causam perda de dados existentes.

## Criando Scripts SQL a partir de Migrações

Para gerar um script SQL a partir das migrações (útil para ambientes de produção):

```bash
dotnet ef migrations script --project AlertarApp.Infrastructure --startup-project AlertarApp.API --output migration_script.sql
```

Para gerar um script a partir de uma migração específica até outra:

```bash
dotnet ef migrations script MigracaoInicial MigracaoFinal --project AlertarApp.Infrastructure --startup-project AlertarApp.API --output migration_script.sql
```

## Troubleshooting

### Erros Comuns

1. **"O banco de dados já contém um objeto chamado..."**
   - Causa: Tentativa de criar um objeto que já existe no banco de dados.
   - Solução: Reverter para uma migração anterior ou ajustar o modelo.

2. **"A relação de tabela já existe..."**
   - Causa: Conflito na criação de relações entre tabelas.
   - Solução: Verifique as configurações de relacionamento nos arquivos de configuração.

3. **"Não é possível remover a coluna porque há dependências..."**
   - Causa: Tentativa de remover uma coluna que tem restrições ou é referenciada por outras tabelas.
   - Solução: Remova primeiro as dependências ou use uma abordagem diferente.
