using AdminPortal360.Domain.Entities;

namespace AdminPortal360.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string username);
    Task<bool> UpdateUserRoleAsync(string username, string role);
    IEnumerable<User> GetAllUsers();
}

public class UserRepository : IUserRepository
{
    private static readonly List<User> Users = new()
    {
        new User { Username = "admin", Role = "Admin" },
        new User { Username = "operator", Role = "Operator" },
        new User { Username = "viewer", Role = "Viewer" }
    };

    public Task<User?> GetUserAsync(string username)
        => Task.FromResult(Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)));

    public Task<bool> UpdateUserRoleAsync(string username, string role)
    {
        var user = Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        if (user == null) return Task.FromResult(false);
        user.Role = role;
        return Task.FromResult(true);
    }

    public IEnumerable<User> GetAllUsers() => Users;
}