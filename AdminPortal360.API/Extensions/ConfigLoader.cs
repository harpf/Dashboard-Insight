using Microsoft.Extensions.Configuration;

namespace AdminPortal360.API.Extensions;

public static class ConfigLoader
{
    public static IConfigurationBuilder AddCustomJsonIfExists(this IConfigurationBuilder builder, string path)
    {
        if (File.Exists(path))
        {
            builder.AddJsonFile(path, optional: true, reloadOnChange: true);
        }
        return builder;
    }
}