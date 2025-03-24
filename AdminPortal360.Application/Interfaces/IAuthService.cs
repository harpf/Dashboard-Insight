using AdminPortal360.Application.DTOs;

namespace AdminPortal360.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> AuthenticateAsync(LoginRequest request);
}