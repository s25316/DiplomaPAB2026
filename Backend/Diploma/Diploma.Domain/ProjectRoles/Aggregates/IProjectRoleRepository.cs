using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;

namespace Diploma.Domain.ProjectRoles.Aggregates;

public interface IProjectRoleRepository
{
    Task<int> TotalCountAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default);

    Task<OptionalResult<ProjectRole>> GetAsync(
        ProjectRoleId projectRoleId,
        CancellationToken cancellationToken = default);

    Task CreateAsync(
        PersonId personId,
        ProjectRole item,
        CancellationToken cancellationToken = default);

    Task<ExistingResult> UpdateAsync(
        PersonId personId,
        ProjectRole item,
        CancellationToken cancellationToken = default);

    Task<ExistingResult> DeleteAsync(
        PersonId personId,
        ProjectRole item,
        CancellationToken cancellationToken = default);
}