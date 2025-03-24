import React, { useEffect, useState } from "react";

export default function ModulesPage() {
  const [servers, setServers] = useState([]);
  const token = localStorage.getItem("jwt");

  useEffect(() => {
    fetch("/api/modules/servers", {
      headers: { Authorization: `Bearer ${token}` },
    })
      .then((res) => res.json())
      .then(setServers);
  }, []);

  return (
    <div className="p-6 grid gap-6">
      <h1 className="text-2xl font-bold">PowerShell Modules per Server</h1>
      {servers.map((server, idx) => (
        <div key={idx} className="border rounded p-4 shadow">
          <h2 className="text-lg font-semibold">{server.name} ({server.ip})</h2>
          <ul className="list-disc list-inside mt-2">
            {server.modules.map((mod, midx) => (
              <li key={midx}>
                {mod.name} – <span className="text-sm text-gray-600">v{mod.version}</span>
              </li>
            ))}
          </ul>
        </div>
      ))}
    </div>
  );
}