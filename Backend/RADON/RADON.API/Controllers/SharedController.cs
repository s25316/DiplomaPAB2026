using Microsoft.AspNetCore.Mvc;
using RADON.Application.Interfaces.Shared.Dictionaries;
using RADON.Models.Dictionaries.Responses;

namespace RADON.API.Controllers;

[Route("api/shared")]
[ApiController]
public class SharedController : ControllerBase
{
    /// <include file='ApiDescription.xml' path='docs/members/member[@name="GetDisciplines_Summary"]/*' />
    /// <remarks>
    /// <include file='ApiDescription.xml' path='docs/members/member[@name="GetDisciplines_Description"]/summary' />
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("disciplines")]
    public async Task<IActionResult> GetDisciplinesAsync(
        [FromServices] IDisciplineRespository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items.Values);
    }
}