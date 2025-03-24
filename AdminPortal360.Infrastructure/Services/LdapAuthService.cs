using System.DirectoryServices;

namespace AdminPortal360.Infrastructure.Services;

public interface ILdapAuthService
{
    bool Authenticate(string username, string password);
}

public class LdapAuthService : ILdapAuthService
{
    private readonly IConfiguration _config;
    public LdapAuthService(IConfiguration config) => _config = config;

    public bool Authenticate(string username, string password)
    {
        try
        {
            using var entry = new DirectoryEntry(_config["Ldap:Path"], username, password);
            var nativeObject = entry.NativeObject;
            return true;
        }
        catch
        {
            return false;
        }
    }
}