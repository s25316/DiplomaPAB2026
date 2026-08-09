using Diploma.Database;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Shared.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using DatabaseProjectRole = Diploma.Database.Models.Projects.ProjectRoles.ProjectRole;

namespace Diploma.Infrastructure.QueryBuilders.Projects;

public class ProjectRoleQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabaseProjectRole>(
    context
    .ProjectRoles
    .AsNoTracking()
    .Include(i => i.ProjectEvent)
    .Where(i => i.NextId == null)
    .Where(i => i.ProjectEvent.ProjectEventTypeId != ProjectEvent.ProjectRoleRemoved.Id)
    )
{
    public ProjectRoleQueryBuilder WithProjectRoleId(ProjectRoleId item)
    {
        With(query => query.Where(i =>
            i.ProjectRoleId == item.Value ||
            (i.Root != null && i.Root.ProjectRoleId == item.Value)
        ));
        return this;
    }

    public ProjectRoleQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.ProjectEvent.ProjectId == item.Value));
        return this;
    }
}
