# Estrutura do Banco de Dados

## Visão Geral

Este documento descreve o modelo de dados e a arquitetura híbrida de banco de dados utilizada pelo Alertar App. A solução combina PostgreSQL, Firebase Realtime Database e SQLite para oferecer um equilíbrio ideal entre performance, disponibilidade offline e sincronização em tempo real.

## Arquitetura Híbrida de Banco de Dados

O Alertar App utiliza uma arquitetura de banco de dados híbrida com três tecnologias complementares:

### PostgreSQL (Banco de Dados Principal)

- **Hospedagem**: Via Neon, Supabase ou Railway
- **Propósito**: Armazenamento persistente e centralizado de todos os dados da aplicação
- **Benefícios**: Integridade referencial, transações ACID, consultas complexas
- **Dados armazenados**: Informações de usuários, histórico completo de alertas, contatos de confiança, configurações

### Firebase Realtime Database

- **Propósito**: Sincronização em tempo real para dados críticos
- **Benefícios**: Baixa latência, atualizações em tempo real, sincronização automática
- **Dados armazenados**: Alertas ativos, localização em tempo real, status dos usuários

### SQLite (Dispositivo Local)

- **Propósito**: Cache local e funcionalidade offline no aplicativo Flutter
- **Benefícios**: Acesso a dados mesmo sem conexão, redução de latência, economia de dados móveis
- **Dados armazenados**: Cópia local dos dados essenciais, alertas não sincronizados, configurações do usuário

Esta arquitetura híbrida proporciona:

- Controle e segurança dos dados através do PostgreSQL
- Sincronização instantânea para dados críticos com Firebase
- Funcionamento offline através do SQLite
- Escalabilidade com custos controlados

## Fluxo de Dados na Arquitetura Híbrida

### Sincronização entre Bancos

1. **Criação e Atualização de Dados**:
   - Dados críticos (alertas, localização) são gravados simultaneamente no SQLite local e enviados ao Firebase
   - Firebase propaga as atualizações para outros dispositivos em tempo real
   - Dados são persistidos no PostgreSQL para armazenamento permanente

2. **Recuperação de Dados**:
   - Dados frequentes são carregados do cache SQLite para acesso imediato
   - Atualizações em tempo real vêm do Firebase Realtime DB
   - Dados históricos ou completos são solicitados do PostgreSQL quando necessário

3. **Modo Offline**:
   - Dados são armazenados localmente no SQLite
   - Quando a conexão é restabelecida, os dados são sincronizados com o Firebase e PostgreSQL

### Estratégia de Persistência por Tipo de Dado

| Tipo de Dado | PostgreSQL | Firebase | SQLite |
|-------------|:---------:|:-------:|:------:|
| Dados de usuário | ✓ | ✓ (básico) | ✓ |
| Alertas ativos | ✓ | ✓ | ✓ |
| Histórico de alertas | ✓ | - | ✓ (parcial) |
| Localização em tempo real | - | ✓ | ✓ |
| Contatos de confiança | ✓ | ✓ | ✓ |
| Logs e Analytics | ✓ | - | - |

## Modelo Entidade-Relacionamento (PostgreSQL)

O banco de dados PostgreSQL é composto por três entidades principais:

1. **Users**: Armazena informações dos usuários
2. **Alerts**: Registra os alertas emitidos pelos usuários
3. **TrustedContacts**: Contém os contatos de confiança cadastrados pelos usuários

### Diagrama ER

```marmeid
+----------------+       +----------------+       +-------------------+
|     Users      |       |     Alerts     |       |  TrustedContacts  |
+----------------+       +----------------+       +-------------------+
| PK Id          |---+   | PK Id          |       | PK Id             |
| Name           |   |   | FK UserId      |-------| FK UserId         |
| Email          |   +-->| Title          |       | Name              |
| Phone          |       | Description    |       | Phone             |
| DocumentId     |       | Latitude       |       | Email             |
| PinHash        |       | Longitude      |       | SendSms           |
| PinSalt        |       | CreatedAt      |       | SendEmail         |
| CreatedAt      |       | Status         |       | CreatedAt         |
| LastLogin      |       +----------------+       | IsActive          |
| IsActive       |                                +-------------------+
+----------------+
```

## Detalhamento das Tabelas

### Users

Armazena informações dos usuários do aplicativo.

| Coluna      | Tipo           | Descrição                                 | Restrições                    |
|-------------|----------------|-------------------------------------------|-------------------------------|
| Id          | UNIQUEIDENTIFIER | Identificador único do usuário           | PK, NOT NULL                  |
| Name        | NVARCHAR(100)  | Nome completo do usuário                  | NOT NULL                      |
| Email       | NVARCHAR(100)  | Endereço de email do usuário              | NOT NULL, UNIQUE              |
| Phone       | NVARCHAR(20)   | Número de telefone do usuário             | NOT NULL                      |
| DocumentId  | NVARCHAR(14)   | CPF do usuário                            | NOT NULL, UNIQUE              |
| PinHash     | NVARCHAR(128)  | Hash do PIN de acesso                     | NOT NULL                      |
| PinSalt     | NVARCHAR(36)   | Salt usado para gerar o hash do PIN       | NOT NULL                      |
| CreatedAt   | DATETIME2      | Data e hora de criação do registro        | NOT NULL, DEFAULT(GETUTCDATE())|
| LastLogin   | DATETIME2      | Data e hora do último login               | NULL                          |
| IsActive    | BIT            | Indica se o usuário está ativo            | NOT NULL, DEFAULT(1)          |

