using Diploma.Models.RecruitmentMessages;
using Diploma.Models.Shared;

namespace Diploma.Application.RecruitmentMessages.Queries.Interfaces;

public interface IRecruitmentMessageQueryService
{
    Task<Response<RecruitmentMessageDto>> GetAsync(
        RecruitmentMessageQueryParameters parameters,
        CancellationToken cancellationToken = default);
}