using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using RADON.Application.Interfaces.Courses;
using RADON.Application.Interfaces.Institutions;
using RADON.Application.Interfaces.Shared;
using RADON.Database;
using RADON.Database.MsSql;
using RADON.Infrastructure.Configurations;
using RADON.Infrastructure.Jobs;
using RADON.Infrastructure.Repositories.Courses;
using RADON.Infrastructure.Repositories.Institutions;
using RADON.Infrastructure.Repositories.Shared;

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

        // --- INSTITUTIONS ---
        services.AddTransient<IInstitutionKindRespository, InstitutionKindRespository>();
        services.AddTransient<IInstitutionStatusRespository, InstitutionStatusRespository>();
        services.AddTransient<IUniversityTypeRespository, UniversityTypeRespository>();
        services.AddTransient<IScientificInstitutionTypeRespository, ScientificInstitutionTypeRespository>();
        services.AddTransient<IInstitutionRespository, InstitutionRespository>();

        // --- COURSES ---
        services.AddTransient<ICourseFormRespository, CourseFormRespository>();
        services.AddTransient<ICourseInstanceStatusRespository, CourseInstanceStatusRespository>();
        services.AddTransient<ICourseLevelRespository, CourseLevelRespository>();
        services.AddTransient<ICourseProfileRespository, CourseProfileRespository>();
        services.AddTransient<ICourseStatusRespository, CourseStatusRespository>();
        services.AddTransient<IIscedRespository, IscedRespository>();
        services.AddTransient<ILanguageRespository, LanguageRespository>();
        services.AddTransient<IProfessionalTitleRespository, ProfessionalTitleRespository>();

        // --- SHARED ---
        services.AddTransient<IDisciplineRespository, DisciplineRespository>();


        services.AddQuartz(q =>
        {
            // --- INSTITUTIONS ---
            q.AddJob<UpdateInstitutionKindsJob>(opts
                => opts.WithIdentity(nameof(UpdateInstitutionKindsJob))
            );
            q.AddJob<UpdateInstitutionStatusesJob>(opts
                => opts.WithIdentity(nameof(UpdateInstitutionStatusesJob))
            );
            q.AddJob<UpdateUniversityTypesJob>(opts
                => opts.WithIdentity(nameof(UpdateUniversityTypesJob))
            );
            q.AddJob<UpdateScientificInstitutionTypesJob>(opts
                => opts.WithIdentity(nameof(UpdateScientificInstitutionTypesJob))
            );
            q.AddJob<UpdateInstitutionsJob>(opts
                => opts.WithIdentity(nameof(UpdateInstitutionsJob))
            );

            // --- COURSES ---
            q.AddJob<UpdateCourseFormJob>(opts
                => opts.WithIdentity(nameof(UpdateCourseFormJob))
            );
            q.AddJob<UpdateCourseInstanceStatusJob>(opts
                => opts.WithIdentity(nameof(UpdateCourseInstanceStatusJob))
            );
            q.AddJob<UpdateCourseLevelJob>(opts
                => opts.WithIdentity(nameof(UpdateCourseLevelJob))
            );
            q.AddJob<UpdateCourseProfileJob>(opts
                => opts.WithIdentity(nameof(UpdateCourseProfileJob))
            );
            q.AddJob<UpdateCourseStatusJob>(opts
                => opts.WithIdentity(nameof(UpdateCourseStatusJob))
            );
            q.AddJob<UpdateLanguageJob>(opts
                => opts.WithIdentity(nameof(UpdateLanguageJob))
            );
            q.AddJob<UpdateProfessionalTitleJob>(opts
                => opts.WithIdentity(nameof(UpdateProfessionalTitleJob))
            );

            // --- SHARED ---
            q.AddJob<UpdateDisciplineJob>(opts
                => opts.WithIdentity(nameof(UpdateDisciplineJob))
            );


            // --- INSTITUTIONS ---
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateInstitutionKindsJob))
                .StartNow());
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateInstitutionStatusesJob))
                .StartNow());
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateUniversityTypesJob))
                .StartNow());
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateScientificInstitutionTypesJob))
                .StartNow());
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateInstitutionsJob))
                .StartNow());


            // --- COURSES ---
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateCourseFormJob))
                .StartNow());
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateCourseInstanceStatusJob))
                .StartNow());
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateCourseLevelJob))
                .StartNow());
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateCourseProfileJob))
                .StartNow());
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateCourseStatusJob))
                .StartNow());
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateLanguageJob))
                .StartNow());
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateProfessionalTitleJob))
                .StartNow());

            // --- SHARED ---
            q.AddTrigger(opts => opts
                .ForJob(nameof(UpdateDisciplineJob))
                .StartNow());
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}
