using Diploma.API.Extensions;
using Diploma.Application.PersonUris.Commands.UseCases;
using Diploma.Models.PersonUris;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.PersonProfile;

[Authorize]
[Route("api/person/profile/urls")]
[ApiController]
public class PersonProfileUriController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost()]
    public async Task<IActionResult> CreateAsync(
        PersonUriCreateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonUriCreateHandler.Request
        {
            PersonId = personId.Value,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonUriCreateResult.Success => Created(),
            PersonUriCreateResult.Failure.NotFound => NotFound(),
            PersonUriCreateResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUriCreateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPut("{uriId:guid}")]
    public async Task<IActionResult> UpdateAsync(
        Guid uriId,
        PersonUriUpdateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonUriUpdateHandler.Request
        {
            PersonId = personId.Value,
            UriId = uriId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonUriUpdateResult.Success => Ok(),
            PersonUriUpdateResult.Failure.NotFound => NotFound(),
            PersonUriUpdateResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUriUpdateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpDelete("{uriId:guid}")]
    public async Task<IActionResult> DeleteAsync(
        Guid uriId,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonUriDeleteHandler.Request
        {
            PersonId = personId.Value,
            UriId = uriId,
        }, cancellationToken);

        return result switch
        {
            PersonUriDeleteResult.Success => Ok(),
            PersonUriDeleteResult.Failure.NotFound => NotFound(),
            PersonUriDeleteResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUriDeleteResult)}: {result.GetType()}"),
        };
    }
}