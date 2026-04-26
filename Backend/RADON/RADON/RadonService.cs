using Microsoft.Extensions.DependencyInjection;
using RADON.Base.Responses;
using RADON.Configurations;
using RADON.Courses;
using RADON.Courses.Responses;
using RADON.Dictionaries;
using RADON.Dictionaries.Responses;
using RADON.Institutions;
using RADON.Institutions.Responses;
using System.Net.Http.Json;

namespace RADON;

public interface IRadonService
{
    Task<Response<InstitutionReport>> GetInstitutionsAsync(InstitutionQueryParameters parameters, CancellationToken cancellationToken = default);
    Task<Response<CourseReport>> GetCoursesAsync(CourseQueryParameters parameters, CancellationToken cancellationToken = default);
    Task<IEnumerable<DictValue>> GetDictionariesAsync(DictionaryType type, CancellationToken cancellationToken = default);
}

public class RadonService : IRadonService
{
    private readonly IServiceProvider provider;


    public RadonService()
    {
        var services = new ServiceCollection();

        services.AddHttpClient();

        // --- DICTIONARIES CONFIGURATIONS ---
        // --- INSTITUTION ---
        services.AddSingleton<DictiionaryInstitutionKindsUriConfiguration>();
        services.AddSingleton<DictiionaryInstitutionStatusesUriConfiguration>();
        services.AddSingleton<DictiionaryInstitutionUniversityTypesUriConfiguration>();
        services.AddSingleton<DictiionaryInstitutionScientificInstitutionTypesUriConfiguration>();

        // --- SHARED ---
        services.AddSingleton<DictiionarySharedVoivodeshipsUriConfiguration>();
        services.AddSingleton<DictiionarySharedSupervisingInstitutionsUriConfiguration>();
        services.AddSingleton<DictiionarySharedDisciplinesUriConfiguration>();
        services.AddSingleton<DictiionarySharedDomainsUriConfiguration>();

        // --- COURSE ---
        services.AddSingleton<DictiionaryCourseLevelsUriConfiguration>();
        services.AddSingleton<DictiionaryCourseProfilesUriConfiguration>();
        services.AddSingleton<DictiionaryCourseCurrentStatusesUriConfiguration>();
        services.AddSingleton<DictiionaryCourseLegalBasisTypesUriConfiguration>();
        services.AddSingleton<DictiionaryCourseProfessionalTitlesUriConfiguration>();
        services.AddSingleton<DictiionaryCourseInstanceStatusesUriConfiguration>();
        services.AddSingleton<DictiionaryCourseInstanceFormsUriConfiguration>();
        services.AddSingleton<DictiionaryCoursePhilologicalLanguagesUriConfiguration>();
        services.AddSingleton<DictiionaryCourseMainInstitutionKindsUriConfiguration>();

        // --- REGULAR CONFIGURATIONS ---
        services.AddSingleton<CourseUriConfiguration>();
        services.AddSingleton<InstitutionUriConfiguration>();

        // --- URI BUILDERS ---
        services.AddSingleton<CourseUriBuilder>();
        services.AddSingleton<InstitutionUriBuilder>();

        // --- STRATEGIES ---
        services.AddSingleton<IGetDictionaryTypeConfigurationStrategy, GetDictionaryTypeConfigurationStrategy>();

        provider = services.BuildServiceProvider();
    }

    public async Task<Response<InstitutionReport>> GetInstitutionsAsync(InstitutionQueryParameters parameters, CancellationToken cancellationToken = default)
    {
        var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
        var uriBuilder = provider.GetRequiredService<InstitutionUriBuilder>();

        var requestUri = uriBuilder.Build(parameters);
        var httpClient = httpClientFactory.CreateClient();

        var response = await httpClient.GetFromJsonAsync<Response<InstitutionReport>>(requestUri, cancellationToken);
        ArgumentNullException.ThrowIfNull(response);

        return response;
    }

    public async Task<Response<CourseReport>> GetCoursesAsync(CourseQueryParameters parameters, CancellationToken cancellationToken = default)
    {
        var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
        var uriBuilder = provider.GetRequiredService<CourseUriBuilder>();

        var requestUri = uriBuilder.Build(parameters);
        var httpClient = httpClientFactory.CreateClient();

        var response = await httpClient.GetFromJsonAsync<Response<CourseReport>>(requestUri, cancellationToken);
        ArgumentNullException.ThrowIfNull(response);

        return response;
    }

    public async Task<IEnumerable<DictValue>> GetDictionariesAsync(DictionaryType type, CancellationToken cancellationToken = default)
    {
        var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
        var strategy = provider.GetRequiredService<IGetDictionaryTypeConfigurationStrategy>();

        var configurationType = strategy.Execute(type);
        var configuration = provider.GetRequiredService(configurationType) as DictiionaryUriConfiguration;
        ArgumentNullException.ThrowIfNull(configuration);

        var httpClient = httpClientFactory.CreateClient();

        var response = await httpClient.GetFromJsonAsync<IEnumerable<DictValue>>(configuration.Value.ToString(), cancellationToken);
        ArgumentNullException.ThrowIfNull(response);

        return response;
    }
}