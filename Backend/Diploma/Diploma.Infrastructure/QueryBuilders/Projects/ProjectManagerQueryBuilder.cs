using Diploma.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Shared.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using DatabaseProjectManager = Diploma.Database.Models.Projects.ProjectManagers.ProjectManager;

namespace Diploma.Infrastructure.QueryBuilders.Projects;

public class ProjectManagerQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabaseProjectManager>(
    context
    .ProjectManagers
    .AsNoTracking()
    .Include(i => i.GrantEvent)
    .Where(i => i.RevokeEventId == null)
    .Where(i => context.ProjectEvents.Count(pe =>
        pe.ProjectEventTypeId == ProjectEvent.ProjectRemoved.Id &&
        pe.ProjectId == i.GrantEvent.ProjectId
    ) == 0)
    )
{
    public ProjectManagerQueryBuilder WithProjectManagerId(ProjectManagerId item)
    {
        With(query => query.Where(i => i.ProjectManagerId == item.Value));
        return this;
    }

    public ProjectManagerQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.GrantEvent.ProjectId == item.Value));
        return this;
    }

    public ProjectManagerQueryBuilder WithPersonId(PersonId item)
    {
        With(query => query.Where(i => i.PersonId == item.Value));
        return this;
    }
}
