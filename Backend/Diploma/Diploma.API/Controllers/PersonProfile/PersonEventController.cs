using Diploma.API.Extensions;
using Diploma.Application.Persons.Queries.Profile.UseCases;
using Diploma.Models.PersonEvents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.PersonProfile;

[Route("api/person/profile/events")]
[ApiController]
public class PersonEventController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] PersonEventQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonGetEventsHandler.Request
        {
            PersonId = personId.Value,
            Model = queryParameters,
        }, cancellationToken);

        return result switch
        {
            PersonEventQueryResult.Success success => Ok(success.Response),
            PersonEventQueryResult.Failure.NotFound => NotFound(),
            PersonEventQueryResult.Failure.ProfileInactive => Conflict(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEventQueryResult)}: {result.GetType()}"),
        };
    }
}