using Diploma.API.Controllers.Services;
using Diploma.Models.PersonEducations;
using Diploma.Models.PersonEmployments;
using Diploma.Models.PersonUris;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.Persons;

[AllowAnonymous]
[Route("api/persons")]
[ApiController]
public class PersonsController(IPersonsService service) : ControllerBase
{
    [HttpGet("{personId:guid}/identity")]
    public async Task<IActionResult> GetIdentityDataAsync(
        Guid personId,
        CancellationToken cancellationToken
    ) => await service.GetIdentityDataAsync(personId, cancellationToken);


    [HttpGet("{personId:guid}/profile")]
    public async Task<IActionResult> GetProfileDataAsync(
        Guid personId,
        CancellationToken cancellationToken
    ) => await service.GetProfileDataAsync(personId, cancellationToken);


    [HttpGet("{personId:guid}/education/disciplines")]
    public async Task<IActionResult> GetEducationDisciplinesAsync(
        Guid personId,
        CancellationToken cancellationToken
    ) => await service.GetEducationDisciplinesAsync(personId, cancellationToken);


    [HttpGet("{personId:guid}/education")]
    public async Task<IActionResult> GetEducationHistoryAsync(
        Guid personId,
        [FromQuery] PersonEducationQueryParameters queryParameters,
        CancellationToken cancellationToken
    ) => await service.GetEducationHistoryAsync(personId, queryParameters, cancellationToken);


    [HttpGet("{personId:guid}/employments")]
    public async Task<IActionResult> GetEmploymentsAsync(
        Guid personId,
        [FromQuery] PersonEmploymentQueryParameters queryParameters,
        CancellationToken cancellationToken
    ) => await service.GetEmploymentsAsync(personId, queryParameters, cancellationToken);


    [HttpGet("{personId:guid}/uris")]
    public async Task<IActionResult> GetUrisAsync(
        Guid personId,
        [FromQuery] PersonUriQueryParameters queryParameters,
        CancellationToken cancellationToken
    ) => await service.GetUrisAsync(personId, queryParameters, cancellationToken);
}