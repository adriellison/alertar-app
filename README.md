# 🚨 alertar-app

Alertar é um aplicativo de **segurança pessoal** com foco em situações de emergência, oferecendo funcionalidades como **envio rápido de alertas**, **compartilhamento de localização em tempo real**, **simulação de desativação falsa** e **transmissão de áudio/vídeo ao vivo**.

Ideal para mulheres, casais, pessoas em situação de risco ou qualquer indivíduo preocupado com sua segurança pessoal.

![Licença: GPL v3](https://img.shields.io/badge/Licença-GPLv3-blue.svg)

---

## ✨ Funcionalidades principais

- 🔕 **Envio de alertas silenciosos com timer**
- 📍 **Compartilhamento de localização em tempo real**
- 🎥 **Transmissão de vídeo e áudio ao vivo**
- 💰 **Doações in-app para apoio ao projeto**
- 👥 **Integração com contatos de confiança**
- 🛡️ **Modo simulado para sequestros (desativação falsa)**
- 📦 **Registro local e opcional em nuvem**
- 🔑 **Login com PIN e autenticação segura**

---

## 🗂️ Estrutura do Repositório

```plaintext
alertar-app/
├── backend/           # API .NET (autenticação, alertas, controle de usuários)
├── frontend/          # App mobile (Flutter) e/ou web (Angular/React)
├── docs/              # Documentação técnica, diagramas, fluxos e protótipos
└── README.md
```

---

## 📦 Tecnologias Utilizadas

- 🧠 .NET (API backend)
- 💻 Flutter (aplicativo mobile multiplataforma)
- 🌐 Firebase (autenticação, realtime database opcional)
- 🔐 Criptografia local (armazenamento seguro de dados)
- 🎨 Figma (protótipos interativos e identidade visual)
- 📊 Markdown & Mermaid (documentações e diagramas)

---

## 🚀 Instalação

Siga os passos abaixo para configurar o projeto localmente:

1. Clone o repositório:

   ```bash
   git clone https://github.com/seu-usuario/alertar-app.git
   ```

2. Navegue até o diretório do projeto:

   ```bash
   cd alertar-app
   ```

3. Instale as dependências do backend:

   ```bash
   cd backend
   dotnet restore
   ```

4. Instale as dependências do frontend:

   ```bash
   cd ../frontend
   npm install
   ```

5. Inicie o servidor de desenvolvimento:

   ```bash
   npm start
   ```

---

## 🧪 Testes & Qualidade

- ✅ Testes unitários e de integração no backend
- 🧪 Testes de interface com Flutter Testing
- 🔍 Lint e validações no CI/CD
- 📤 Deploy automatizado (em breve)

---

## 📈 Status do Projeto

🚧 Em desenvolvimento – estamos trabalhando nas primeiras funcionalidades.

---

## 📄 Licença

Este projeto está licenciado sob os termos da **GNU General Public License v3.0 (GPLv3)**.

Você tem o direito de:

- ✔️ Usar, copiar e distribuir o código-fonte;
- ✔️ Modificar o projeto para suas necessidades;
- ✔️ Compartilhar versões modificadas;

**Desde que:**

- 🔁 O projeto continue sendo distribuído sob a mesma licença GPLv3;
- 🙌 Seja dado o devido crédito aos autores;
- 💡 Seja fornecido o código-fonte correspondente.

🔗 Para mais detalhes, acesse https://www.gnu.org/licenses/gpl-3.0.html.

---

## 🤝 Como Contribuir

Contribuições são muito bem-vindas! Você pode:

- Criar um fork
- Sugerir melhorias e correções via Pull Request
- Ajudar com traduções, documentação ou testes

Siga os passos abaixo para contribuir:

1. Faça um fork do projeto.

2. Crie uma branch para sua feature:

   ```bash
   git checkout -b minha-feature
   ```

3. Faça suas alterações e commit:

   ```bash
   git commit -m 'Adiciona minha feature'
   ```

4. Envie para o seu fork:

   ```bash
   git push origin minha-feature
   ```

5. Abra um Pull Request explicando suas alterações.

Consulte [CONTRIBUTING.md](CONTRIBUTING.md) para mais detalhes.

## 📘 Guia de Commits Semânticos

Para manter um histórico de commits consistente e informativo, siga o padrão [Conventional Commits](https://www.conventionalcommits.org/pt-br/v1.0.0/). Consulte nosso [Guia de Commits Semânticos](docs/conventional_commits.md) para detalhes e exemplos em português.

<!-- ---

## 📬 Contato

Se tiver dúvidas ou sugestões, entre em contato:

- Email: contato@alertarapp.org
- Twitter: [@alertarapp](https://twitter.com/alertarapp)
- Site: [www.alertarapp.org](https://www.alertarapp.org) -->

---

## ☕ Apoie este Projeto

Se este projeto te ajudou ou você acredita que pode salvar vidas, considere apoiar:

- 💳 Doações via Pix ou PayPal (em breve no app)
- 🌟 Dando uma estrela no repositório
- 📢 Compartilhando com outras pessoas
