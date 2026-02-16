using Microsoft.Extensions.DependencyInjection;

namespace GUS.TERYT.Application;

public static class Configuration
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
    {
        return services;
    }
}