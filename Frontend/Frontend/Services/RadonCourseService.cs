using RADON.Models.Courses;
using RADON.Models.Courses.Responses;
using RADON.Models.Shared;

namespace Frontend.Services;

public interface IRadonCourseService
{
    Task<Response<Course>?> GetAsync(QueryParameters queryParameters);
}

public class RadonCourseService(
    IBackendHttpClientFactory clientFactory
) : IRadonCourseService
{
    public async Task<Response<Course>?> GetAsync(QueryParameters queryParameters)
    {
        try
        {
            using var client = await clientFactory.CreateUnAuthorizedClientAsync();

            var query = new List<string>
            {
                $"Pagination.Page={queryParameters.Pagination.Page}",
                $"Pagination.ItemsPerPage={queryParameters.Pagination.ItemsPerPage}",
                $"OrderBy={((int)queryParameters.OrderBy)}",
                $"Order={((int)queryParameters.Order)}"
            };

            // Podstawowe parametry tekstowe i flagi
            if (!string.IsNullOrEmpty(queryParameters.Name))
                query.Add($"Name={Uri.EscapeDataString(queryParameters.Name)}");

            if (queryParameters.IsTeacherTraining.HasValue)
                query.Add($"IsTeacherTraining={queryParameters.IsTeacherTraining.Value.ToString().ToLower()}");

            if (queryParameters.IsPhilological.HasValue)
                query.Add($"IsPhilological={queryParameters.IsPhilological.Value.ToString().ToLower()}");

            if (queryParameters.IsDual.HasValue)
                query.Add($"IsDual={queryParameters.IsDual.Value.ToString().ToLower()}");

            if (queryParameters.IsBridging.HasValue)
                query.Add($"IsBridging={queryParameters.IsBridging.Value.ToString().ToLower()}");

            if (queryParameters.IsCoopWithVocational.HasValue)
                query.Add($"IsCoopWithVocational={queryParameters.IsCoopWithVocational.Value.ToString().ToLower()}");

            // Kolekcje Guid (np. CourseUuid, InstitutionUuid, CourseInstanceUuid)
            foreach (var uuid in queryParameters.CourseUuids)
                query.Add($"CourseUuid={uuid}");

            foreach (var instUuid in queryParameters.InstitutionUuids)
                query.Add($"InstitutionUuid={instUuid}");

            foreach (var instInstanceUuid in queryParameters.CourseInstanceUuids)
                query.Add($"CourseInstanceUuid={instInstanceUuid}");

            // Kolekcje słownikowe / kodowe
            foreach (var level in queryParameters.LevelCodes)
                query.Add($"LevelCode={Uri.EscapeDataString(level)}");

            foreach (var profile in queryParameters.ProfileCodes)
                query.Add($"ProfileCode={Uri.EscapeDataString(profile)}");

            foreach (var isced in queryParameters.IscedCodes)
                query.Add($"IscedCode={Uri.EscapeDataString(isced)}");

            foreach (var status in queryParameters.StatusCodes)
                query.Add($"StatusCode={Uri.EscapeDataString(status)}");

            foreach (var discipline in queryParameters.DisciplineCodes)
                query.Add($"DisciplineCode={Uri.EscapeDataString(discipline)}");

            foreach (var form in queryParameters.FormCodes)
                query.Add($"FormCode={Uri.EscapeDataString(form)}");

            foreach (var title in queryParameters.ProfessionalTitleCodes)
                query.Add($"ProfessionalTitleCode={Uri.EscapeDataString(title)}");

            foreach (var lang in queryParameters.LanguageCodes)
                query.Add($"LanguageCode={Uri.EscapeDataString(lang)}");

            foreach (var instStatus in queryParameters.InstanceStatusCodes)
                query.Add($"InstanceStatusCode={Uri.EscapeDataString(instStatus)}");

            foreach (var philLang in queryParameters.PhilologicalLanguageCodes)
                query.Add($"PhilologicalLanguageCode={Uri.EscapeDataString(philLang)}");

            var queryString = string.Join("&", query);

            return await client.GetFromJsonAsync<Response<Course>>($"/api/radon/courses?{queryString}");
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}