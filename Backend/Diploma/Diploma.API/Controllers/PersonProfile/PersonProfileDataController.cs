using Diploma.API.Extensions;
using Diploma.Application.Persons.Commands.Profile.UseCases;
using Diploma.Application.Persons.Queries.Profile.UseCases;
using Diploma.Models.Persons.Profile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.PersonProfile;

[Route("api/person/profile")]
[ApiController]
public class PersonProfileDataController(IMediator mediator) : ControllerBase
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

        var result = await mediator.Send(new PersonGetIdentityDataHandler.Request
        {
            PersonId = personId.Value,
        }, cancellationToken);

        return result switch
        {
            PersonIdentityDataQueryResult.Success success => Ok(success.Response),
            PersonIdentityDataQueryResult.Failure.NotFound => NotFound(),
            PersonIdentityDataQueryResult.Failure.ProfileInactive => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonIdentityDataQueryResult)}: {result.GetType()}"),
        };
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

        var result = await mediator.Send(new PersonGetProfileDataHandler.Request
        {
            PersonId = personId.Value,
        }, cancellationToken);

        return result switch
        {
            PersonProfileDataQueryResult.Success success => Ok(success.Response),
            PersonProfileDataQueryResult.Failure.NotFound => NotFound(),
            PersonProfileDataQueryResult.Failure.ProfileInactive => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonProfileDataQueryResult)}: {result.GetType()}"),
        };
    }
}