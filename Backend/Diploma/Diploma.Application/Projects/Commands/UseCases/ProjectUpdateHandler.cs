using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Models.Projects;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.Projects.Commands.UseCases;

public class ProjectUpdateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRepository projectRepository,
    IProjectManagerRepository managerRepository
    ) : IRequestHandler<ProjectUpdateHandler.Request, ProjectUpdateResult>
{
    public sealed record Request : IRequest<ProjectUpdateResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectId { get; init; }
        public required ProjectUpdateRequest Model { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Moderator,
        ];


    public async Task<ProjectUpdateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync();
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectUpdateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectUpdateResult.Failure.Forbidden();

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
            return new ProjectUpdateResult.Failure.Forbidden();

        var projectResult = await projectRepository.GetAsync(request.ProjectId, cancellationToken);

        if (!projectResult.HasValue)
            return new ProjectUpdateResult.Failure.NotFound();

        var project = projectResult.Value;

        if (project.Title == request.Model.Title &&
            project.Description == request.Model.Description &&
            project.IsVisible == request.Model.IsVisible)
        {
            return new ProjectUpdateResult.Success();
        }

        project.Title = request.Model.Title;
        project.Description = request.Model.Description;
        project.ChangeVisibility(request.Model.IsVisible);

        await projectRepository.UpdateAsync(
            request.PersonId,
            project,
            cancellationToken
        );

        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectUpdateResult.Success();
    }
}