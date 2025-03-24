import React, { useEffect, useState } from "react";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { useNavigate, Link } from "react-router-dom";

export default function Dashboard() {
  const [authMethod, setAuthMethod] = useState("LDAP");
  const [jwtKey, setJwtKey] = useState("");
  const [ldapPath, setLdapPath] = useState("");
  const [samlMetadataUrl, setSamlMetadataUrl] = useState("");
  const [role, setRole] = useState("");
  const [users, setUsers] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("jwt");
    if (!token) {
      navigate("/login");
      return;
    }
    fetch("/api/config/config-type", {
      headers: { Authorization: `Bearer ${token}` },
    }).then(res => {
      if (res.status === 401) navigate("/login");
    });

    const payload = JSON.parse(atob(token.split(".")[1]));
    setRole(payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);

    if (payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === "Admin") {
      fetch("/api/user/list", {
        headers: { Authorization: `Bearer ${token}` },
      })
        .then(res => res.json())
        .then(setUsers);
    }
  }, []);

  const handleSave = async () => {
    const token = localStorage.getItem("jwt");
    const res = await fetch("/api/config/save", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        authMethod,
        jwtKey,
        ldapPath,
        samlMetadataUrl,
      }),
    });

    if (res.ok) alert("Configuration saved");
    else alert("Error saving configuration");
  };

  const handleLogout = () => {
    localStorage.removeItem("jwt");
    navigate("/login");
  };

  const handleRoleChange = async (username, role) => {
    const token = localStorage.getItem("jwt");
    const res = await fetch("/api/user/set-role", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({ username, role }),
    });
    if (res.ok) alert("Role updated");
  };

  return (
    <div className="p-6 grid gap-6">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold">AdminPortal360 – Configuration</h1>
        <div className="flex gap-4">
          <Link to="/dashboard">
            <Button variant="outline">Dashboard</Button>
          </Link>
          {role === "Admin" && (
            <Link to="/settings">
              <Button variant="outline">Settings</Button>
            </Link>
          )}
          <Button onClick={handleLogout} variant="destructive">Logout</Button>
        </div>
      </div>

      <Card>
        <CardContent className="p-6">
          <Tabs value={authMethod} onValueChange={setAuthMethod} className="w-full">
            <TabsList>
              <TabsTrigger value="LDAP">LDAP</TabsTrigger>
              <TabsTrigger value="SAML">SAML</TabsTrigger>
            </TabsList>

            <TabsContent value="LDAP">
              <div className="grid gap-4">
                <Label>LDAP Path</Label>
                <Input value={ldapPath} onChange={(e) => setLdapPath(e.target.value)} placeholder="e.g. LDAP://dc=domain,dc=local" />
              </div>
            </TabsContent>

            <TabsContent value="SAML">
              <div className="grid gap-4">
                <Label>Metadata URL</Label>
                <Input value={samlMetadataUrl} onChange={(e) => setSamlMetadataUrl(e.target.value)} placeholder="https://idp.example.com/metadata" />
              </div>
            </TabsContent>
          </Tabs>

          <div className="grid gap-4 mt-6">
            <Label>JWT Key</Label>
            <Input value={jwtKey} onChange={(e) => setJwtKey(e.target.value)} type="password" placeholder="Secret key" />
          </div>

          <Button className="mt-6" onClick={handleSave}>
            Save
          </Button>
        </CardContent>
      </Card>

      {role === "Admin" && (
        <Card>
          <CardContent className="p-6">
            <h2 className="text-lg font-bold mb-4">User Management</h2>
            {users.map(user => (
              <div key={user.username} className="flex items-center gap-4 mb-2">
                <span className="w-1/3">{user.username}</span>
                <select
                  defaultValue={user.role}
                  onChange={e => handleRoleChange(user.username, e.target.value)}
                  className="border rounded px-2 py-1"
                >
                  <option value="Viewer">Viewer</option>
                  <option value="Operator">Operator</option>
                  <option value="Admin">Admin</option>
                </select>
              </div>
            ))}
          </CardContent>
        </Card>
      )}
    </div>
  );
}