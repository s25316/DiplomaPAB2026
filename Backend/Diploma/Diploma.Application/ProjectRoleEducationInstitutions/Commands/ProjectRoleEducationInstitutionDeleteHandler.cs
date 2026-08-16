using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.ProjectRoleEducationInstitutions.Aggregates;
using Diploma.Models.ProjectRoleEducationInstitutions;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.ProjectRoleEducationInstitutions.Commands;

public class ProjectRoleEducationInstitutionDeleteHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectManagerRepository managerRepository,
    IProjectRoleEducationInstitutionRepository repository
    ) : IRequestHandler<ProjectRoleEducationInstitutionDeleteHandler.Request, ProjectRoleEducationInstitutionDeleteResult>
{
    public sealed record Request : IRequest<ProjectRoleEducationInstitutionDeleteResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectRoleEducationInstitutionId { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Moderator,
        ];


    public async Task<ProjectRoleEducationInstitutionDeleteResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectRoleEducationInstitutionDeleteResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectRoleEducationInstitutionDeleteResult.Failure.Forbidden();

        var projectRoleEducationInstitutionResult = await repository.GetAsync((ProjectRoleEducationInstitutionId)request.ProjectRoleEducationInstitutionId, cancellationToken);

        if (!projectRoleEducationInstitutionResult.HasValue)
            return new ProjectRoleEducationInstitutionDeleteResult.Failure.NotFound();

        var projectRoleEducationInstitution = projectRoleEducationInstitutionResult.Value;

        var personRoles = await managerRepository.GetAsync(
            request.PersonId,
            projectRoleEducationInstitution.ProjectId,
            cancellationToken);

        var countRoles = personRoles
            .Select(i => i.ProjectManagerRole)
            .ToHashSet()
            .Intersect(availableRoles)
            .Count();

        if (countRoles == 0)
            return new ProjectRoleEducationInstitutionDeleteResult.Failure.Forbidden();

        await repository.DeleteAsync(request.PersonId, projectRoleEducationInstitution, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectRoleEducationInstitutionDeleteResult.Success();
    }
}