using Diploma.Application.RecruitmentMessages.Queries.Interfaces;
using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Models.RecruitmentMessages;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.RecruitmentMessages.QueryServices;

public class RecruitmentMessageQueryService(
    RecruitmentMessageQueryBuilder builder
    ) : IRecruitmentMessageQueryService
{
    public async Task<Response<RecruitmentMessageDto>> GetAsync(
        RecruitmentId recruitmentId,
        RecruitmentMessageQueryParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var baseQuery = builder
            .WithRecruitmentId(recruitmentId)
            .Build();

        var query = builder
            .WithOrderBy(parameters.Pagination)
            .Build();

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        var items = await query.ToListAsync();

        return new Response<RecruitmentMessageDto>
        {
            Items = items.Select(i => new RecruitmentMessageDto
            {

                RecruitmentMessageId = i.RecruitmentMessageId,
                Message = i.Message,
                File = i.File,
                PersonId = i.PersonId,
                CreatedAt = i.CreatedAt,
            }).ToList(),
            Pagination = new ResponsePagination
            {
                TotalCount = totalCount,
                ItemsPerPage = parameters.Pagination.ItemsPerPage,
                Page = parameters.Pagination.Page,
            },
        };
    }
}