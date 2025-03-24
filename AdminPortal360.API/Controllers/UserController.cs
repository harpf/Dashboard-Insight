using Microsoft.AspNetCore.Mvc;
using AdminPortal360.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AdminPortal360.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepo;
    public UserController(IUserRepository userRepo) => _userRepo = userRepo;

    [Authorize(Roles = "Admin")]
    [HttpPost("set-role")]
    public async Task<IActionResult> SetUserRole([FromBody] RoleUpdateRequest request)
    {
        var success = await _userRepo.UpdateUserRoleAsync(request.Username, request.Role);
        if (!success) return NotFound();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("list")]
    public IActionResult ListUsers()
    {
        var users = _userRepo.GetAllUsers();
        return Ok(users);
    }

    public class RoleUpdateRequest
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }
}