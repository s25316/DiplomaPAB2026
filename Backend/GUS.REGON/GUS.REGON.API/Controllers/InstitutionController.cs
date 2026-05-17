using GUS.REGON.Application.Interfaces;
using GUS.REGON.Models;
using GUS.REGON.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GUS.REGON.API.Controllers;

[Route("api")]
[ApiController]
public class InstitutionController : ControllerBase
{
    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetInstitutions_Summary"]/*' />
    /// <remarks>
    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetInstitutions_Description"]/summary' />
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<Report.Full>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("institutions")]
    public async Task<IActionResult> GetInstitutionsAsync(
        [FromServices] IRequestRepository repository,
        [FromQuery] QueryParameters parameters,
        CancellationToken cancellationToken)
    {
        var requests = await repository.GetAsync(parameters, cancellationToken);
        return Ok(requests);
    }
}