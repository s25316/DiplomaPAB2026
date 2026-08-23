using Diploma.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;
using static Diploma.Models.Recruitments.RecruitmentQueryParameters;
using DatabaseRecruitment = Diploma.Database.Models.Projects.Recruitments.Recruitment;

namespace Diploma.Infrastructure.QueryBuilders.Projects;

public class RecruitmentQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabaseRecruitment>(
    context
    .Recruitments
    .AsNoTracking()
    .Include(i => i.LastRecruitmentStatusAudit)
    .Include(i => i.RecruitmentProjectRoles)
    )
{
    public RecruitmentQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.ProjectId == item.Value));
        return this;
    }

    public RecruitmentQueryBuilder WithRecruitmentId(RecruitmentId item)
    {
        With(query => query.Where(i => i.RecruitmentId == item.Value));
        return this;
    }

    public RecruitmentQueryBuilder WithStatusId(int? item)
    {
        if (item == null)
            return this;

        With(query => query.Where(i =>
            i.LastRecruitmentStatusAudit != null &&
            i.LastRecruitmentStatusAudit.RecruitmentStatusId == item.Value
        ));
        return this;
    }

    public RecruitmentQueryBuilder WithPersonId(PersonId item)
    {
        With(query => query.Where(i => i.PersonId == item.Value));
        return this;
    }

    public RecruitmentQueryBuilder WithOrderBy(
        Order order,
        RecruitmentOrderBy orderBy,
        QueryParametersPagination pagination)
    {
        With(query =>
        {
            return orderBy switch
            {
                _ => order == Order.Ascending
                    ? query.OrderBy(i => i.CreatedAt)
                    : query.OrderByDescending(i => i.CreatedAt)
            };
        });
        Paginate(pagination);
        return this;
    }
}