### Alerts

Registra os alertas emitidos pelos usuários.

| Coluna      | Tipo           | Descrição                                 | Restrições                    |
|-------------|----------------|-------------------------------------------|-------------------------------|
| Id          | UNIQUEIDENTIFIER | Identificador único do alerta           | PK, NOT NULL                  |
| UserId      | UNIQUEIDENTIFIER | Referência ao ID do usuário             | FK, NOT NULL                  |
| Title       | NVARCHAR(100)  | Título do alerta                          | NOT NULL                      |
| Description | NVARCHAR(500)  | Descrição detalhada do alerta             | NULL                          |
| Latitude    | FLOAT          | Latitude da localização do alerta         | NOT NULL                      |
| Longitude   | FLOAT          | Longitude da localização do alerta        | NOT NULL                      |
| CreatedAt   | DATETIME2      | Data e hora de criação do alerta          | NOT NULL, DEFAULT(GETUTCDATE())|
| Status      | NVARCHAR(20)   | Status do alerta (ACTIVE, RESOLVED, CANCELED) | NOT NULL, DEFAULT('ACTIVE')  |

### TrustedContacts

Contém os contatos de confiança cadastrados pelos usuários para receber notificações de alerta.

| Coluna      | Tipo           | Descrição                                 | Restrições                    |
|-------------|----------------|-------------------------------------------|-------------------------------|
| Id          | UNIQUEIDENTIFIER | Identificador único do contato          | PK, NOT NULL                  |
| UserId      | UNIQUEIDENTIFIER | Referência ao ID do usuário             | FK, NOT NULL                  |
| Name        | NVARCHAR(100)  | Nome do contato                           | NOT NULL                      |
| Phone       | NVARCHAR(20)   | Número de telefone do contato             | NOT NULL                      |
| Email       | NVARCHAR(100)  | Endereço de email do contato              | NULL                          |
| SendSms     | BIT            | Indica se deve enviar SMS durante alertas | NOT NULL, DEFAULT(1)          |
| SendEmail   | BIT            | Indica se deve enviar email durante alertas | NOT NULL, DEFAULT(0)        |
| CreatedAt   | DATETIME2      | Data e hora de criação do registro        | NOT NULL, DEFAULT(GETUTCDATE())|
| IsActive    | BIT            | Indica se o contato está ativo            | NOT NULL, DEFAULT(1)          |

## Índices

### Users

- PK_Users (Id)
- UQ_Users_Email (Email)
- UQ_Users_DocumentId (DocumentId)

### Alerts

- PK_Alerts (Id)
- IX_Alerts_UserId (UserId)
- IX_Alerts_Status (Status)
- IX_Alerts_CreatedAt (CreatedAt)

### TrustedContacts

- PK_TrustedContacts (Id)
- IX_TrustedContacts_UserId (UserId)
- IX_TrustedContacts_IsActive (IsActive)

## Chaves Estrangeiras

- FK_Alerts_Users: Alerts.UserId → Users.Id (ON DELETE CASCADE)
- FK_TrustedContacts_Users: TrustedContacts.UserId → Users.Id (ON DELETE CASCADE)

## Script SQL de Criação

```sql
-- Criação da tabela Users
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    DocumentId NVARCHAR(14) NOT NULL,
    PinHash NVARCHAR(128) NOT NULL,
    PinSalt NVARCHAR(36) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    LastLogin DATETIME2 NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT UQ_Users_Email UNIQUE (Email),
    CONSTRAINT UQ_Users_DocumentId UNIQUE (DocumentId)
);

-- Criação da tabela Alerts
CREATE TABLE Alerts (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
    CONSTRAINT FK_Alerts_Users FOREIGN KEY (UserId) 
        REFERENCES Users(Id) ON DELETE CASCADE
);

-- Criação da tabela TrustedContacts
CREATE TABLE TrustedContacts (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NULL,
    SendSms BIT NOT NULL DEFAULT 1,
    SendEmail BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_TrustedContacts_Users FOREIGN KEY (UserId) 
        REFERENCES Users(Id) ON DELETE CASCADE
);

-- Criação dos índices
CREATE INDEX IX_Alerts_UserId ON Alerts(UserId);
CREATE INDEX IX_Alerts_Status ON Alerts(Status);
CREATE INDEX IX_Alerts_CreatedAt ON Alerts(CreatedAt);

CREATE INDEX IX_TrustedContacts_UserId ON TrustedContacts(UserId);
CREATE INDEX IX_TrustedContacts_IsActive ON TrustedContacts(IsActive);
```
