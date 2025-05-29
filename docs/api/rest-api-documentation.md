# API RESTful do Alertar App

## Visão Geral

Esta documentação descreve a API RESTful do Alertar App, seguindo as melhores práticas de design de APIs.

## Base URL

```http
https://api.alertarapp.com/v1
```

## Autenticação

A API utiliza autenticação JWT (JSON Web Token). Para acessar endpoints protegidos, inclua o token no cabeçalho HTTP:

```http
Authorization: Bearer {seu_token_jwt}
```

Para obter um token, use o endpoint de login.

## Formato das Respostas

Todas as respostas são retornadas no formato JSON com os seguintes padrões:

### Sucesso

```json
{
  "success": true,
  "data": { /* dados da resposta */ },
  "message": "Mensagem de sucesso"
}
```

### Erro

```json
{
  "success": false,
  "error": {
    "code": "ERROR_CODE",
    "message": "Mensagem descritiva do erro"
  }
}
```

## Códigos de Status HTTP

- `200 OK`: Requisição bem-sucedida
- `201 Created`: Recurso criado com sucesso
- `400 Bad Request`: Requisição inválida
- `401 Unauthorized`: Autenticação necessária
- `403 Forbidden`: Acesso negado
- `404 Not Found`: Recurso não encontrado
- `422 Unprocessable Entity`: Erro de validação
- `500 Internal Server Error`: Erro interno do servidor

## Paginação

Endpoints que retornam múltiplos itens são paginados. A resposta incluirá:

```json
{
  "success": true,
  "data": [ /* itens */ ],
  "pagination": {
    "total": 100,
    "count": 10,
    "perPage": 10,
    "currentPage": 1,
    "totalPages": 10,
    "links": {
      "next": "/endpoint?page=2",
      "previous": null
    }
  }
}
```

## Endpoints

### Autenticação

#### Login

```http
POST /auth/login
```

**Parâmetros do Corpo**

| Nome       | Tipo   | Descrição                                   |
|------------|--------|---------------------------------------------|
| documentId | string | Documento de identificação (CPF) do usuário |
| pin        | string | PIN de 4 a 6 dígitos                        |

**Exemplo de Requisição**

```json
{
  "documentId": "12345678900",
  "pin": "123456"
}
```

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "user": {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "name": "João Silva",
      "email": "joao.silva@email.com"
    }
  },
  "message": "Login realizado com sucesso"
}
```

### Usuários

#### Criar Usuário

```
POST /users
```

**Parâmetros do Corpo**

| Nome       | Tipo   | Descrição                                   |
|------------|--------|---------------------------------------------|
| name       | string | Nome completo do usuário                    |
| email      | string | Email do usuário                            |
| phone      | string | Telefone do usuário                         |
| documentId | string | Documento de identificação (CPF) do usuário |
| pin        | string | PIN de 4 a 6 dígitos                        |

**Exemplo de Requisição**

```json
{
  "name": "Maria Santos",
  "email": "maria.santos@email.com",
  "phone": "(11) 98765-4321",
  "documentId": "12345678901",
  "pin": "123456"
}
```

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174001",
    "name": "Maria Santos",
    "email": "maria.santos@email.com",
    "phone": "(11) 98765-4321",
    "documentId": "12345678901",
    "createdAt": "2025-05-29T12:00:00Z",
    "isActive": true
  },
  "message": "Usuário criado com sucesso"
}
```

#### Obter Usuário

```
GET /users/{id}
```

**Parâmetros da URL**

| Nome | Tipo   | Descrição                  |
|------|--------|----------------------------|
| id   | string | ID único (UUID) do usuário |

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174001",
    "name": "Maria Santos",
    "email": "maria.santos@email.com",
    "phone": "(11) 98765-4321",
    "documentId": "12345678901",
    "createdAt": "2025-05-29T12:00:00Z",
    "lastLogin": "2025-05-29T14:30:00Z",
    "isActive": true
  },
  "message": "Usuário recuperado com sucesso"
}
```

#### Atualizar Usuário

```
PUT /users/{id}
```

**Parâmetros da URL**

| Nome | Tipo   | Descrição                  |
|------|--------|----------------------------|
| id   | string | ID único (UUID) do usuário |

**Parâmetros do Corpo**

| Nome  | Tipo   | Descrição                |
|-------|--------|--------------------------|
| name  | string | Nome completo do usuário |
| email | string | Email do usuário         |
| phone | string | Telefone do usuário      |

**Exemplo de Requisição**

```json
{
  "name": "Maria Santos Silva",
  "email": "maria.santos@novomail.com",
  "phone": "(11) 91234-5678"
}
```

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174001",
    "name": "Maria Santos Silva",
    "email": "maria.santos@novomail.com",
    "phone": "(11) 91234-5678",
    "documentId": "12345678901",
    "createdAt": "2025-05-29T12:00:00Z",
    "lastLogin": "2025-05-29T14:30:00Z",
    "isActive": true
  },
  "message": "Usuário atualizado com sucesso"
}
```

