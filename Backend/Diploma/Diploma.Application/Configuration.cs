using Diploma.Application.Services;
using Diploma.Application.Services.Generators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Diploma.Application;

public static class Configuration
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(
            Assembly.GetExecutingAssembly()
        ));

        services.AddTransient<IEmailService, EmailService>();

        // Generators
        services.AddTransient<ISaltGenerator, SaltGenerator>();
        services.AddSingleton<ICodeGenerator, CodeGenerator>();
        services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();

        return services;
    }
}