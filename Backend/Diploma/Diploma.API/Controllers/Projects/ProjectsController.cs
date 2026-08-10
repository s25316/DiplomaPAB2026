using Diploma.API.Extensions;
using Diploma.Application.Projects.Commands.UseCases;
using Diploma.Application.Projects.Queries.UseCases;
using Diploma.Models.Projects;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.Projects;

[Route("api/projects")]
[ApiController]
public class ProjectsController(IMediator mediator) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] ProjectQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {

        var result = await mediator.Send(new ProjectGetHandler.Request
        {
            Model = queryParameters,
        });
        return result switch
        {
            ProjectQueryResult.Success success => Ok(success.Response),
            ProjectQueryResult.Failure.NotFound => NotFound(),
            ProjectQueryResult.Failure.ProfileInactive => BadRequest("Profil jest nieaktywny"),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectQueryResult)}: {result.GetType()}"),
        };
    }

    [Authorize]
    [HttpGet()]
    public async Task<IActionResult> GetAsync(
        [FromQuery] ProjectQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectPersonGetHandler.Request
        {
            PersonId = personId.Value,
            Model = queryParameters,
        }, cancellationToken);

        return result switch
        {
            ProjectQueryResult.Success success => Ok(success.Response),
            ProjectQueryResult.Failure.NotFound => NotFound(),
            ProjectQueryResult.Failure.ProfileInactive => BadRequest("Profil jest nieaktywny"),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectQueryResult)}: {result.GetType()}"),
        };
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        ProjectCreateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectCreateHandler.Request
        {
            PersonId = personId.Value,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            ProjectCreateResult.Success => Created(),
            ProjectCreateResult.Failure.NotFound => NotFound(),
            ProjectCreateResult.Failure.Forbidden => Forbid(),
            ProjectCreateResult.Failure.ProfileIsEmpty => BadRequest("Profil jest niekompletny wejdz i uzupełnij profil dla utworzenia projektu"),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectCreateResult)}: {result.GetType()}"),
        };
    }

    [Authorize]
    [HttpPut("{projectId:guid}")]
    public async Task<IActionResult> UpdateAsync(
        Guid projectId,
        ProjectUpdateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectUpdateHandler.Request
        {
            PersonId = personId.Value,
            ProjectId = projectId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            ProjectUpdateResult.Success => Ok(),
            ProjectUpdateResult.Failure.NotFound => NotFound(),
            ProjectUpdateResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectUpdateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpDelete("{projectId:guid}")]
    public async Task<IActionResult> DeleteAsync(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectDeleteHandler.Request
        {
            PersonId = personId.Value,
            ProjectId = projectId,
        }, cancellationToken);

        return result switch
        {
            ProjectDeleteResult.Success => Ok(),
            ProjectDeleteResult.Failure.NotFound => NotFound(),
            ProjectDeleteResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectDeleteResult)}: {result.GetType()}"),
        };
    }
}