using Diploma.Application.Interfaces.Database;
using Diploma.Application.Interfaces.Generators;
using Diploma.Application.Interfaces.Repositories;
using Diploma.Application.Interfaces.Security;
using Diploma.Application.Interfaces.Smtp;
using Diploma.Application.PersonEducations.Queries.Interfaces;
using Diploma.Application.PersonEmployments.Queries.Interfaces;
using Diploma.Application.Persons.Commands.Authentication.MessageGenerators;
using Diploma.Application.Persons.Commands.Authentication.Projections.RefreshTokens;
using Diploma.Application.Persons.Commands.Interfaces;
using Diploma.Application.Persons.Commands.Lifecycle.MessageGenerators;
using Diploma.Application.PersonUris.Queries.Interfaces;
using Diploma.Application.ProjectRoles.Queries.Interfaces;
using Diploma.Application.Projects.Queries.Interfaces;
using Diploma.Database;
using Diploma.Database.MsSql;
using Diploma.Domain.Base.Events;
using Diploma.Domain.Companies.Aggregates;
using Diploma.Domain.EducationCourseInstances.Aggregates;
using Diploma.Domain.EducationCourses.Aggregates;
using Diploma.Domain.EducationDisciplines.ValueObjects;
using Diploma.Domain.EducationInstitutions.Aggregates;
using Diploma.Domain.PersonEducations.Aggregates;
using Diploma.Domain.PersonEmployments.Aggregates;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Persons.Events.Authentication;
using Diploma.Domain.Persons.Events.Lifecycle;
using Diploma.Domain.Persons.Events.Profile;
using Diploma.Domain.PersonUris.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.Companies.Repositories;
using Diploma.Infrastructure.Companies.Services;
using Diploma.Infrastructure.Configurations;
using Diploma.Infrastructure.EducationCourseInstances.Repositories;
using Diploma.Infrastructure.EducationCourses.Repositories;
using Diploma.Infrastructure.EducationCourses.Services;
using Diploma.Infrastructure.EducationDisciplines.Repositories;
using Diploma.Infrastructure.EducationDisciplines.Services;
using Diploma.Infrastructure.EducationInstitutions.Repositories;
using Diploma.Infrastructure.EducationInstitutions.Services;
using Diploma.Infrastructure.Jobs;
using Diploma.Infrastructure.Jobs.Educations;
using Diploma.Infrastructure.PersonEducations.QueryServices;
using Diploma.Infrastructure.PersonEducations.Repositories;
using Diploma.Infrastructure.PersonEmployments.QueryServices;
using Diploma.Infrastructure.PersonEmployments.Repositories;
using Diploma.Infrastructure.Persons;
using Diploma.Infrastructure.Persons.Authentication.EventPublishers;
using Diploma.Infrastructure.Persons.Authentication.MessageGenerators;
using Diploma.Infrastructure.Persons.Authentication.Projections;
using Diploma.Infrastructure.Persons.Lifecycle.EventPublishers;
using Diploma.Infrastructure.Persons.Lifecycle.LinkGenerators;
using Diploma.Infrastructure.Persons.Lifecycle.MessageGenerators;
using Diploma.Infrastructure.Persons.Profile.EventPublishers;
using Diploma.Infrastructure.PersonUris.QueryServices;
using Diploma.Infrastructure.PersonUris.Repositories;
using Diploma.Infrastructure.ProjectManagers.Repositories;
using Diploma.Infrastructure.ProjectRoles.QueryServices;
using Diploma.Infrastructure.ProjectRoles.Repositories;
using Diploma.Infrastructure.Projects.QueryServices;
using Diploma.Infrastructure.Projects.Repositories;
using Diploma.Infrastructure.QueryBuilders.Persons;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Infrastructure.Services.Database;
using Diploma.Infrastructure.Services.Generators;
using Diploma.Infrastructure.Services.Repositories;
using Diploma.Infrastructure.Services.Security;
using Diploma.Infrastructure.Services.Smtp;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Diploma.Infrastructure;

