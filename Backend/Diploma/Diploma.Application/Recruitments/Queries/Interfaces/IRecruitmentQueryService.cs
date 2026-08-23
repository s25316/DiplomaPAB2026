using Diploma.Models.Recruitments;
using Diploma.Models.Shared;

namespace Diploma.Application.Recruitments.Queries.Interfaces;

public interface IRecruitmentQueryService
{
    Task<Response<RecruitmentDto>> GetByPersonIdAsync(
        Guid personId,
        RecruitmentQueryParameters parameters,
        CancellationToken cancellationToken = default);

    Task<Response<RecruitmentDto>> GetByProjectIdAsync(
        Guid projectId,
        RecruitmentQueryParameters parameters,
        CancellationToken cancellationToken = default);
}