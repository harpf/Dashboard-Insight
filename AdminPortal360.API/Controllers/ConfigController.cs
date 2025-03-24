using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace AdminPortal360.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _config;
    private const string configFilePath = "appsettings.custom.json";

    public ConfigController(IConfiguration config)
    {
        _config = config;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("save")]
    public IActionResult SaveConfig([FromBody] ConfigDto dto)
    {
        var newConfig = new
        {
            Jwt = new {
                Key = dto.JwtKey,
                Issuer = "AdminPortal360",
                Audience = "AdminPortal360Users"
            },
            Authentication = new {
                Method = dto.AuthMethod
            },
            Ldap = new {
                Path = dto.LdapPath
            },
            Saml = new {
                MetadataUrl = dto.SamlMetadataUrl
            }
        };

        var json = JsonSerializer.Serialize(newConfig, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText(configFilePath, json);
        return Ok(new { success = true });
    }
}

public class ConfigDto
{
    public string AuthMethod { get; set; }
    public string JwtKey { get; set; }
    public string LdapPath { get; set; }
    public string SamlMetadataUrl { get; set; }
}