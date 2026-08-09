using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;

namespace Diploma.Domain.Projects.Aggregates;

public interface IProjectRepository
{
    Task<OptionalResult<Project>> GetAsync(ProjectId projectId, CancellationToken cancellationToken = default);
    Task CreateAsync(
        PersonId personId,
        Project item,
        CancellationToken cancellationToken = default);

    Task<ExistingResult> UpdateAsync(
        PersonId personId,
        Project item,
        CancellationToken cancellationToken = default);

    Task<ExistingResult> DeleteAsync(
        PersonId personId,
        Project item,
        CancellationToken cancellationToken = default);
}