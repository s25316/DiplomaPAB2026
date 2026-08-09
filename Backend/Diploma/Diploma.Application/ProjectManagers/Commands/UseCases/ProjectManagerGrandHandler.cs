using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Models.ProjectManagers;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.ProjectManagers.Commands.UseCases;

public class ProjectManagerGrandHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRepository projectRepository,
    IProjectManagerRepository managerRepository
    ) : IRequestHandler<ProjectManagerGrandHandler.Request, ProjectManagerGrandResult>
{
    public sealed record Request : IRequest<ProjectManagerGrandResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectId { get; init; }
        public required ProjectManagerGrandRequest Model { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.RoleManager,
        ];

    public async Task<ProjectManagerGrandResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectManagerGrandResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectManagerGrandResult.Failure.Forbidden();

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
            return new ProjectManagerGrandResult.Failure.Forbidden();

        var projectResult = await projectRepository.GetAsync(request.ProjectId, cancellationToken);

        if (!projectResult.HasValue)
            return new ProjectManagerGrandResult.Failure.NotFound();

        var project = projectResult.Value;

        ArgumentNullException.ThrowIfNull(project.Id);
        var projectManager = ProjectManager.Create(
            request.PersonId,
            project.Id,
            ProjectManagerRole.Creator);

        await managerRepository.GrantAsync(request.PersonId, projectManager, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectManagerGrandResult.Success();
    }
}