using Microsoft.Extensions.DependencyInjection;

namespace RADON.Application;

public static class Configuration
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
    {
        return services;
    }
}