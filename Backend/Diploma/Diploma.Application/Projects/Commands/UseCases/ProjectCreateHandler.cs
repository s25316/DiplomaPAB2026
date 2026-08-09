using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Models.Projects;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.Projects.Commands.UseCases;

public class ProjectCreateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRepository projectRepository,
    IProjectManagerRepository managerRepository
    ) : IRequestHandler<ProjectCreateHandler.Request, ProjectCreateResult>
{
    public sealed record Request : IRequest<ProjectCreateResult>
    {
        public required Guid PersonId { get; init; }
        public required ProjectCreateRequest Model { get; init; }
    }


    public async Task<ProjectCreateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectCreateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectCreateResult.Failure.Forbidden();

        if (!person.HasIdentityData)
            return new ProjectCreateResult.Failure.ProfileIsEmpty();


        var project = Project.Create(request.Model.Title, request.Model.Description);
        await projectRepository.CreateAsync(request.PersonId, project, cancellationToken);

        ArgumentNullException.ThrowIfNull(project.Id);
        var projectManager = ProjectManager.Create(
            request.PersonId,
            project.Id,
            ProjectManagerRole.Creator);

        await managerRepository.GrantAsync(request.PersonId, projectManager, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectCreateResult.Success();
    }
}