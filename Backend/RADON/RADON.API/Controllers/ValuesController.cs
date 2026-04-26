using Microsoft.AspNetCore.Mvc;
using RADON.Base.Responses;
using RADON.Courses;
using RADON.Courses.Responses;
using RADON.Dictionaries;
using RADON.Dictionaries.Responses;
using RADON.Institutions;
using RADON.Institutions.Responses;

namespace RADON.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController(IRadonService service) : ControllerBase
{
    [ProducesResponseType(typeof(Response<InstitutionReport>), 200)]
    [HttpGet("institutions")]
    public async Task<IActionResult> GetInstitutionsAsync(
        [FromQuery] InstitutionQueryParameters queryParameters,
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
        [FromQuery] DictionaryType type,
        CancellationToken cancellationToken)
    {
        var response = await service.GetDictionariesAsync(type, cancellationToken);
        return Ok(response);
    }
}
