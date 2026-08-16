using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;

namespace Diploma.Domain.ProjectRoleDisciplines.Aggregates;

public interface IProjectRoleDisciplineRepository
{
    Task<OptionalResult<ProjectRoleDiscipline>> GetAsync(ProjectRoleDisciplineId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProjectRoleDiscipline>> GetAsync(ProjectRoleId id, CancellationToken cancellationToken = default);
    Task<int> TotalCountAsync(ProjectRoleId id, CancellationToken cancellationToken = default);
    Task CreateAsync(
        PersonId personId,
        ProjectRoleDiscipline item,
        CancellationToken cancellationToken = default);

    Task<ExistingResult> DeleteAsync(
        PersonId personId,
        ProjectRoleDiscipline item,
        CancellationToken cancellationToken = default);
}