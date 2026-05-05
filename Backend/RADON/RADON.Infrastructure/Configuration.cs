using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using RADON.Application.Interfaces;
using RADON.Database;
using RADON.Database.MsSql;
using RADON.Infrastructure.Configurations;
using RADON.Infrastructure.Jobs;
using RADON.Infrastructure.Repositories.Institutions;

namespace RADON.Infrastructure;

public static class Configuration
{
    private const string SECTION_DATABASE = "Database";

    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseConfiguration>(configuration.GetSection(SECTION_DATABASE));

        services.AddDbContext<RadonDbContext, RadonMsSqlDbContext>((p, o) =>
        {
            var configuration = p.GetRequiredService<IOptions<DatabaseConfiguration>>().Value;
            o.UseSqlServer(configuration.ConnectionString);
        });

        services.AddSingleton<IRadonService, RadonService>();


        services.AddTransient<IInstitutionKindRespository, InstitutionKindRespository>();
        services.AddTransient<IInstitutionStatusRespository, InstitutionStatusRespository>();
        services.AddTransient<IUniversityTypeRespository, UniversityTypeRespository>();
        services.AddTransient<IScientificInstitutionTypeRespository, ScientificInstitutionTypeRespository>();
        services.AddTransient<IInstitutionRespository, InstitutionRespository>();


        services.AddQuartz(q =>
        {
            q.AddJob<CreateOrUpdateInstitutionKindsJob>(opts
                => opts.WithIdentity(nameof(CreateOrUpdateInstitutionKindsJob))
            );
            q.AddJob<CreateOrUpdateInstitutionStatusesJob>(opts
                => opts.WithIdentity(nameof(CreateOrUpdateInstitutionStatusesJob))
            );
            q.AddJob<CreateOrUpdateUniversityTypesJob>(opts
                => opts.WithIdentity(nameof(CreateOrUpdateUniversityTypesJob))
            );
            q.AddJob<CreateOrUpdateScientificInstitutionTypesJob>(opts
                => opts.WithIdentity(nameof(CreateOrUpdateScientificInstitutionTypesJob))
            );
            q.AddJob<CreateOrUpdateInstitutionsJob>(opts
                => opts.WithIdentity(nameof(CreateOrUpdateInstitutionsJob))
            );


            q.AddTrigger(opts => opts
                .ForJob(nameof(CreateOrUpdateInstitutionKindsJob))
                .StartNow());

            q.AddTrigger(opts => opts
                .ForJob(nameof(CreateOrUpdateInstitutionStatusesJob))
                .StartNow());

            q.AddTrigger(opts => opts
                .ForJob(nameof(CreateOrUpdateUniversityTypesJob))
                .StartNow());

            q.AddTrigger(opts => opts
                .ForJob(nameof(CreateOrUpdateScientificInstitutionTypesJob))
                .StartNow());

            q.AddTrigger(opts => opts
                .ForJob(nameof(CreateOrUpdateInstitutionsJob))
                .StartNow());
        });
        //services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}
