**Documento Arquitetural do Aplicativo ALERTAR**

------

### 1. Visão Geral do Produto

**Nome:** ALERTAR
 **Categoria:** Aplicativo de emergência pessoal
 **Público-Alvo:** Qualquer pessoa em situação de risco, com foco especial em mulheres, idosos e pessoas em ambientes urbanos inseguros.

**Proposta de Valor:**

- App discreto, rápido e seguro para acionar alertas de emergência.
- Funciona mesmo em modo silencioso e com a tela bloqueada.
- Código aberto com foco em segurança e privacidade.

------

### 2. Funcionalidades Principais

- Botão SOS (com opção silenciosa ou audível)
- Envio de localização em tempo real
- Transmissão de áudio e vídeo
- Gravação local de incidentes
- PIN de desbloqueio com "falso positivo"
- Integração com contatos de emergência e serviços como SAMU, Bombeiros e Polícia
- Interface camuflada (calculadora ou relógio)
- Tela de histórico e gerenciamento de alertas
- Painel administrativo para exportação de dados

------

### 3. Tecnologias Utilizadas

#### Frontend Mobile

- **Plataforma:** Android (inicialmente)
- **Framework:** Flutter (permitindo futura extensão para iOS)
- **Linguagem:** Dart
- **Gerenciamento de estado:** Riverpod / Bloc

#### Backend

- **Servidor:** .NET 8 (possibilidade de migrar para Azure Functions futuramente)
- **Banco de dados:** Firebase Realtime Database (para alertas e registros)
- **Armazenamento local:** SQLite ou Hive (para logs e dados offline)
- **Notificações:** Firebase Cloud Messaging (FCM)

#### Web (Painel Admin)

- **Framework:** Angular ou Next.js
- **Hospedagem:** Firebase Hosting ou Azure Static Web Apps
- **Exportação de dados:** CSV, PDF

------

### 4. Identidade Visual

**Nome:** ALERTAR
 **Logo:** Escudo estilizado com ponto central e ondas (em desenvolvimento)
 **Cores:**

- Primário: Azul Petróleo `#1E2A38`
- Ação: Vermelho Alerta `#FF3B30`
- Secundário: Azul Claro `#3D8BFF`
- Neutros: Branco, Cinza Claro `#F2F2F2`, Cinza Escuro `#333333`

**Fontes:**

- *Inter* para textos e botões
- *Sora* para títulos e identidade

------

### 5. Arquitetura de Componentes

```plaintext
+------------------+
|     Mobile App   |
+--------+---------+
         |
         v
+--------+---------+       +----------------+
|  Firebase Auth   | <---> | Firebase Realtime DB |
+------------------+       +----------------+
         |
         v
+------------------+
|     API .NET     |
+--------+---------+
         |
         v
+------------------+
| Painel Web Admin |
+------------------+
```

------

### 6. Segurança e Privacidade

- Criptografia AES para armazenamento local
- HTTPS para todas as comunicações
- Autenticação com Firebase Auth + PIN offline
- Desativação de alerta com PIN reverso (ex: 4321 ativa alerta silencioso)
- Não rastreia localização continuamente, apenas sob alerta
- Nenhum dado sensível é compartilhado com terceiros

------

### 7. Fluxo de Uso

**1. Ativação do Alerta:**

- Tela de bloqueio / Interface camuflada / Botão no app

**2. Envio de dados:**

- Localização atual
- Início de gravação local
- Alerta via push + SMS para contatos

**3. Opção de transmitir ao vivo:**

- Transmissão via link privado temporário

**4. Encerramento do alerta:**

- Apenas com PIN correto

------

### 8. Requisitos de Sistema

#### Funcionais

- Cadastro de contatos de emergência
- Disparo de alertas personalizados
- Painel com histórico e opção de exportação

#### Não-funcionais

- Tempo de resposta inferior a 2s para envio de alerta
- Suporte offline para funcionalidades básicas
- Modo leve para economizar bateria
- Acessibilidade total (voz, contraste)

------

### 9. Fases do Projeto

1. **Protótipo (Figma)**: Design das telas e fluxos
2. **MVP Mobile (Android)**: Alerta + Gravação + Contatos
3. **Integração Backend**: Firebase + .NET
4. **Painel Web**: Exportação e gerenciamento de alertas
5. **Extensão iOS / APIs de Serviço**
6. **Lançamento e Marketing Comunitário**

------

### 10. Licença e Monetização

- Licença MIT (código aberto)
- Botão de doação via Pix, PayPal e QR
- Possível parcerias com ONGs e redes de apoio

---

O documento arquitetural do app **ALERTAR** foi criado com base na visão de produto, arquitetura técnica, segurança, design e roadmap. Você pode acessá-lo e atualizá-lo conforme o projeto evolui.