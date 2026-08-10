using Base.Models.Interfaces.QueryBuilders;
using Diploma.Database;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Microsoft.EntityFrameworkCore;
using DatabaseProjectRole = Diploma.Database.Models.Projects.ProjectRoles.ProjectRole;

namespace Diploma.Infrastructure.QueryBuilders.Projects;

public class ProjectRoleQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabaseProjectRole>(
    context
    .ProjectRoles
    .AsNoTracking()
    .Include(i => i.LastProjectRoleData)
    .Include(i => i.Project)
    .Where(i => i.RemovedAt == null)
    .Where(i => i.LastProjectRoleData != null)
    .Where(i => i.Project.RemovedAt == null)
    )
{
    public ProjectRoleQueryBuilder WithProjectRoleId(ProjectRoleId item)
    {
        With(query => query.Where(i => i.ProjectRoleId == item.Value));
        return this;
    }

    public ProjectRoleQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.ProjectId == item.Value));
        return this;
    }
}