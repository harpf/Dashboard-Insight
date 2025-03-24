# AdminPortal360 – Architecture Overview

## 🧱 Layers Overview

AdminPortal360 follows a layered architecture for modularity and separation of concerns:

```
Client (React)
 └── Pages (Dashboard, Login, Modules)
 └── API Calls (fetch with JWT)
 └── State Management (React Hooks)
 └── Visual Feedback (Sonner Toasts)

API (.NET Core)
 └── Controllers (Auth, Config, Modules, Users)
 └── Application Layer (Interfaces, DTOs)
 └── Infrastructure (Repositories, AD/LDAP, SAML)
 └── Domain (Entities)

PowerShell Logic
 └── Remote Execution (Invoke-Command)
 └── Custom Functions (Update/Uninstall Module)
```

## 🔐 Authentication Flow

1. User logs in via LDAP or SAML.
2. JWT is issued and saved in `localStorage`.
3. JWT is attached to all API requests.
4. Role is parsed from JWT and drives navigation/UI.

## ⚙️ Configuration

- Configuration stored in `appsettings.custom.json`.
- UI allows:
  - LDAP/SAML selection
  - JWT key update
  - PSRepository management

## 📦 PowerShell Module Management

### Features:
- View all registered servers and installed modules.
- Select version from dynamic dropdown.
- Execute updates and uninstall per-server.
- Use custom PSRepositories.
- Perform bulk actions on multiple servers.

### Flow:
1. Frontend calls backend API with module and server info.
2. Backend initiates `Invoke-Command` with credentials.
3. Runs `Install-Module` or `Uninstall-Module`.

## 🧪 Feedback & UX

- `sonner` library used for toasts:
  - Success/failure for save, update, uninstall
  - Multi-server actions report per host