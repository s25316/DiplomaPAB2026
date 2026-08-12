using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Models.ProjectRoles;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;
using DomainProjectRole = Diploma.Domain.ProjectRoles.Aggregates.ProjectRole;

namespace Diploma.Application.ProjectRoles.Commands.UseCases;

public class ProjectRoleCreateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRepository projectRepository,
    IProjectRoleRepository projectRoleRepository,
    IProjectManagerRepository managerRepository
    ) : IRequestHandler<ProjectRoleCreateHandler.Request, ProjectRoleCreateResult>
{
    public sealed record Request : IRequest<ProjectRoleCreateResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectId { get; init; }
        public required ProjectRoleCreateRequest Model { get; init; }
    }


    private const int MAX_COUNT = 10;

    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Moderator,
        ];


    public async Task<ProjectRoleCreateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectRoleCreateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectRoleCreateResult.Failure.Forbidden();

        var projectResult = await projectRepository.GetAsync(request.ProjectId, cancellationToken);

        if (!projectResult.HasValue)
            return new ProjectRoleCreateResult.Failure.NotFound();

        var personRoles = await managerRepository.GetAsync(
            request.PersonId,
            request.ProjectId,
            cancellationToken);

        var countRoles = personRoles
            .Select(i => i.ProjectManagerRole)
            .ToHashSet()
            .Intersect(availableRoles)
            .Count();

        if (countRoles == 0)
            return new ProjectRoleCreateResult.Failure.Forbidden();

        var totalCount = await projectRoleRepository.TotalCountAsync(request.ProjectId, cancellationToken);

        if (totalCount >= MAX_COUNT)
            return new ProjectRoleCreateResult.Failure.OverMaxLimit(MAX_COUNT);

        var projectRole = DomainProjectRole.Create(
            request.ProjectId,
            request.Model.Title,
            request.Model.Description);

        await projectRoleRepository.CreateAsync(request.PersonId, projectRole, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectRoleCreateResult.Success();
    }
}