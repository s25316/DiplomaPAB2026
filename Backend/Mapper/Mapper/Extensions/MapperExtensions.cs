using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mapper.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection AddMapper(
        this IServiceCollection services
    ) => services.AddMapper(AppDomain.CurrentDomain.GetAssemblies());


    public static IServiceCollection AddMapper(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies)
    {
        var types = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic)
            .Where(t => t.IsSubclassOf(typeof(MappingConfiguration)))
            .ToList();

        foreach (var type in types)
        {
            services.AddSingleton(type);
        }

        services.AddSingleton<IMapper>(p =>
        {
            var configurations = types
            .Select(t => (MappingConfiguration)p.GetRequiredService(t))
            .ToList();

            return new Mapper(configurations);
        });
        return services;
    }
}