### Alertas

#### Criar Alerta

```
POST /alerts
```

**Parâmetros do Corpo**

| Nome        | Tipo    | Descrição                                  |
|-------------|---------|--------------------------------------------|
| title       | string  | Título do alerta                           |
| description | string  | Descrição do alerta (opcional)             |
| latitude    | number  | Latitude da localização do usuário         |
| longitude   | number  | Longitude da localização do usuário        |

**Exemplo de Requisição**

```json
{
  "title": "Emergência",
  "description": "Preciso de ajuda na região central",
  "latitude": -23.550520,
  "longitude": -46.633309
}
```

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174002",
    "userId": "123e4567-e89b-12d3-a456-426614174001",
    "title": "Emergência",
    "description": "Preciso de ajuda na região central",
    "latitude": -23.550520,
    "longitude": -46.633309,
    "createdAt": "2025-05-29T15:00:00Z",
    "status": "ACTIVE"
  },
  "message": "Alerta criado com sucesso"
}
```

#### Listar Alertas do Usuário

```
GET /alerts
```

**Parâmetros de Consulta**

| Nome   | Tipo   | Descrição                                                             |
|--------|--------|-----------------------------------------------------------------------|
| status | string | Filtrar por status (ACTIVE, RESOLVED, CANCELED) - opcional            |
| page   | number | Número da página (padrão: 1)                                          |
| limit  | number | Limite de itens por página (padrão: 10, máximo: 100)                  |

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": [
    {
      "id": "123e4567-e89b-12d3-a456-426614174002",
      "userId": "123e4567-e89b-12d3-a456-426614174001",
      "title": "Emergência",
      "description": "Preciso de ajuda na região central",
      "latitude": -23.550520,
      "longitude": -46.633309,
      "createdAt": "2025-05-29T15:00:00Z",
      "status": "ACTIVE"
    }
  ],
  "pagination": {
    "total": 1,
    "count": 1,
    "perPage": 10,
    "currentPage": 1,
    "totalPages": 1,
    "links": {
      "next": null,
      "previous": null
    }
  }
}
```

#### Obter Alerta

```
GET /alerts/{id}
```

**Parâmetros da URL**

| Nome | Tipo   | Descrição                  |
|------|--------|----------------------------|
| id   | string | ID único (UUID) do alerta  |

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174002",
    "userId": "123e4567-e89b-12d3-a456-426614174001",
    "title": "Emergência",
    "description": "Preciso de ajuda na região central",
    "latitude": -23.550520,
    "longitude": -46.633309,
    "createdAt": "2025-05-29T15:00:00Z",
    "status": "ACTIVE"
  },
  "message": "Alerta recuperado com sucesso"
}
```

#### Atualizar Status do Alerta

```
PATCH /alerts/{id}/status
```

**Parâmetros da URL**

| Nome | Tipo   | Descrição                  |
|------|--------|----------------------------|
| id   | string | ID único (UUID) do alerta  |

**Parâmetros do Corpo**

| Nome   | Tipo   | Descrição                                           |
|--------|--------|-----------------------------------------------------|
| status | string | Novo status (ACTIVE, RESOLVED, CANCELED)            |

**Exemplo de Requisição**

```json
{
  "status": "RESOLVED"
}
```

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174002",
    "userId": "123e4567-e89b-12d3-a456-426614174001",
    "title": "Emergência",
    "description": "Preciso de ajuda na região central",
    "latitude": -23.550520,
    "longitude": -46.633309,
    "createdAt": "2025-05-29T15:00:00Z",
    "status": "RESOLVED"
  },
  "message": "Status do alerta atualizado com sucesso"
}
```

### Contatos de Confiança

#### Criar Contato de Confiança

```
POST /trusted-contacts
```

**Parâmetros do Corpo**

| Nome      | Tipo    | Descrição                                      |
|-----------|---------|------------------------------------------------|
| name      | string  | Nome do contato                                |
| phone     | string  | Telefone do contato                            |
| email     | string  | Email do contato                               |
| sendSms   | boolean | Se deve enviar SMS durante alertas             |
| sendEmail | boolean | Se deve enviar email durante alertas           |

**Exemplo de Requisição**

