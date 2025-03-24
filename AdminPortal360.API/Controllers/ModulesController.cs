using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;

namespace AdminPortal360.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModulesController : ControllerBase
{
    [Authorize]
    [HttpPost("update")]
    public IActionResult UpdateModule([FromBody] ModuleUpdateRequest request)
    {
        try
        {
            var securePassword = new SecureString();
            foreach (var c in request.Password) securePassword.AppendChar(c);
            var credential = new PSCredential(request.Username, securePassword);

            var connectionInfo = new WSManConnectionInfo(
                new Uri($"http://{request.ServerName}:5985/wsman"),
                "http://schemas.microsoft.com/powershell/Microsoft.PowerShell",
                credential)
            {
                AuthenticationMechanism = AuthenticationMechanism.Default,
                OperationTimeout = 4 * 60 * 1000,
                OpenTimeout = 1 * 60 * 1000
            };

            using var runspace = RunspaceFactory.CreateRunspace(connectionInfo);
            runspace.Open();

            using var ps = PowerShell.Create();
            ps.Runspace = runspace;
            ps.AddScript($@"
                if (Get-Module -ListAvailable -Name '{request.Module}') {{
                    Install-Module -Name '{request.Module}' -RequiredVersion '{request.Version}' -Force
                }} else {{
                    Install-Module -Name '{request.Module}' -RequiredVersion '{request.Version}' -Force
                }}
            ");
            ps.Invoke();

            if (ps.HadErrors)
                return BadRequest(string.Join("\n", ps.Streams.Error.Select(e => e.ToString())));

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    public class ModuleUpdateRequest
    {
        public string ServerName { get; set; }
        public string Module { get; set; }
        public string Version { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}