using Diploma.Application.Interfaces.Database;
using Diploma.Domain.EducationInstitutions.Aggregates;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.ProjectRoleEducationInstitutions.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Models.ProjectRoleEducationInstitutions;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;
using DomainProjectRoleEducationInstitution = Diploma.Domain.ProjectRoleEducationInstitutions.Aggregates.ProjectRoleEducationInstitution;

namespace Diploma.Application.ProjectRoleEducationInstitutions.Commands;

public class ProjectRoleEducationInstitutionCreateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRoleRepository projectRoleRepository,
    IProjectRoleEducationInstitutionRepository repository,
    IEducationInstitutionRepository educationInstitutionRepository,
    IProjectManagerRepository managerRepository
    ) : IRequestHandler<ProjectRoleEducationInstitutionCreateHandler.Request, ProjectRoleEducationInstitutionCreateResult>
{
    public sealed record Request : IRequest<ProjectRoleEducationInstitutionCreateResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectId { get; init; }
        public required Guid ProjectRoleId { get; init; }
        public required ProjectRoleEducationInstitutionCreateRequest Model { get; init; }
    }


    private const int MAX_COUNT = 10;

    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Moderator,
        ];


    public async Task<ProjectRoleEducationInstitutionCreateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var educationInstitutionResult = await educationInstitutionRepository.GetAsync(request.Model.EductioninstitutionId, cancellationToken);

        if (!educationInstitutionResult.HasValue)
            return new ProjectRoleEducationInstitutionCreateResult.Failure.NotFound();

        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectRoleEducationInstitutionCreateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectRoleEducationInstitutionCreateResult.Failure.Forbidden();

        var projectRoleResult = await projectRoleRepository.GetAsync(request.ProjectRoleId, cancellationToken);

        if (!projectRoleResult.HasValue)
            return new ProjectRoleEducationInstitutionCreateResult.Failure.NotFound();

        if (projectRoleResult.Value.ProjectId.Value != request.ProjectId)
            return new ProjectRoleEducationInstitutionCreateResult.Failure.NotFound();

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
            return new ProjectRoleEducationInstitutionCreateResult.Failure.Forbidden();

        var totalCount = await repository.TotalCountAsync(request.ProjectRoleId, cancellationToken);

        if (totalCount >= MAX_COUNT)
            return new ProjectRoleEducationInstitutionCreateResult.Failure.OverMaxLimit(MAX_COUNT);

        var pprojectRoleEducationInstitution = DomainProjectRoleEducationInstitution.Create(
            request.ProjectId,
            request.ProjectRoleId,
            request.Model.EductioninstitutionId);

        await repository.CreateAsync(request.PersonId, pprojectRoleEducationInstitution, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ProjectRoleEducationInstitutionCreateResult.Success();
    }
}