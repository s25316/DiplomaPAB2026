using Diploma.Domain.PersonEducations.Aggregates;
using Diploma.Domain.PersonEmployments.Aggregates;
using Microsoft.Extensions.DependencyInjection;

namespace Diploma.Domain;

public static class Configuration
{
    public static IServiceCollection AddDomainConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IPersonEducationService, PersonEducationService>();
        services.AddTransient<IPersonEmploymentService, PersonEmploymentService>();

        return services;
    }
}