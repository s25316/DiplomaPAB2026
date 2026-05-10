using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using RADON.Application.Interfaces.Courses;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Application.Interfaces.Institutions;
using RADON.Application.Interfaces.Institutions.Dictionaries;
using RADON.Application.Interfaces.Shared.Dictionaries;
using RADON.Database;
using RADON.Database.MsSql;
using RADON.Infrastructure.Configurations;
using RADON.Infrastructure.Jobs;
using RADON.Infrastructure.QueryBuilders;
using RADON.Infrastructure.Repositories.Courses;
using RADON.Infrastructure.Repositories.Courses.Dictionaries;
using RADON.Infrastructure.Repositories.Institutions;
using RADON.Infrastructure.Repositories.Institutions.Dictionaries;
using RADON.Infrastructure.Repositories.Shared.Dictionaries;

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
        services.AddTransient<IInstitutionKindRepository, InstitutionKindRepository>();
        services.AddTransient<IInstitutionStatusRepository, InstitutionStatusRepository>();
        services.AddTransient<IUniversityTypeRepository, UniversityTypeRepository>();
        services.AddTransient<IScientificInstitutionTypeRepository, ScientificInstitutionTypeRepository>();
        services.AddTransient<IInstitutionRepository, InstitutionRepository>();

        // --- COURSES ---
        services.AddTransient<ICourseFormRepository, CourseFormRepository>();
        services.AddTransient<ICourseInstanceStatusRepository, CourseInstanceStatusRepository>();
        services.AddTransient<ICourseLevelRepository, CourseLevelRepository>();
        services.AddTransient<ICourseProfileRepository, CourseProfileRepository>();
        services.AddTransient<ICourseStatusRepository, CourseStatusRepository>();
        services.AddTransient<IIscedRepository, IscedRepository>();
        services.AddTransient<ILanguageRepository, LanguageRepository>();
        services.AddTransient<IProfessionalTitleRepository, ProfessionalTitleRepository>();
        services.AddTransient<ICourseRepository, CourseRepository>();

        // --- SHARED ---
        services.AddTransient<IDisciplineRespository, DisciplineRespository>();


        // --- QUERY BUILDERS ---
        services.AddTransient<CourseQueryBuilder>();
        services.AddTransient<InstitutionQueryBuilder>();


        services.AddQuartz(q =>
        {
            q.AddJobListener<JobChainerListener>();
            var configurator = new JobConfigurator(q);

            // --- INSTITUTION DICTIONARIES ---
            configurator.AddDictionaryJob<UpdateInstitutionKindJob>();
            configurator.AddDictionaryJob<UpdateInstitutionStatusJob>();
            configurator.AddDictionaryJob<UpdateUniversityTypeJob>();
            configurator.AddDictionaryJob<UpdateScientificInstitutionTypeJob>();

            // --- COURSES DICTIONARIES ---
            configurator.AddDictionaryJob<UpdateCourseFormJob>();
            configurator.AddDictionaryJob<UpdateCourseInstanceStatusJob>();
            configurator.AddDictionaryJob<UpdateCourseLevelJob>();
            configurator.AddDictionaryJob<UpdateCourseProfileJob>();
            configurator.AddDictionaryJob<UpdateCourseStatusJob>();
            configurator.AddDictionaryJob<UpdateLanguageJob>();
            configurator.AddDictionaryJob<UpdateProfessionalTitleJob>();

            // --- SHARED DICTIONARIES ---
            configurator.AddDictionaryJob<UpdateDisciplineJob>();

            configurator.AddJob<UpdateCourseJob>();
            configurator.AddJob<UpdateInstitutionJob>();
        });
        //services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}
