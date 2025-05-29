# Documentação Arquitetural - App ALERTAR

## Sumário

1. Diagrama de Sequência
2. Diagrama de Atividades
3. Diagrama de Casos de Uso
4. Diagrama de Navegação entre Telas (UX Flow)
5. Estrutura de Banco de Dados
6. Endpoints REST

## 1. Diagrama de Sequência

### 1.1 Login com PIN

```
sequenceDiagram
    participant Usuário
    participant App
    participant Firebase
    Usuário->>App: Digita PIN
    App->>Firebase: Verifica autenticação
    Firebase-->>App: Autenticado / Erro
    App-->>Usuário: Acesso liberado ou mensagem de erro
```

### 1.2 Envio de Alerta de Emergência

```
sequenceDiagram
    participant Usuário
    participant App
    participant Firebase
    participant ContatoEmergência

    Usuário->>App: Pressiona botão de alerta
    App->>App: Captura localização atual
    App->>Firebase: Salva alerta e localização
    Firebase-->>App: Confirmação de salvamento
    App->>ContatoEmergência: Envia SMS/WhatsApp com localização
```

### 1.3 Recuperação de Senha

```
sequenceDiagram
    participant Usuário
    participant App
    participant Firebase

    Usuário->>App: Solicita redefinir senha
    App->>Firebase: Envia email de recuperação
    Firebase-->>Usuário: Recebe email
    Usuário->>Firebase: Clica no link, redefine senha
```

## 2. Diagrama de Atividades

### 2.1 Fluxo de Ativação de Alerta

```
flowchart TD
    A[Usuário inicia app] --> B{Está logado?}
    B -- Sim --> C[Mostra tela principal]
    B -- Não --> D[Mostra tela de login ou PIN]
    C --> E[Pressiona botão de alerta]
    E --> F[Verifica permissões de localização]
    F --> G[Captura localização atual]
    G --> H[Salva dados do alerta no Firebase]
    H --> I[Envia notificação para contatos de emergência]
    I --> J[Fim]
```

### 2.2 Fluxo de Cadastro

```
flowchart TD
    A[Usuário abre o app] --> B[Seleciona 'Criar conta']
    B --> C[Preenche nome, email, senha]
    C --> D[Confirma dados]
    D --> E{Dados válidos?}
    E -- Sim --> F[Cria conta no Firebase]
    E -- Não --> G[Mostra erro e volta ao formulário]
    F --> H[Usuário logado]
    H --> I[Fim]
```

### 2.3 Fluxo de Redefinição de Senha

```
flowchart TD
    A[Usuário seleciona 'Esqueci minha senha'] --> B[Insere email]
    B --> C[Valida formato do email]
    C --> D{Email existe?}
    D -- Sim --> E[Envia email de redefinição]
    D -- Não --> F[Mostra mensagem de erro]
    E --> G[Usuário redefine senha pelo link]
    G --> H[Confirmação de nova senha]
    H --> I[Fim]
```

## 3. Diagrama de Casos de Uso

```
graph TD
    Actor1(Usuário) --> UC1[Registrar Conta]
    Actor1 --> UC2[Fazer Login / PIN]
    Actor1 --> UC3[Ativar Alerta de Emergência]
    Actor1 --> UC4[Visualizar Histórico de Alertas]
    Actor1 --> UC5[Adicionar/Remover Contatos de Emergência]
    Actor1 --> UC6[Editar Perfil]
    Actor1 --> UC7[Reiniciar Senha]
    Actor1 --> UC8[Configurar Permissões e Preferências]

    Actor2(Contato de Emergência) --> UC9[Receber Alerta e Localização]

    Actor3(Admin ONG/Polícia) --> UC10[Acessar Painel Administrativo]
    Actor3 --> UC11[Exportar Dados de Alertas]
```

**Descrição dos Atores:**

- **Usuário:** Pessoa que instala e utiliza o app para proteção pessoal.
- **Contato de Emergência:** Pessoa indicada pelo usuário para receber alertas.
- **Admin ONG/Polícia:** Parceiros que acessam o painel para monitoramento de alertas (versão futura).

**Principais Casos de Uso:**

- **Registrar Conta:** Cadastro inicial do usuário.
- **Fazer Login / PIN:** Autenticação via email/senha ou PIN rápido.
- **Ativar Alerta:** Envio emergencial com geolocalização.
- **Visualizar Histórico:** Acesso a alertas antigos.
- **Gerenciar Contatos:** Adicionar, editar e excluir contatos.
- **Editar Perfil:** Atualização de dados pessoais.
- **Reiniciar Senha:** Caso esqueça a senha.
- **Configurações:** Controle de privacidade, permissões e preferências.
- **Recebimento de Alerta:** Contato recebe alerta por push/SMS/WhatsApp.
- **Painel Admin:** Permite a ONGs e autoridades analisarem dados (via Web).

## 4. Diagrama de Navegação entre Telas (UX Flow)

(Em desenvolvimento)

## Estrutura de Banco de Dados

### Tabelas/Collections Firebase

- **Users**
  - id
  - nome
  - email
  - senha (criptografada)
  - telefone
  - contatosEmergencia[]
- **Alerts**
  - id
  - userId
  - localizacao {latitude, longitude}
  - dataHora
  - tipoAlerta (violência, sequestro, acidente, etc)
  - status (enviado, confirmado)
- **Logs**
  - id
  - userId
  - acao
  - dataHora

## Endpoints REST

### Autenticação

- `POST /auth/login`
- `POST /auth/pin`
- `POST /auth/register`
- `POST /auth/forgot-password`

### Alertas

- `POST /alerts`
- `GET /alerts/{id}`
- `GET /alerts/user/{userId}`

### Contatos de Emergência

- `GET /contacts/{userId}`
- `POST /contacts`
- `DELETE /contacts/{contactId}`

### Logs

- `GET /logs/{userId}`

---

Aqui está o início completo da documentação dos fluxos do app **ALERTAR**, incluindo fluxogramas no estilo Mermaid, descrições detalhadas de cada etapa e considerações de segurança e UX.

Na próxima etapa, posso continuar com:

- Fluxos de **alerta rápido** e **alerta silencioso**
- Cadastro e edição de **contatos de emergência**
- Gerenciamento de **configurações de segurança**
- Acesso por **atalhos da tela de bloqueio** ou **via smartwatch**, se aplicável