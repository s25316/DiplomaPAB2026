using Diploma.API.Extensions;
using Diploma.Application.Interfaces.Security;
using Diploma.Application.ProjectRoleDisciplines.Commands;
using Diploma.Application.ProjectRoleEducationInstitutions.Commands;
using Diploma.Application.ProjectRoles.Commands.UseCases;
using Diploma.Application.ProjectRoles.Queries.UseCases;
using Diploma.Models.ProjectRoleDisciplines;
using Diploma.Models.ProjectRoleEducationInstitutions;
using Diploma.Models.ProjectRoles;
using Diploma.Models.Projects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.Projects;

[Route("api/projects")]
[ApiController]
public class ProjectRoleController(
    IMediator mediator,
    IJwtValidator validator,
    IJwtNameIdentifierExtractor extractor
    ) : ControllerBase
{
    [HttpGet("projectRoles/all")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] ProjectRoleQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        Guid? personId = null;
        if (Request.TryGetJwt(out var jwt) && validator.IsValid(jwt))
        {
            personId = extractor.Extract(jwt);
        }

        var result = await mediator.Send(new ProjectRoleGetHandler.Request
        {
            PersonId = personId,
            Model = queryParameters,
        }, cancellationToken);
        return result switch
        {
            ProjectRoleQueryResult.Success success => Ok(success),
            ProjectRoleQueryResult.Failure.NotFound => NotFound(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectQueryResult)}: {result.GetType()}"),
        };
    }

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


    [Authorize]
    [HttpPost("{projectId:guid}/projectRoles/{projectRoleId:guid}/disciplines")]
    public async Task<IActionResult> CreateDisciplineAsync(
        Guid projectId,
        Guid projectRoleId,
        ProjectRoleDisciplineCreateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectRoleDisciplineCreateHandler.Request
        {
            PersonId = personId.Value,
            ProjectId = projectId,
            ProjectRoleId = projectRoleId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            ProjectRoleDisciplineCreateResult.Success => Created(),
            ProjectRoleDisciplineCreateResult.Failure.NotFound => NotFound(),
            ProjectRoleDisciplineCreateResult.Failure.Forbidden => Forbid(),
            ProjectRoleDisciplineCreateResult.Failure.OverMaxLimit maxLimit => Conflict($"Przekroczono maksymalny limit {maxLimit.MaxLimit}."),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectRoleDisciplineCreateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpDelete("projectRoles/disciplines/{projectRoleDisciplineId:guid}")]
    public async Task<IActionResult> DeleteDisciplineAsync(
        Guid projectRoleDisciplineId,
        ProjectRoleDisciplineCreateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectRoleDisciplineDeleteHandler.Request
        {
            PersonId = personId.Value,
            ProjectRoleDisciplineId = projectRoleDisciplineId,
        }, cancellationToken);

        return result switch
        {
            ProjectRoleDisciplineDeleteResult.Success => Created(),
            ProjectRoleDisciplineDeleteResult.Failure.NotFound => NotFound(),
            ProjectRoleDisciplineDeleteResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectRoleDisciplineDeleteResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPost("{projectId:guid}/projectRoles/{projectRoleId:guid}/institutions")]
    public async Task<IActionResult> CreateInstitutionAsync(
        Guid projectId,
        Guid projectRoleId,
        ProjectRoleEducationInstitutionCreateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectRoleEducationInstitutionCreateHandler.Request
        {
            PersonId = personId.Value,
            ProjectId = projectId,
            ProjectRoleId = projectRoleId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            ProjectRoleEducationInstitutionCreateResult.Success => Created(),
            ProjectRoleEducationInstitutionCreateResult.Failure.NotFound => NotFound(),
            ProjectRoleEducationInstitutionCreateResult.Failure.Forbidden => Forbid(),
            ProjectRoleEducationInstitutionCreateResult.Failure.OverMaxLimit maxLimit => Conflict($"Przekroczono maksymalny limit {maxLimit.MaxLimit}."),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectRoleEducationInstitutionCreateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpDelete("projectRoles/institutions/{projectRoleEducationInstitutionId:guid}")]
    public async Task<IActionResult> DeleteInstitutionAsync(
        Guid projectRoleEducationInstitutionId,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new ProjectRoleEducationInstitutionDeleteHandler.Request
        {
            PersonId = personId.Value,
            ProjectRoleEducationInstitutionId = projectRoleEducationInstitutionId,
        }, cancellationToken);

        return result switch
        {
            ProjectRoleEducationInstitutionDeleteResult.Success => Created(),
            ProjectRoleEducationInstitutionDeleteResult.Failure.NotFound => NotFound(),
            ProjectRoleEducationInstitutionDeleteResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(ProjectRoleEducationInstitutionDeleteResult)}: {result.GetType()}"),
        };
    }
}