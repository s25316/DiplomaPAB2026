using Diploma.Database;
using Diploma.Database.Models.Projects.Recruitments;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.QueryBuilders.Projects;

public class RecruitmentMessageQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<RecruitmentMessage>(
    context
    .RecruitmentMessages
    .AsNoTracking()
    .Include(i => i.Recruitment)
    )
{
    public RecruitmentMessageQueryBuilder WithMessageId(Guid item)
    {
        With(query => query.Where(i => i.RecruitmentMessageId == item));
        return this;
    }

    public RecruitmentMessageQueryBuilder WithRecruitmentId(RecruitmentId item)
    {
        With(query => query.Where(i => i.RecruitmentId == item.Value));
        return this;
    }

    public RecruitmentMessageQueryBuilder WithPersonId(PersonId item)
    {
        With(query => query.Where(i => i.Recruitment.PersonId == item.Value));
        return this;
    }

    public RecruitmentMessageQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.Recruitment.ProjectId == item.Value));
        return this;
    }


    public RecruitmentMessageQueryBuilder WithOrderBy(QueryParametersPagination pagination)
    {
        With(query => query.OrderByDescending(i => i.CreatedAt));
        Paginate(pagination);
        return this;
    }
}