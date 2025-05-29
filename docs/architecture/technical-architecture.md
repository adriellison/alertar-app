## **Arquitetura Técnica do App Alertar**

### **1. Visão Geral**

O sistema **Alertar** será composto por três grandes blocos:

- **Frontend Mobile (Android)**
- **Backend (API REST)**
- **Serviços Externos e Integrações**

Essa arquitetura foi desenhada para ser **modular, escalável, segura e resiliente**, aproveitando tecnologias modernas e mantendo a performance ideal em dispositivos Android.

------

### **2. Tecnologias Utilizadas**

| Camada                  | Tecnologia                                              |
| ----------------------- | ------------------------------------------------------- |
| App Mobile              | Kotlin (Jetpack Compose) + Firebase SDK + Mapbox        |
| Backend API             | ASP.NET Core (.NET 8) + Firebase Admin SDK              |
| Banco de dados          | Firestore (Firebase NoSQL) + Storage Firebase           |
| Autenticação            | Firebase Authentication (com email/senha e PIN)         |
| Push Notifications      | Firebase Cloud Messaging (FCM)                          |
| Serviços de Localização | Google Location Services ou Mapbox SDK                  |
| Infraestrutura Web      | Azure ou GCP (Hospedagem da API, monitoramento e CI/CD) |
| Painel Web              | Angular 17 + Tailwind + ASP.NET para API de gestão      |



------

### **3. Diagrama de Componentes**

```
mermaidCopiarEditargraph TD
  subgraph Mobile App (Android)
    UI[Interface do Usuário]
    LocationService[Serviço de Localização]
    AudioRecorder[Gravador de Áudio]
    AlertHandler[Gerenciador de Alertas]
    LocalDB[Armazenamento Local (Room/SharedPrefs)]
    FCMClient[Firebase Messaging]
    UI --> AlertHandler
    AlertHandler --> LocationService
    AlertHandler --> AudioRecorder
    AlertHandler --> FCMClient
    AlertHandler --> LocalDB
    AlertHandler --> APIClient
  end

  subgraph API REST (.NET 8)
    APIClient[APIClient (HTTP)]
    Auth[Autenticação Firebase]
    AlertController[Controlador de Alertas]
    UserController[Controlador de Usuários]
    ContactController[Controlador de Contatos]
    FirestoreDB[Firestore (Firebase)]
    FirebaseStorage[Gravação de Áudio]
    AlertController --> FirestoreDB
    AlertController --> FirebaseStorage
    UserController --> FirestoreDB
    ContactController --> FirestoreDB
  end

  subgraph Painel Web (Admin)
    AdminUI[Dashboard Angular]
    AdminUI --> APIClient
  end

  subgraph Serviços Externos
    FirebaseAuth[Firebase Auth]
    FCM[Firebase Cloud Messaging]
    MapService[Google Maps ou Mapbox]
  end

  Auth --> FirebaseAuth
  FCMClient --> FCM
  LocationService --> MapService
```

------

### **4. Componentes e Responsabilidades**

#### **Mobile App**

- **UI**: Interface do usuário amigável, moderna, inspirada em apps como Uber e Neon.
- **AlertHandler**: Orquestra a emissão do alerta (áudio, localização, contatos).
- **FCMClient**: Recebe notificações push para retorno de status.
- **LocalDB**: Guarda logs e alertas localmente para redundância.
- **AudioRecorder**: Gera o áudio e envia junto com localização.

#### **Backend API**

- **Autenticação**: Middleware com FirebaseAuth e verificação via token.
- **AlertController**: Recebe alertas, grava dados e retorna confirmações.
- **UserController**: Gerencia criação, autenticação e alteração de perfil.
- **ContactController**: Gerencia contatos de emergência e priorizações.
- **FirestoreDB**: Guarda dados de usuários, alertas, configurações.

#### **Admin Dashboard**

- Visualização de alertas
- Exportação de logs
- Gestão de usuários parceiros (ONGs, polícia, etc.)

------

### **5. Segurança**

- **Tokens JWT** para autenticação de chamadas.
- **Criptografia** de PINs e dados sensíveis no app.
- **Regra de acesso no Firebase Firestore** (por tipo de usuário e permissão).
- **Canal HTTPS obrigatório** em todas as interações.

------

### **6. Escalabilidade e Futuro**

- Modularidade permite portar o app para iOS futuramente.
- API preparada para multiclientes (caso uso B2B com ONGs, empresas).
- Logs podem ser replicados em BigQuery/Elastic para análises futuras.
- Arquitetura compatível com serverless (Cloud Functions) ou microserviços.