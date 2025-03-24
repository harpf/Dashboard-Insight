using Microsoft.AspNetCore.Mvc;
using AdminPortal360.Application.Interfaces;
using AdminPortal360.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace AdminPortal360.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.AuthenticateAsync(request);
        if (result == null) return Unauthorized("Invalid credentials.");
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("config-type")]
    public IActionResult GetAuthType([FromServices] IConfiguration config)
    {
        return Ok(config["Authentication:Method"]);
    }
}