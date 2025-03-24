using AdminPortal360.Application.DTOs;
using AdminPortal360.Application.Interfaces;
using AdminPortal360.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdminPortal360.Application.Services;

public class AuthService : IAuthService
{
    private readonly ILdapAuthService _ldapService;
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _config;

    public AuthService(ILdapAuthService ldapService, IUserRepository userRepo, IConfiguration config)
    {
        _ldapService = ldapService;
        _userRepo = userRepo;
        _config = config;
    }

    public async Task<AuthResponse?> AuthenticateAsync(LoginRequest request)
    {
        var method = _config["Authentication:Method"];
        bool valid = method switch
        {
            "LDAP" => _ldapService.Authenticate(request.Username, request.Password),
            "SAML" => true, // Stub
            _ => false
        };

        if (!valid) return null;

        var user = await _userRepo.GetUserAsync(request.Username);
        if (user == null) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthResponse { Token = tokenHandler.WriteToken(token), Role = user.Role };
    }
}