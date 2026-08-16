using Diploma.Database;
using Diploma.Database.Models.Projects.ProjectRoles;
using Diploma.Domain.ProjectRoleDisciplines.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.QueryBuilders.Projects;

public class ProjectRoleEducationDisciplineQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<ProjectRoleEducationDiscipline>(
    context
    .ProjectRoleEducationDisciplines
    .AsNoTracking()
    .Include(i => i.AddProjectEvent)
    .Include(i => i.ProjectRole)
    .ThenInclude(i => i.Project)
    .Where(i => i.ProjectRole.Project.RemovedAt == null)
    .Where(i => i.ProjectRole.RemovedAt == null)
    .Where(i => i.RemoveProjectEventId == null)
    )
{
    public ProjectRoleEducationDisciplineQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.ProjectRole.ProjectId == item.Value));
        return this;
    }

    public ProjectRoleEducationDisciplineQueryBuilder WithProjectRoleId(ProjectRoleId item)
    {
        With(query => query.Where(i => i.ProjectRoleId == item.Value));
        return this;
    }

    public ProjectRoleEducationDisciplineQueryBuilder WithProjectRoleDisciplineId(ProjectRoleDisciplineId item)
    {
        With(query => query.Where(i => i.ProjectRoleEducationDisciplineId == item.Value));
        return this;
    }
}