using RADON.Models.Institutions;
using RADON.Models.Institutions.Responses;
using RADON.Models.Shared;

namespace Frontend.Services;

public interface IRadonInstitutionService
{
    Task<Response<Institution>?> GetAsync(QueryParameters queryParameters);
}

public class RadonInstitutionService(
    IBackendHttpClientFactory clientFactory
    ) : IRadonInstitutionService
{

    public async Task<Response<Institution>?> GetAsync(QueryParameters queryParameters)
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

            if (!string.IsNullOrEmpty(queryParameters.Name))
                query.Add($"Name={Uri.EscapeDataString(queryParameters.Name)}");

            foreach (var kind in queryParameters.KindCodes)
                query.Add($"KindCode={Uri.EscapeDataString(kind)}");

            foreach (var status in queryParameters.StatusCodes)
                query.Add($"StatusCode={Uri.EscapeDataString(status)}");

            foreach (var uniType in queryParameters.UniversityTypeCodes)
                query.Add($"UniversityTypeCode={Uri.EscapeDataString(uniType)}");

            foreach (var sciType in queryParameters.ScientificInstitutionTypeCodes)
                query.Add($"ScientificInstitutionTypeCode={Uri.EscapeDataString(sciType)}");

            foreach (var sciType in queryParameters.InstitutionUuids)
                query.Add($"InstitutionUuid={Uri.EscapeDataString(sciType.ToString())}");

            if (queryParameters.Regon is not null)
                query.Add($"Regon={Uri.EscapeDataString(queryParameters.Regon.Value)}");


            var queryString = string.Join("&", query);

            return await client.GetFromJsonAsync<Response<RADON.Models.Institutions.Responses.Institution>>($"/api/radon/institutions?{queryString}");
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}