using Microsoft.AspNetCore.Mvc;
using RADON.Base.Responses;
using RADON.Courses;
using RADON.Institutions;
using RADON.Institutions.Responses;
using System.Text.Json;

namespace RADON.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [ProducesResponseType(typeof(Response<InstitutionReport>), 200)]
    [HttpGet("institutions")]
    public async Task<IActionResult> Get([FromQuery] InstitutionQueryParameters queryParameters)
    {
        var queryBuilder = new InstitutionUriBuilder(new Uri("https://radon.nauka.gov.pl/opendata/polon/institutions"));
        var requestUri = queryBuilder.Build(queryParameters);

        var x = queryParameters.ResultNumbers;
        try
        {
            using var httpClient = new HttpClient();
            // 3. Wysyłamy zapytanie GET
            // GetFromJsonAsync to najszybsza metoda - wysyła i od razu deserializuje
            var response = await httpClient.GetFromJsonAsync<Response<InstitutionReport>>(requestUri);

            if (response == null)
            {
                return NotFound("API zwróciło pustą odpowiedź.");
            }

            var results = response.Results;

            return Ok(response);
        }
        catch (HttpRequestException e)
        {
            // Obsługa błędów sieciowych (np. 404, 500, timeout)
            return StatusCode(500, $"Błąd połączenia z API: {e.Message}");
        }
        catch (JsonException e)
        {
            // Obsługa błędów deserializacji
            return StatusCode(500, $"Błąd przetwarzania danych: {e.Message}");
        }
    }

    [HttpGet("courses")]
    public IActionResult Get([FromQuery] CourseQueryParameters queryParameters)
    {
        return Ok(queryParameters);
    }
}
