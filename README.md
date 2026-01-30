# IssueTrackerLight

En lokal Blazor Web App byggd i C#/.NET som kombinerar:
- Blazor UI (Interactive Server)
- Minimal API (HTTP-endpoints)
- Delad service via Dependency Injection

Applikationen körs lokalt och använder in-memory data.

---

## Krav

- Visual Studio Community
- .NET 8
- Blazor Web App (Interactive Server)

---

## Starta projektet

1. Klona repot
2. Öppna solution i Visual Studio
3. Tryck **F5**

---

## UI-routes

- `/issues` – Lista ärenden
- `/issues/new` – Skapa nytt ärende
- `/issues/{id}` – Detaljsida

---

## API-endpoints (Minimal API)

- `GET /api/issues`  
  Lista alla ärenden

- `GET /api/issues/{id}`  
  Hämta ett specifikt ärende

- `POST /api/issues`  
  Skapa nytt ärende  
  ```json
  {
    "title": "Titel",
    "description": "Beskrivning"
  }
