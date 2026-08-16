using Diploma.Database;
using Diploma.Domain.ProjectRoleEducationInstitutions.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Microsoft.EntityFrameworkCore;
using DatabaseItem = Diploma.Database.Models.Projects.ProjectRoles.ProjectRoleEducationInstitution;

namespace Diploma.Infrastructure.QueryBuilders.Projects;

public class ProjectRoleEducationInstitutionQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabaseItem>(
    context
    .ProjectRoleEducationInstitutions
    .AsNoTracking()
    .Include(i => i.AddProjectEvent)
    .Include(i => i.ProjectRole)
    .ThenInclude(i => i.Project)
    .Where(i => i.ProjectRole.Project.RemovedAt == null)
    .Where(i => i.ProjectRole.RemovedAt == null)
    .Where(i => i.RemoveProjectEventId == null)
    )
{
    public ProjectRoleEducationInstitutionQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.ProjectRole.ProjectId == item.Value));
        return this;
    }

    public ProjectRoleEducationInstitutionQueryBuilder WithProjectRoleId(ProjectRoleId item)
    {
        With(query => query.Where(i => i.ProjectRoleId == item.Value));
        return this;
    }

    public ProjectRoleEducationInstitutionQueryBuilder WithProjectRoleDisciplineId(ProjectRoleEducationInstitutionId item)
    {
        With(query => query.Where(i => i.ProjectRoleEducationInstitutionId == item.Value));
        return this;
    }
}