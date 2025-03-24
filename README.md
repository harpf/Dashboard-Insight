<<<<<<< HEAD
# AdminPortal360 – Module Control Extended

This version includes:

## 🆕 New Features

### 🔽 Version Selection
- Dynamically fetch available versions from a selected repository
- Version dropdown rendered per module

### ❌ Module Uninstall
- UI button to trigger uninstall per server/module
- POST `/api/modules/uninstall` with payload:
```json
{
  "serverName": "Server01",
  "module": "CustomModule"
}
```

### 🔄 Dynamic Version Fetch
- Endpoint: `GET /api/modules/versions?module=<name>&repository=<repo>`
- Returned values shown in dropdown next to each module

### 🔧 Repository Management
- Add/remove custom PowerShell repositories in UI
- Saved to configuration file

---

## Example API for Module Update
```json
{
  "serverName": "Server01",
  "module": "Az",
  "version": "10.0.0",
  "username": "DOMAIN\\admin",
  "password": "secret",
  "repository": "InternalRepo"
}
```

---

📦 Full feature set available in this archive.
=======
# AdminPortal360

## 🔐 Authentication
- LDAP and SAML authentication
- JWT-based role access
- Roles: Admin, Operator, Viewer

## 🧰 Configuration UI
- LDAP/SAML switching
- JWT secret management
- Repository management (PSGallery and custom)

## 🧪 PowerShell Module Control
- View installed modules per server
- Select version (dropdown)
- Update/downgrade modules
- Uninstall support
- Multi-server bulk actions
- Visual toast feedback for all operations

## 💬 Feedback Integration
- Uses `sonner` toast notifications (success/failure)
- All actions visually confirmed

## 🔁 API Endpoints
- `POST /api/modules/update`
- `POST /api/modules/uninstall`
- `GET /api/modules/versions`
- `GET /api/user/list`, `POST /api/user/set-role`
- `POST /api/config/save`, `GET /api/config/config-type`

---

## 📁 Structure
- `AdminPortal360.API/` - .NET Core Web API
- `AdminPortal360.Web/` - React UI with Tailwind + shadcn/ui
- `PowerShell_Remoting_Functions.ps1` - for remote execution

MIT License – Contributions welcome
>>>>>>> fa666cea6a3c7555915f45ccecb40cbf0c19726d