```json
{
  "name": "Carlos Pereira",
  "phone": "(11) 98765-4321",
  "email": "carlos.pereira@email.com",
  "sendSms": true,
  "sendEmail": true
}
```

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174003",
    "userId": "123e4567-e89b-12d3-a456-426614174001",
    "name": "Carlos Pereira",
    "phone": "(11) 98765-4321",
    "email": "carlos.pereira@email.com",
    "sendSms": true,
    "sendEmail": true,
    "createdAt": "2025-05-29T16:00:00Z",
    "isActive": true
  },
  "message": "Contato de confiança criado com sucesso"
}
```

#### Listar Contatos de Confiança

```
GET /trusted-contacts
```

**Parâmetros de Consulta**

| Nome   | Tipo    | Descrição                                    |
|--------|---------|----------------------------------------------|
| active | boolean | Filtrar por status ativo (opcional)          |
| page   | number  | Número da página (padrão: 1)                 |
| limit  | number  | Limite de itens por página (padrão: 10)      |

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": [
    {
      "id": "123e4567-e89b-12d3-a456-426614174003",
      "userId": "123e4567-e89b-12d3-a456-426614174001",
      "name": "Carlos Pereira",
      "phone": "(11) 98765-4321",
      "email": "carlos.pereira@email.com",
      "sendSms": true,
      "sendEmail": true,
      "createdAt": "2025-05-29T16:00:00Z",
      "isActive": true
    }
  ],
  "pagination": {
    "total": 1,
    "count": 1,
    "perPage": 10,
    "currentPage": 1,
    "totalPages": 1,
    "links": {
      "next": null,
      "previous": null
    }
  }
}
```

#### Atualizar Contato de Confiança

```
PUT /trusted-contacts/{id}
```

**Parâmetros da URL**

| Nome | Tipo   | Descrição                            |
|------|--------|--------------------------------------|
| id   | string | ID único (UUID) do contato           |

**Parâmetros do Corpo**

| Nome      | Tipo    | Descrição                                    |
|-----------|---------|----------------------------------------------|
| name      | string  | Nome do contato                              |
| phone     | string  | Telefone do contato                          |
| email     | string  | Email do contato                             |
| sendSms   | boolean | Se deve enviar SMS durante alertas           |
| sendEmail | boolean | Se deve enviar email durante alertas         |

**Exemplo de Requisição**

```json
{
  "name": "Carlos Pereira Silva",
  "phone": "(11) 99876-5432",
  "email": "carlos.pereira@novomail.com",
  "sendSms": true,
  "sendEmail": false
}
```

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174003",
    "userId": "123e4567-e89b-12d3-a456-426614174001",
    "name": "Carlos Pereira Silva",
    "phone": "(11) 99876-5432",
    "email": "carlos.pereira@novomail.com",
    "sendSms": true,
    "sendEmail": false,
    "createdAt": "2025-05-29T16:00:00Z",
    "isActive": true
  },
  "message": "Contato de confiança atualizado com sucesso"
}
```

#### Ativar/Desativar Contato de Confiança

```
PATCH /trusted-contacts/{id}/status
```

**Parâmetros da URL**

| Nome | Tipo   | Descrição                            |
|------|--------|--------------------------------------|
| id   | string | ID único (UUID) do contato           |

**Parâmetros do Corpo**

| Nome     | Tipo    | Descrição                              |
|----------|---------|----------------------------------------|
| isActive | boolean | Status ativo (true) ou inativo (false) |

**Exemplo de Requisição**

```json
{
  "isActive": false
}
```

**Exemplo de Resposta**

```json
{
  "success": true,
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174003",
    "userId": "123e4567-e89b-12d3-a456-426614174001",
    "name": "Carlos Pereira Silva",
    "phone": "(11) 99876-5432",
    "email": "carlos.pereira@novomail.com",
    "sendSms": true,
    "sendEmail": false,
    "createdAt": "2025-05-29T16:00:00Z",
    "isActive": false
  },
  "message": "Status do contato de confiança atualizado com sucesso"
}
```

## Tratamento de Erros

### Erro de Validação

```json
{
  "success": false,
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Erro de validação nos dados fornecidos",
    "details": [
      {
        "field": "email",
        "message": "O email fornecido não é válido"
      },
      {
        "field": "phone",
        "message": "O telefone deve seguir o formato (XX) XXXXX-XXXX"
      }
    ]
  }
}
```

### Erro de Autenticação

```json
{
  "success": false,
  "error": {
    "code": "AUTHENTICATION_ERROR",
    "message": "Credenciais inválidas"
  }
}
```

### Erro de Recurso Não Encontrado

```json
{
  "success": false,
  "error": {
    "code": "RESOURCE_NOT_FOUND",
    "message": "O recurso solicitado não foi encontrado"
  }
}
```

## Versionamento da API

A API é versionada através do prefixo na URL (exemplo: `/v1/users`). Versões anteriores serão mantidas por pelo menos 6 meses após o lançamento de uma nova versão.
