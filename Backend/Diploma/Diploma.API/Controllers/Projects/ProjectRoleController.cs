using Diploma.API.Extensions;
using Diploma.Application.ProjectRoles.Commands.UseCases;
using Diploma.Application.ProjectRoles.Queries.UseCases;
using Diploma.Models.ProjectRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.Projects;

[Route("api/projects")]
[ApiController]
public class ProjectRoleController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet("projectRoles")]
    public async Task<IActionResult> GetAsync(
        [FromQuery] ProjectRoleQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectRolePersonGetHandler.Request
        {
            PersonId = personId.Value,
            Model = queryParameters,
        }, cancellationToken);

        return result switch
        {
            ProjectRoleQueryResult.Success success => Ok(success),
            ProjectRoleQueryResult.Failure.NotFound => NotFound(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectRoleCreateResult)}: {result.GetType()}"),
        };
    }

    [Authorize]
    [HttpPost("{projectId:guid}/projectRoles")]
    public async Task<IActionResult> CreateAsync(
        Guid projectId,
        ProjectRoleCreateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectRoleCreateHandler.Request
        {
            PersonId = personId.Value,
            ProjectId = projectId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            ProjectRoleCreateResult.Success => Created(),
            ProjectRoleCreateResult.Failure.NotFound => NotFound(),
            ProjectRoleCreateResult.Failure.Forbidden => Forbid(),
            ProjectRoleCreateResult.Failure.OverMaxLimit maxLimit => Conflict($"Przekroczono maksymalny limit {maxLimit.MaxLimit}."),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectRoleCreateResult)}: {result.GetType()}"),
        };
    }

    [Authorize]
    [HttpPut("projectRoles/{projectRoleId:guid}")]
    public async Task<IActionResult> UpdateAsync(
        Guid projectRoleId,
        ProjectRoleUpdateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectRoleUpdateHandler.Request
        {
            PersonId = personId.Value,
            ProjectRoleId = projectRoleId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            ProjectRoleUpdateResult.Success => Ok(),
            ProjectRoleUpdateResult.Failure.NotFound => NotFound(),
            ProjectRoleUpdateResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectRoleUpdateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpDelete("projectRoles/{projectRoleId:guid}")]
    public async Task<IActionResult> DeleteAsync(
        Guid projectRoleId,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectRoleDeleteHandler.Request
        {
            PersonId = personId.Value,
            ProjectRoleId = projectRoleId,
        }, cancellationToken);

        return result switch
        {
            ProjectRoleDeleteResult.Success => Ok(),
            ProjectRoleDeleteResult.Failure.NotFound => NotFound(),
            ProjectRoleDeleteResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectRoleDeleteResult)}: {result.GetType()}"),
        };
    }
}