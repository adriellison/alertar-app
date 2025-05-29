Abaixo está a documentação detalhada dos fluxos principais do app **Alertar**, estruturados como um arquiteto de software faria. Essa estrutura inclui os principais estados, decisões e transições desde o início da interação do usuário com o aplicativo, com foco em **cadastro**, **login**, **entrada com PIN**, e **recuperação de senha**:

------

## **1. Fluxo Geral de Acesso**

### 1.1. Início (Splash Screen)

- Exibição do logotipo e carregamento inicial.
- Verificação de sessão salva/token válido.
  - **Se sim:** Redireciona para tela principal (Home).
  - **Se não:** Redireciona para tela de login.

------

## **2. Cadastro de Usuário**

**Tela: "Criar Conta"**

### Entradas:

- Nome completo
- CPF (ou e-mail)
- Telefone (validação via SMS opcional)
- Criar PIN de 4 dígitos
- Termos e condições (checkbox obrigatório)

### Validações:

- Campos obrigatórios não vazios
- Formato válido de CPF ou e-mail
- PIN: apenas numérico e 4 dígitos
- Aceite de termos obrigatório

### Processos:

- Envia dados para backend
- Gera e armazena token de autenticação
- Redireciona para onboarding/tutorial ou Home

------

## **3. Login do Usuário**

**Tela: "Login"**

### Entradas:

- CPF ou e-mail
- PIN (campo protegido)

### Validações:

- Autenticação via backend
- Token armazenado localmente (Secure Storage)

### Ações:

- **Sucesso:** Redireciona para tela principal
- **Falha:** Exibe erro e opção de redefinir PIN

------

## **4. Entrada via PIN (Autenticação Silenciosa)**

**Cenário: Sessão salva, porém protegida por PIN**

### Tela: "Digite seu PIN"

- Entradas: Teclado numérico
- 3 tentativas falhas: trava temporária ou oferece recuperação de PIN
- PIN correto: libera acesso ao app

**Opção extra:** PIN alternativo (falso) aciona alerta silencioso

------

## **5. Recuperação de PIN**

**Tela: "Esqueceu seu PIN?"**

### Fluxo:

1. Usuário informa CPF ou e-mail
2. Verificação de identidade (via código SMS ou e-mail)
3. Entrada de novo PIN (confirmação em 2 campos)
4. Atualização segura do novo PIN no backend
5. Redireciona para login

------

## **6. Logout e Encerramento de Sessão**

### Opções:

- Manual via menu de configurações
- Automático após tempo de inatividade (definido por política)
- Ao trocar de dispositivo

------

## **Considerações Arquiteturais**

### Segurança:

- PIN e Token criptografados em armazenamento local
- Comunicação com backend usando HTTPS + JWT
- Validação de dispositivo (opcional)

### Acessibilidade:

- Botões grandes e legíveis
- Suporte para leitura de tela (screen reader)
- Contraste adequado e modo escuro (futuramente)

### Extensibilidade:

- Suporte a login social (Google/Apple) no futuro
- Possibilidade de múltiplos perfis por dispositivo