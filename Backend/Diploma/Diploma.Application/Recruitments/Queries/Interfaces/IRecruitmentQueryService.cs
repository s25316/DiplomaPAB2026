using Diploma.Models.Recruitments;
using Diploma.Models.Shared;

namespace Diploma.Application.Recruitments.Queries.Interfaces;

public interface IRecruitmentQueryService
{
    Task<Response<RecruitmentDto>> GetAsync(
        RecruitmentQueryParameters parameters,
        CancellationToken cancellationToken = default);
}