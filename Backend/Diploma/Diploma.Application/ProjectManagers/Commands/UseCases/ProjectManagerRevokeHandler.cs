using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Models.ProjectManagers;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.ProjectManagers.Commands.UseCases;

public class ProjectManagerRevokeHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRepository projectRepository,
    IProjectManagerRepository managerRepository
    ) : IRequestHandler<ProjectManagerRevokeHandler.Request, ProjectManagerRevokeResult>
{
    public sealed record Request : IRequest<ProjectManagerRevokeResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectManagerId { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.RoleManager,
        ];


    public async Task<ProjectManagerRevokeResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectManagerRevokeResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectManagerRevokeResult.Failure.Forbidden();

        var personManagerResult = await managerRepository.GetAsync(
            (ProjectManagerId)request.ProjectManagerId,
            cancellationToken);

        if (!personManagerResult.HasValue)
            return new ProjectManagerRevokeResult.Failure.NotFound();

        var personManager = personManagerResult.Value;

        var personRoles = await managerRepository.GetAsync(
            request.PersonId,
            personManager.ProjectId,
            cancellationToken);

        var countRoles = personRoles
            .Select(i => i.ProjectManagerRole)
            .ToHashSet()
            .Intersect(availableRoles)
            .Count();

        if (countRoles == 0)
            return new ProjectManagerRevokeResult.Failure.Forbidden();

        var projectResult = await projectRepository.GetAsync(personManager.ProjectId, cancellationToken);

        if (!projectResult.HasValue)
            return new ProjectManagerRevokeResult.Failure.NotFound();

        await managerRepository.RevokeAsync(request.PersonId, personManager, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectManagerRevokeResult.Success();
    }
}