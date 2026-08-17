using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;

namespace Diploma.Domain.Recruitments.Aggregates;

public interface IRecruitmentRepository
{
    Task<OptionalResult<Recruitment>> GetAsync(
        RecruitmentId id,
        CancellationToken cancellationToken = default);

    Task<OptionalResult<Recruitment>> GetAsync(
        PersonId personId,
        ProjectId projectId,
        CancellationToken cancellationToken = default);

    Task CreateAsync(
        Recruitment item,
        CancellationToken cancellationToken = default);

    Task<ExistingResult> UpdateAsync(
        PersonId personId,
        Recruitment item,
        CancellationToken cancellationToken = default);
}