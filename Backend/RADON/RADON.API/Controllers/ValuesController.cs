using Microsoft.AspNetCore.Mvc;
using RADON.Contracts.Dictionaries;
using RADON.Contracts.Dictionaries.Responses;
using RADON.Contracts.Institutions;
using RADON.Contracts.Institutions.Responses;
using RADON.Contracts.Shared.Responses;
using RADON.Courses.Responses;
using CourseQueryParameters = RADON.Contracts.Courses.QueryParameters;

namespace RADON.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController(IRadonService service) : ControllerBase
{
    [ProducesResponseType(typeof(Response<InstitutionReport>), 200)]
    [HttpGet("institutions")]
    public async Task<IActionResult> GetInstitutionsAsync(
        [FromQuery] QueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var response = await service.GetInstitutionsAsync(queryParameters, cancellationToken);
        return Ok(response);
    }

    [ProducesResponseType(typeof(Response<CourseReport>), 200)]
    [HttpGet("courses")]
    public async Task<IActionResult> GetCoursesAsync(
        [FromQuery] CourseQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var response = await service.GetCoursesAsync(queryParameters, cancellationToken);
        return Ok(response);
    }

    [ProducesResponseType(typeof(IEnumerable<DictValue>), 200)]
    [HttpGet("dictionaries")]
    public async Task<IActionResult> GetDictionariesAsync(
        [FromQuery] DictionaryResource resource,
        CancellationToken cancellationToken)
    {
        var response = await service.GetDictionariesAsync(resource, cancellationToken);
        return Ok(response);
    }
}
