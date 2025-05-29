```mermaid
flowchart TD
    classDef appStyle fill:#f9f9f9,stroke:#333,stroke-width:2px
    classDef postgresStyle fill:#326690,stroke:#1F425A,stroke-width:2px,color:white
    classDef firebaseStyle fill:#FFCA28,stroke:#F57C00,stroke-width:2px
    classDef sqliteStyle fill:#8CB4FF,stroke:#3B78E7,stroke-width:2px

    A[APP MOBILE\nFlutter] :::appStyle
    B[PostgreSQL\nPersistência principal] :::postgresStyle
    C[Firebase Realtime DB\nSincronização em tempo real] :::firebaseStyle
    D[SQLite\nCache local & funcionamento offline] :::sqliteStyle

    A -->|Persistência de longa duração| B
    A -->|Sincronização em tempo real| C
    A -->|Armazenamento local| D
    C -->|Notificações & atualizações| A
    D -->|Sincronização quando online| B

    subgraph Servidor
        B
    end
    
    subgraph Nuvem
        C
    end
    
    subgraph Dispositivo
        A
        D
    end
```
