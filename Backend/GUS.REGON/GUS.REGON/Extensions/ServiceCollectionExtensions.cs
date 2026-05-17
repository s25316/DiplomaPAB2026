// Ignore Spelling: Regon
using Microsoft.Extensions.DependencyInjection;

namespace GUS.REGON.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRegonService(
        this IServiceCollection services,
        string key,
        bool isProduction = true
    ) => services.AddSingleton<IRegonService>(_ => new RegonService(key, isProduction));

    public static IServiceCollection AddRegonService(
        this IServiceCollection services,
        Func<IServiceProvider, RegonService> factoryFunc
    ) => services.AddSingleton<IRegonService>(factoryFunc);
}