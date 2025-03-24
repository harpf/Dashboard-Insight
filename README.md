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