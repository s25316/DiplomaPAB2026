using Microsoft.Extensions.DependencyInjection;

namespace GUS.REGON.Application;

public static class Configuration
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
    {
        return services;
    }
}