using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GUS.REGON.Infrastructure;

public static class Configuration
{
    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}