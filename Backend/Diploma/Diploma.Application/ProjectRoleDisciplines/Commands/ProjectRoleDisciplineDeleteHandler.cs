using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.ProjectRoleDisciplines.Aggregates;
using Diploma.Models.ProjectRoleDisciplines;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.ProjectRoleDisciplines.Commands;

public class ProjectRoleDisciplineDeleteHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectManagerRepository managerRepository,
    IProjectRoleDisciplineRepository repository
    ) : IRequestHandler<ProjectRoleDisciplineDeleteHandler.Request, ProjectRoleDisciplineDeleteResult>
{
    public sealed record Request : IRequest<ProjectRoleDisciplineDeleteResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectRoleDisciplineId { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Moderator,
        ];


    public async Task<ProjectRoleDisciplineDeleteResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectRoleDisciplineDeleteResult.Failure.NotFound();

        var person = personResult.Value;

        var projectRoleDisciplineResult = await repository.GetAsync((ProjectRoleDisciplineId)request.ProjectRoleDisciplineId, cancellationToken);

        if (!projectRoleDisciplineResult.HasValue)
            return new ProjectRoleDisciplineDeleteResult.Failure.NotFound();

        var projectRoleDiscipline = projectRoleDisciplineResult.Value;

        var personRoles = await managerRepository.GetAsync(
            request.PersonId,
            projectRoleDiscipline.ProjectId,
            cancellationToken);

        var countRoles = personRoles
            .Select(i => i.ProjectManagerRole)
            .ToHashSet()
            .Intersect(availableRoles)
            .Count();

        if (countRoles == 0)
            return new ProjectRoleDisciplineDeleteResult.Failure.Forbidden();


        await repository.DeleteAsync(request.PersonId, projectRoleDiscipline, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectRoleDisciplineDeleteResult.Success();
    }
}