using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Models.Projects;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.Projects.Commands.UseCases;

public class ProjectDeleteHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRepository projectRepository,
    IProjectManagerRepository managerRepository
    ) : IRequestHandler<ProjectDeleteHandler.Request, ProjectDeleteResult>
{

    public sealed record Request : IRequest<ProjectDeleteResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectId { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ];


    public async Task<ProjectDeleteResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectDeleteResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectDeleteResult.Failure.Forbidden();

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
            return new ProjectDeleteResult.Failure.Forbidden();

        var projectResult = await projectRepository.GetAsync(request.ProjectId, cancellationToken);

        if (!projectResult.HasValue)
            return new ProjectDeleteResult.Failure.NotFound();

        await projectRepository.DeleteAsync(
            request.PersonId,
            projectResult.Value,
            cancellationToken
        );

        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectDeleteResult.Success();
    }
}