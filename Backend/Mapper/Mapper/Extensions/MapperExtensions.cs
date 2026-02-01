using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mapper.Extensions;

public static class MapperExtensions
{
    public static IMapper GetMapper(
        this IServiceProvider provider
    ) => provider.GetRequiredService<IMapper>();


    public static IServiceCollection AddMapper(
        this IServiceCollection services,
        IEnumerable<MappingConfiguration> configurations
    ) => services.AddSingleton<IMapper>(new Mapper(configurations));


    public static IServiceCollection AddMapper(
        this IServiceCollection services,
        Assembly assembly
    ) => services.AddMapper([assembly]);


    public static IServiceCollection AddMapper(
        this IServiceCollection services
    ) => services.AddMapper(AppDomain.CurrentDomain.GetAssemblies());


    public static IServiceCollection AddMapper(
        this IServiceCollection services,
        IEnumerable<Assembly> assemblies)
    {
        var classes = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic)
            .Where(t => t.IsSubclassOf(typeof(MappingConfiguration)))
            .ToList();

        var configurations = new List<MappingConfiguration>();
        foreach (var type in classes)
        {
            var instance = (MappingConfiguration?)Activator.CreateInstance(type);
            if (instance is not null)
            {
                configurations.Add(instance);
            }
        }

        return services.AddMapper(configurations);
    }
}
