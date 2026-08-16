using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;

namespace Diploma.Domain.ProjectRoleEducationInstitutions.Aggregates;

public interface IProjectRoleEducationInstitutionRepository
{
    Task<OptionalResult<ProjectRoleEducationInstitution>> GetAsync(ProjectRoleEducationInstitutionId id, CancellationToken cancellationToken = default);
    Task<int> TotalCountAsync(ProjectRoleId id, CancellationToken cancellationToken = default);
    Task CreateAsync(
        PersonId personId,
        ProjectRoleEducationInstitution item,
        CancellationToken cancellationToken = default);

    Task<ExistingResult> DeleteAsync(
        PersonId personId,
        ProjectRoleEducationInstitution item,
        CancellationToken cancellationToken = default);
}