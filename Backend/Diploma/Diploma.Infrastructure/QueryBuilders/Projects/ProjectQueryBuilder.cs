using Diploma.Database;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Shared.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using DatabaseProject = Diploma.Database.Models.Projects.Project;

namespace Diploma.Infrastructure.QueryBuilders.Projects;

public class ProjectQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabaseProject>(
    context
    .Projects
    .AsNoTracking()
    .Include(i => i.Root)
    .Where(i => i.Next == null)
    .Where(i => context.ProjectEvents.Count(pe =>
        pe.ProjectEventTypeId != ProjectEvent.ProjectRemoved.Id &&
        pe.ProjectId == i.ProjectId || (i.Root != null && pe.ProjectId == i.Root.ProjectId)
    ) == 0)
    )
{
    public ProjectQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.ProjectId == item.Value || (i.Root != null && i.Root.ProjectId == item.Value)));
        return this;
    }

    public ProjectQueryBuilder WithIsVisible(bool item)
    {
        With(query => query.Where(i => i.IsVisible == item));
        return this;
    }
}