using Diploma.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Microsoft.EntityFrameworkCore;
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

    public RecruitmentQueryBuilder WithPersonId(PersonId item)
    {
        With(query => query.Where(i => i.PersonId == item.Value));
        return this;
    }
}