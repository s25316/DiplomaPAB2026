using Microsoft.AspNetCore.Mvc;
using RADON.Application.Interfaces.Shared.Dictionaries;
using RADON.Models.Dictionaries.Responses;

namespace RADON.API.Controllers;

[Route("api/shared")]
[ApiController]
public class SharedController : ControllerBase
{
    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("disciplines")]
    public async Task<IActionResult> GetDisciplinesAsync(
        [FromServices] IDisciplineRespository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }
}