public static class Configuration
{
    private const string SECTION_DATABASE = "Database";
    private const string SECTION_EMAIL = "Email";
    private const string SECTION_FRONTEND = "Frontend";
    private const string SECTION_JWT = "Jwt";

    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IOptions<BackendHostConfiguration>>(p =>
        {
            var server = p.GetRequiredService<IServer>();
            var addressesFeature = server.Features.Get<IServerAddressesFeature>();
            var addresses = addressesFeature?.Addresses;

            var address = addresses?.FirstOrDefault();
            ArgumentNullException.ThrowIfNullOrWhiteSpace(address);

            return Options.Create(new BackendHostConfiguration
            {
                Uri = address,
            });
        });

        services.Configure<DatabaseConfiguration>(configuration.GetSection(SECTION_DATABASE));
        services.Configure<EmailConfiguration>(configuration.GetSection(SECTION_EMAIL));
        services.Configure<FrontendHostConfiguration>(configuration.GetSection(SECTION_FRONTEND));
        services.Configure<JwtConfiguration>(configuration.GetSection(SECTION_JWT));



        services.AddDbContext<DiplomaDbContext, DiplomaMsSqlDbContext>();
        /*services.AddDbContext<DiplomaDbContext, DiplomaMsSqlDbContext>((p, c) =>
        {
            var connectionString = p.GetRequiredService<IOptions<DatabaseConfiguration>>().Value.ConnectionString;
            c.UseSqlServer(connectionString);
        });*/

        // JOBS
        services.AddQuartz();

        // INFRASTRUCTURE SERVICES
        services.AddTransient<PersonOperationQueryBuilder>();
        services.AddTransient<PersonUriQueryBuilder>();
        services.AddTransient<PersonEmploymentQueryBuilder>();
        services.AddTransient<PersonEducationQueryBuilder>();
        services.AddTransient<ProjectManagerQueryBuilder>();
        services.AddTransient<ProjectRoleQueryBuilder>();
        services.AddTransient<ProjectQueryBuilder>();

        // APPLICATION SERVICES

        // Database
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

        // Generators
        services.AddSingleton<IStringGenerator, StringGenerator>();

        //Repositories
        services.AddTransient<IEmailRespository, EmailRespository>();

        // Security
        services.AddTransient<JwtSecurityTokenHandler>();
        services.AddTransient<SymmetricSecurityKey>(p =>
        {
            var configuration = p.GetRequiredService<IOptions<JwtConfiguration>>();
            return new(Encoding.UTF8.GetBytes(configuration.Value.Secret));
        });

        services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
        services.AddSingleton<IJwtGenerator, JwtGenerator>();
        services.AddSingleton<IJwtValidator, JwtValidator>();
        services.AddSingleton<IJwtNameIdentifierExtractor, JwtNameIdentifierExtractor>();

        // Smtp
        services.AddSingleton<ISmtpService, SmtpService>();


        // COMPANY
        services.AddTransient<ICompanyService, CompanyService>();
        services.AddTransient<ICompanyRepository, CompanyRepository>();


        // EDUCATION
        services.AddTransient<IEducationCouseService, EducationCouseService>();
        services.AddTransient<IEducationCourseRepository, EducationCourseRepository>();
        services.AddTransient<IEducationCourseInstanceRepository, EducationCourseInstanceRepository>();

        services.AddTransient<IEducationDisciplineService, EducationDisciplineService>();
        services.AddTransient<IEducationDisciplineRepository, EducationDisciplineRepository>();

        services.AddTransient<IEducationInstitutionService, EducationInstitutionService>();
        services.AddTransient<IEducationInstitutionRepository, EducationInstitutionRepository>();


        // PERSON
        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IPersonOperationRepository, PersonOperationRepository>();
        services.AddTransient<IPersonRefreshTokenProjectionService, PersonRefreshTokenProjectionService>();

        services.AddTransient<IPersonUriRepository, PersonUriRepository>();
        services.AddTransient<IPersonUriQueryService, PersonUriQueryService>();

        services.AddTransient<IPersonEducationRepository, PersonEducationRepository>();
        services.AddTransient<IPersonEducationQueryService, PersonEducationQueryService>();
        services.AddTransient<IPersonDisciplineQueryService, PersonDisciplineQueryService>();

        services.AddTransient<IPersonEmploymentRepository, PersonEmploymentRepository>();
        services.AddTransient<IPersonEmploymentQueryService, PersonEmploymentQueryService>();

