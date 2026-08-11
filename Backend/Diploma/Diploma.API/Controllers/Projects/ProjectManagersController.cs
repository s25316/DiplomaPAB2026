using Diploma.API.Extensions;
using Diploma.Application.ProjectManagers.Commands.UseCases;
using Diploma.Models.ProjectManagers;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.Projects;

[Route("api/[controller]")]
[ApiController]
public class ProjectManagersController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpPost("projects/{projectId:guid}/managers")]
    public async Task<IActionResult> GrandAsync(
        Guid projectId,
        ProjectManagerGrandRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectManagerGrandHandler.Request
        {
            PersonId = personId.Value,
            ProjectId = projectId,
            Model = body,
        });

        return result switch
        {
            ProjectManagerGrandResult.Success => Ok(),
            ProjectManagerGrandResult.Failure.NotFound => NotFound(),
            ProjectManagerGrandResult.Failure.Forbidden => Forbid(),
            ProjectManagerGrandResult.Failure.FutureMangerEmptyIdentityData => BadRequest("Profil osoby dla której nadajemy urawnienia jest niekompletny"),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectManagerGrandResult)}: {result.GetType()}"),
        };
    }


    [HttpDelete("projects/managers/{projectManagerId:guid}")]
    public async Task<IActionResult> RevokeAsync(
        Guid projectManagerId,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectManagerRevokeHandler.Request
        {
            PersonId = personId.Value,
            ProjectManagerId = projectManagerId,
        });

        return result switch
        {
            ProjectManagerRevokeResult.Success => Ok(),
            ProjectManagerRevokeResult.Failure.NotFound => NotFound(),
            ProjectManagerRevokeResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectManagerRevokeResult)}: {result.GetType()}"),
        };
    }
}