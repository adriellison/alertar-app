## **Diagrama de Navegação entre Telas (UX Flow)**

### **Objetivo**

Representar de forma visual e descritiva todos os caminhos possíveis que um usuário pode seguir dentro do aplicativo, considerando os fluxos normais, de exceção, de alerta e de segurança.

------

### **Fluxo Geral**

```
mermaidCopiarEditarflowchart TD
    A[Tela de Boas-vindas] --> B[Escolha: Entrar ou Criar Conta]
    B -->|Criar Conta| C[Cadastro de Usuário]
    B -->|Entrar| D[Login]

    D --> E[Verificação com PIN]
    C --> E
    E --> F[Permissões de Segurança]
    F --> G[Tela Inicial]

    G --> H[Emitir Alerta]
    G --> I[Configurações]
    G --> J[Minhas Localizações]
    G --> K[Contatos de Emergência]
    G --> L[Histórico de Alertas]

    I --> I1[Alterar PIN/Login]
    I --> I2[Permissões do Sistema]
    I --> I3[Configurações de Alerta]
    I --> I4[Privacidade]

    H --> H1[Escolha do tipo de alerta: Violência, Sequestro, Acidente]
    H1 --> H2[Alerta em andamento com cronômetro de cancelamento]
    H2 --> H3[Notificações enviadas + Localização compartilhada]
    H3 --> G

    J --> G
    K --> K1[Adicionar/Remover contatos] --> G
    L --> G
```

------

### **Descrição Detalhada dos Principais Fluxos**

#### 1. **Cadastro / Login**

- O usuário inicia o app com uma tela de boas-vindas.
- Pode optar por criar uma conta ou fazer login.
- Após login, é solicitado um PIN como autenticação rápida para as próximas vezes.
- O sistema solicita permissões (GPS, notificações, microfone, acesso a contatos).

#### 2. **Tela Inicial**

- É o hub principal, com acesso rápido a todas as funções essenciais.
- Botão central ou de destaque: **"Emitir Alerta"**.
- Menus inferiores/laterais: histórico, localização, contatos e configurações.

#### 3. **Emissão de Alerta**

- Ação rápida, acessível com poucos toques.
- Oferece diferentes tipos de alertas.
- Após escolha, inicia-se um temporizador de 5-10 segundos para permitir cancelamento.
- Caso não haja cancelamento, são acionadas as notificações, localização é enviada e registros salvos.

#### 4. **Configurações**

- Opções para customização de alertas, alteração de métodos de login/PIN, configurações de privacidade e permissões.

#### 5. **Histórico e Dados**

- O usuário pode ver os alertas anteriores.
- Pode exportar dados em PDF ou CSV para enviar a terceiros (como polícia ou ONGs).

------

### **Boas Práticas Adotadas**

- Uso de PIN para reforçar segurança.
- Delay de cancelamento no alerta para evitar falsos positivos.
- Alertas configuráveis.
- Navegação intuitiva com foco em acessibilidade e usabilidade para todos os perfis.