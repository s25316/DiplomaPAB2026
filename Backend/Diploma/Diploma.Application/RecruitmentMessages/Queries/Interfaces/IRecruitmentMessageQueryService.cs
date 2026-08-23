using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Models.RecruitmentMessages;
using Diploma.Models.Shared;

namespace Diploma.Application.RecruitmentMessages.Queries.Interfaces;

public interface IRecruitmentMessageQueryService
{
    Task<Response<RecruitmentMessageDto>> GetAsync(
        RecruitmentId recruitmentId,
        RecruitmentMessageQueryParameters parameters,
        CancellationToken cancellationToken = default);
}