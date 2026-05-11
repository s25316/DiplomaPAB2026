using Microsoft.AspNetCore.Mvc;
using RADON.Application.Interfaces.Institutions;
using RADON.Application.Interfaces.Institutions.Dictionaries;
using RADON.Models.Dictionaries.Responses;
using RADON.Models.Institutions;
using RADON.Models.Institutions.Responses;
using RADON.Models.Shared;

namespace RADON.API.Controllers;

[Route("api/institutions")]
[ApiController]
public class InstitutionsController : ControllerBase
{
    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetInstitutions_Summary"]/*' />
    /// <remarks>
    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetInstitutions_Description"]/summary' />
    /// </remarks>
    [ProducesResponseType(typeof(Response<Institution>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet()]
    public async Task<IActionResult> GetInstitutionsAsync(
        [FromServices] IInstitutionRepository repository,
        [FromQuery] QueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(queryParameters, cancellationToken);
        return Ok(response);
    }

    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetInstitutionKinds_Summary"]/*' />
    /// <remarks>
    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetInstitutionKinds_Description"]/summary' />
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("kinds")]
    public async Task<IActionResult> GetInstitutionKindsAsync(
        [FromServices] IInstitutionKindRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items.Values);
    }

    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetInstitutionStatuses_Summary"]/*' />
    /// <remarks>
    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetInstitutionStatuses_Description"]/summary' />
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("statuses")]
    public async Task<IActionResult> GetInstitutionStatusesAsync(
        [FromServices] IInstitutionStatusRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items.Values);
    }

    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetScientificInstitutionTypes_Summary"]/*' />
    /// <remarks>
    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetScientificInstitutionTypes_Description"]/summary' />
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("scientificInstitutionTypes")]
    public async Task<IActionResult> GetScientificInstitutionTypesAsync(
        [FromServices] IScientificInstitutionTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items.Values);
    }

    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetUniversityTypes_Summary"]/*' />
    /// <remarks>
    /// <include file='RadonApiDescription.xml' path='docs/members/member[@name="GetUniversityTypes_Description"]/summary' />
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("universityTypes")]
    public async Task<IActionResult> GetUniversityTypesAsync(
        [FromServices] IUniversityTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items.Values);
    }
}