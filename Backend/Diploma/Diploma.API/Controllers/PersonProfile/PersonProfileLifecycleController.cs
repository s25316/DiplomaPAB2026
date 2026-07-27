using Diploma.API.Extensions;
using Diploma.Application.Persons.Commands.Lifecycle.UseCases;
using Diploma.Models.Persons.Lifecycle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.PersonProfile;

[Route("api/person/profile")]
[ApiController]
public class PersonProfileLifecycleController(IMediator mediator) : ControllerBase
{
    [HttpPost()]
    public async Task<IActionResult> CreateAsync(
        PersonCreateRequest body,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonCreateHandler.Request
        {
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonCreateResult.Success success => Ok(success),
            PersonCreateResult.Failure.LoginTaken => Conflict("Wybierz inny login."),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonCreateResult)}: {result.GetType()}"),
        };
    }


    [HttpPost("activate/{operationId}")]
    public async Task<IActionResult> ActivateAsync(
        Guid operationId,
        PersonActivateRequest body,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonActivateHandler.Request
        {
            OperationId = operationId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonActivateResult.Success => NoContent(),
            PersonActivateResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonActivateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpDelete()]
    public async Task<IActionResult> RemoveAsync(CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonRemoveHandler.Request
        {
            PersonId = personId.Value,
        }, cancellationToken);

        return result switch
        {
            PersonRemoveResult.Success => NoContent(),
            PersonRemoveResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonActivateResult)}: {result.GetType()}"),
        };
    }


    [HttpPost("restore/{operationId}")]
    public async Task<IActionResult> RemoveAsync(
        Guid operationId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonRestoreHandler.Request
        {
            OperationId = operationId,
        }, cancellationToken);

        return result switch
        {
            PersonRestoreResult.Success => NoContent(),
            PersonRestoreResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonActivateResult)}: {result.GetType()}"),
        };
    }
}