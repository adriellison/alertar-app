# 📘 Guia de Commits Semânticos (Conventional Commits)

Este guia segue a especificação oficial do [Conventional Commits 1.0.0](https://www.conventionalcommits.org/pt-br/v1.0.0/), adaptado para o contexto do projeto **alertar-app**.

### 📌 Estrutura da Mensagem de Commit

Cada mensagem de commit deve seguir o seguinte formato:

```plaintext
<tipo>[escopo opcional]: <descrição breve>

[corpo opcional]

[rodapé opcional]
```

- **tipo**: identifica a natureza da mudança (ex: `feat`, `fix`).
- **escopo**: (opcional) especifica a área do código afetada (ex: `auth`, `api`).
- **descrição breve**: resumo conciso da alteração, no modo imperativo.
- **corpo**: (opcional) fornece detalhes adicionais sobre a mudança.
- **rodapé**: (opcional) inclui informações como referências a issues ou notas de mudanças significativas.

### 🧩 Tipos Comuns de Commits

| Tipo       | Descrição                                                    |
| ---------- | ------------------------------------------------------------ |
| `feat`     | Introdução de nova funcionalidade                            |
| `fix`      | Correção de bug                                              |
| `docs`     | Alterações na documentação                                   |
| `style`    | Mudanças de formatação, sem impacto no código (ex: espaços, ponto e vírgula) |
| `refactor` | Refatoração de código, sem adição de funcionalidade ou correção de bug |
| `test`     | Adição ou modificação de testes                              |
| `chore`    | Tarefas de manutenção (ex: atualizações de dependências)     |
| `build`    | Mudanças que afetam o sistema de build ou dependências externas |
| `ci`       | Alterações em arquivos de configuração de integração contínua |
| `perf`     | Melhorias de desempenho                                      |
| `revert`   | Reversão de commit anterior                                  |

### 🧠 Exemplos Práticos

**1. Adição de nova funcionalidade:**

```scss
feat(api): adiciona endpoint para criação de alertas
```

**2. Correção de bug:**

```scss
fix(auth): corrige falha na validação de tokens expirados
```

**3. Atualização de documentação:**

```scss
docs(readme): atualiza instruções de instalação
```

**4. Refatoração de código:**

```scss
refactor(video): simplifica lógica de inicialização da câmera
```

**5. Mudança significativa (breaking change):**

```scss
feat(auth)!: altera algoritmo de hash para autenticação
```

Ou utilizando o rodapé: [Conventional Commits](https://www.conventionalcommits.org/pt-br/v1.0.0/?utm_source=chatgpt.com)

```scss
feat(auth): altera algoritmo de hash para autenticação

BREAKING CHANGE: usuários precisarão redefinir suas senhas após esta atualização
```

### 🔗 Referência Oficial

Para mais detalhes, consulte a especificação completa em: https://www.conventionalcommits.org/pt-br/v1.0.0/
