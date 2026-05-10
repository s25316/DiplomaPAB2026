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
    [ProducesResponseType(typeof(Response<Institution>), 200)]
    [ProducesResponseType(500)]
    [HttpGet()]
    public async Task<IActionResult> GetInstitutionKindsAsync(
        [FromServices] IInstitutionRepository repository,
        [FromQuery] QueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(queryParameters, cancellationToken);
        return Ok(response);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("kinds")]
    public async Task<IActionResult> GetInstitutionKindsAsync(
        [FromServices] IInstitutionKindRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("statuses")]
    public async Task<IActionResult> GetInstitutionStatusesAsync(
        [FromServices] IInstitutionStatusRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("scientificInstitutionTypes")]
    public async Task<IActionResult> GetScientificInstitutionTypesAsync(
        [FromServices] IScientificInstitutionTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("universityTypes")]
    public async Task<IActionResult> GetUniversityTypesAsync(
        [FromServices] IUniversityTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }
}