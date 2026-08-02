using static GUS.REGON.Models.Responses.Report;

namespace Frontend.Services;

public interface IRegonService
{
    Task<List<Full>?> GetAsync(IEnumerable<string> regons);
}

public class RegonService(
    IBackendHttpClientFactory clientFactory
    ) : IRegonService
{
    public async Task<List<Full>?> GetAsync(IEnumerable<string> regons)
    {
        using var client = await clientFactory.CreateUnAuthorizedClientAsync();

        var queryString = string.Join("&", regons.Select(r => $"Regon={Uri.EscapeDataString(r)}"));
        var url = $"api/regon/institutions?{queryString}";

        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<List<Full>>();
            return result;
        }

        return null;
    }
}
