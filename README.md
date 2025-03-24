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
