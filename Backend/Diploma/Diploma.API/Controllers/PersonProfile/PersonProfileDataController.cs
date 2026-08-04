using Diploma.API.Controllers.Services;
using Diploma.API.Extensions;
using Diploma.Application.Persons.Commands.Profile.UseCases;
using Diploma.Models.Persons.Profile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.PersonProfile;

[Route("api/person/profile")]
[ApiController]
public class PersonProfileDataController(
    IMediator mediator,
    IPersonsService personsService
    ) : ControllerBase
{
    [Authorize]
    [HttpPost("identity")]
    public async Task<IActionResult> UpdateIdentityDataAsync(
        PersonUpdateIdentityDataRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonUpdateIdentityDataHandler.Request
        {
            PersonId = personId.Value,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonUpdateIdentityDataResult.Success => NoContent(),
            PersonUpdateIdentityDataResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUpdateIdentityDataResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpGet("identity")]
    public async Task<IActionResult> GetIdentityDataAsync(CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        return await personsService.GetIdentityDataAsync(personId.Value, cancellationToken);
    }


    [Authorize]
    [HttpPost("profile")]
    public async Task<IActionResult> UpdateProfileDataAsync(
        PersonUpdateProfileDataRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonUpdateProfileDataHandler.Request
        {
            PersonId = personId.Value,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonUpdateProfileDataResult.Success => NoContent(),
            PersonUpdateProfileDataResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUpdateProfileDataResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfileDataAsync(CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        return await personsService.GetProfileDataAsync(personId.Value, cancellationToken);
    }
}