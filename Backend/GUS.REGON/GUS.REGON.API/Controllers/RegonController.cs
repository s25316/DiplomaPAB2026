using GUS.REGON.Models;
using GUS.REGON.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GUS.REGON.API.Controllers;

[Route("api")]
[ApiController]
public class RegonController : ControllerBase
{
    [ProducesResponseType(typeof(IEnumerable<Report.Full>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("institutions")]
    public async Task<IActionResult> GetAsync(
        [FromServices] RegonService regonService,
        [FromQuery] QueryParameters parameters, CancellationToken cancellationToken)
    {
        return Ok();
    }
}