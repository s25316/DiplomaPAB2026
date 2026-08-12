using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Models.ProjectRoles;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.ProjectRoles.Commands.UseCases;

public class ProjectRoleUpdateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRoleRepository projectRoleRepository,
    IProjectManagerRepository managerRepository
    ) : IRequestHandler<ProjectRoleUpdateHandler.Request, ProjectRoleUpdateResult>
{
    public sealed record Request : IRequest<ProjectRoleUpdateResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectRoleId { get; init; }
        public required ProjectRoleUpdateRequest Model { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Moderator,
        ];


    public async Task<ProjectRoleUpdateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectRoleUpdateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectRoleUpdateResult.Failure.Forbidden();

        var projectRoleResult = await projectRoleRepository.GetAsync(request.ProjectRoleId, cancellationToken);

        if (!projectRoleResult.HasValue)
            return new ProjectRoleUpdateResult.Failure.NotFound();

        var projectRole = projectRoleResult.Value;

        var personRoles = await managerRepository.GetAsync(
            request.PersonId,
            projectRole.ProjectId,
            cancellationToken);

        var countRoles = personRoles
            .Select(i => i.ProjectManagerRole)
            .ToHashSet()
            .Intersect(availableRoles)
            .Count();

        if (countRoles == 0)
            return new ProjectRoleUpdateResult.Failure.Forbidden();

        if (projectRole.Title == request.Model.Title &&
            projectRole.Description == request.Model.Description &&
            projectRole.IsAvailableRecruitment == request.Model.IsAvailableRecruitment)
        {
            return new ProjectRoleUpdateResult.Success();
        }

        projectRole.Title = request.Model.Title;
        projectRole.Description = request.Model.Description;
        projectRole.ChangeAvailableRecruitment(request.Model.IsAvailableRecruitment);

        await projectRoleRepository.UpdateAsync(request.PersonId, projectRole, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectRoleUpdateResult.Success();
    }
}