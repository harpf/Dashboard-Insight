import React, { useState } from "react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Card, CardContent } from "@/components/ui/card";

export default function LoginPage() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async () => {
    const res = await fetch("/api/auth/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ username, password }),
    });

    if (res.ok) {
      const data = await res.json();
      localStorage.setItem("jwt", data.token);
      alert("Erfolgreich eingeloggt");
      window.location.href = "/dashboard";
    } else {
      alert("Login fehlgeschlagen");
    }
  };

  return (
    <div className="flex justify-center items-center h-screen">
      <Card className="w-full max-w-md">
        <CardContent className="p-6 grid gap-4">
          <h2 className="text-xl font-bold">Login</h2>
          <Input placeholder="Benutzername" value={username} onChange={(e) => setUsername(e.target.value)} />
          <Input placeholder="Passwort" type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
          <Button onClick={handleLogin}>Einloggen</Button>
        </CardContent>
      </Card>
    </div>
  );
}