        services.AddPersonEventPublishers();
        services.AddPersonMessageGenerators();

        // PROJECT
        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddTransient<IProjectManagerRepository, ProjectManagerRepository>();
        services.AddTransient<IProjectRoleRepository, ProjectRoleRepository>();

        services.AddTransient<IProjectQueryService, ProjectQueryService>();
        services.AddTransient<IProjectRoleQueryService, ProjectRoleQueryService>();

        return services;
    }

    private static IServiceCollection AddQuartz(this IServiceCollection services)
    {

        services.AddQuartz(q =>
        {
            q.AddJobListener<JobChainerListener>();
            var configurator = new JobConfigurator(q);

            configurator.AddDictionaryJob<EducationDisciplineJob>();
            configurator.AddJob<EducationInstitutionJob>();
            configurator.AddJob<EducationCouseJobs>();
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }

    private static IServiceCollection AddPersonEventPublishers(this IServiceCollection services)
    {
        services.AddTransient<IEventPublisher<PersonActivatedEvent>, PersonActivatedEventPublisher>();
        services.AddTransient<IEventPublisher<PersonAnonymizedEvent>, PersonAnonymizedEventPublisher>();
        services.AddTransient<IEventPublisher<PersonCretedEvent>, PersonCretedEventPublisher>();
        services.AddTransient<IEventPublisher<PersonRemovedEvent>, PersonRemovedEventPublisher>();
        services.AddTransient<IEventPublisher<PersonRestoredEvent>, PersonRestoredEventPublisher>();

        services.AddTransient<IEventPublisher<PersonLoginInSuccessEvent>, PersonLoginInSuccessEventPublisher>();
        services.AddTransient<IEventPublisher<PersonLoginInUnSuccessEvent>, PersonLoginInUnSuccessEventPublisher>();
        services.AddTransient<IEventPublisher<PersonLogOutEvent>, PersonLogOutEventPublisher>();
        services.AddTransient<IEventPublisher<PersonUpdateLoginEvent>, PersonUpdateLoginEventPublisher>();
        services.AddTransient<IEventPublisher<PersonUpdatePasswordEvent>, PersonUpdatePasswordEventPublisher>();

        services.AddTransient<IEventPublisher<PersonUpdateIdentityDataEvent>, PersonUpdateIdentityDataEventPublisher>();
        services.AddTransient<IEventPublisher<PersonUpdateProfileDataEvent>, PersonUpdateProfileDataEventPublisher>();

        return services;
    }

    private static IServiceCollection AddPersonMessageGenerators(this IServiceCollection services)
    {
        // Link Generators
        services.AddSingleton<IPersonActivationLinkGenerator, PersonActivationLinkGenerator>();
        services.AddSingleton<IPersonRestoreLinkGenerator, PersonRestoreLinkGenerator>();


        services.AddSingleton<IPersonCreateAndActivationMessageGenerator, PersonCreateAndActivationMessageGenerator>();
        services.AddSingleton<IPersonActivatedMessageGenerator, PersonActivatedMessageGenerator>();
        services.AddSingleton<IPersonRemoveMessageGenerator, PersonRemoveMessageGenerator>();
        services.AddSingleton<IPersonRestoreMessageGenerator, PersonRestoreMessageGenerator>();

        services.AddSingleton<IPersonLoginInSuccessMessageGenerator, PersonLoginInSuccessMessageGenerator>();
        services.AddSingleton<IPersonLoginInUnSuccessMessageGenerator, PersonLoginInUnSuccessMessageGenerator>();
        services.AddSingleton<IPersonUpdatedLoginMessageGenerator, PersonUpdatedLoginMessageGenerator>();
        services.AddSingleton<IPersonUpdatedPasswordMessageGenerator, PersonUpdatedPasswordMessageGenerator>();
        services.AddSingleton<IPersonUpdateLoginInitiationMessageGenerator, PersonUpdateLoginInitiationMessageGenerator>();
        services.AddSingleton<IPersonUpdatePasswordRecoveryInitiationMessageGenerator, PersonUpdatePasswordRecoveryInitiationMessageGenerator>();

        return services;
    }
}