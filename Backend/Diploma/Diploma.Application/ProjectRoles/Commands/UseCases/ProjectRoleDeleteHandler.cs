using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Models.ProjectRoles;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.ProjectRoles.Commands.UseCases;

public class ProjectRoleDeleteHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRoleRepository projectRoleRepository,
    IProjectManagerRepository managerRepository
    ) : IRequestHandler<ProjectRoleDeleteHandler.Request, ProjectRoleDeleteResult>
{
    public sealed record Request : IRequest<ProjectRoleDeleteResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectRoleId { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Moderator,
        ];


    public async Task<ProjectRoleDeleteResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectRoleDeleteResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectRoleDeleteResult.Failure.Forbidden();

        var projectRoleResult = await projectRoleRepository.GetAsync(request.ProjectRoleId, cancellationToken);

        if (!projectRoleResult.HasValue)
            return new ProjectRoleDeleteResult.Failure.NotFound();

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
            return new ProjectRoleDeleteResult.Failure.Forbidden();

        await projectRoleRepository.DeleteAsync(request.PersonId, projectRole, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectRoleDeleteResult.Success();
    }
}