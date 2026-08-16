using Diploma.Application.Interfaces.Database;
using Diploma.Domain.EducationDisciplines.ValueObjects;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.ProjectRoleDisciplines.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Models.ProjectRoleDisciplines;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;
using DomainProjectRoleDiscipline = Diploma.Domain.ProjectRoleDisciplines.Aggregates.ProjectRoleDiscipline;

namespace Diploma.Application.ProjectRoleDisciplines.Commands;

public class ProjectRoleDisciplineCreateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRoleRepository projectRoleRepository,
    IProjectRoleDisciplineRepository repository,
    IEducationDisciplineRepository disciplineRepository,
    IProjectManagerRepository managerRepository
    ) : IRequestHandler<ProjectRoleDisciplineCreateHandler.Request, ProjectRoleDisciplineCreateResult>
{
    public sealed record Request : IRequest<ProjectRoleDisciplineCreateResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectId { get; init; }
        public required Guid ProjectRoleId { get; init; }
        public required ProjectRoleDisciplineCreateRequest Model { get; init; }
    }


    private const int MAX_COUNT = 10;

    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Moderator,
        ];


    public async Task<ProjectRoleDisciplineCreateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var disciplines = await disciplineRepository.GetAsync(cancellationToken);

        if (!disciplines.ContainsKey(request.Model.DisciplineCode))
            return new ProjectRoleDisciplineCreateResult.Failure.NotFound();

        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectRoleDisciplineCreateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectRoleDisciplineCreateResult.Failure.Forbidden();

        var projectRoleResult = await projectRoleRepository.GetAsync(request.ProjectRoleId, cancellationToken);

        if (!projectRoleResult.HasValue)
            return new ProjectRoleDisciplineCreateResult.Failure.NotFound();

        if (projectRoleResult.Value.ProjectId.Value != request.ProjectId)
            return new ProjectRoleDisciplineCreateResult.Failure.NotFound();

        var projectRole = projectRoleResult.Value;
        ArgumentNullException.ThrowIfNull(projectRole.Id);

        var personRoles = await managerRepository.GetAsync(
            request.PersonId,
            projectRoleResult.Value.ProjectId,
            cancellationToken);

        var countRoles = personRoles
            .Select(i => i.ProjectManagerRole)
            .ToHashSet()
            .Intersect(availableRoles)
            .Count();

        if (countRoles == 0)
            return new ProjectRoleDisciplineCreateResult.Failure.Forbidden();

        var all = await repository.GetAsync(projectRole.Id, cancellationToken);
        var isExist = all
            .Select(i => i.DisciplineCode)
            .ToHashSet()
            .Contains(request.Model.DisciplineCode);

        if (isExist)
            return new ProjectRoleDisciplineCreateResult.Success();

        if (all.Count() >= MAX_COUNT)
            return new ProjectRoleDisciplineCreateResult.Failure.OverMaxLimit(MAX_COUNT);

        var projectRoleDiscipline = DomainProjectRoleDiscipline.Create(
            request.ProjectId,
            request.ProjectRoleId,
            request.Model.DisciplineCode);

        await repository.CreateAsync(request.PersonId, projectRoleDiscipline, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectRoleDisciplineCreateResult.Success();
    }
}