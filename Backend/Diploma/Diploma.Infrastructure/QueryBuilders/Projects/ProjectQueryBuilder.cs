using Diploma.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;
using static Diploma.Models.Projects.ProjectQueryParameters;
using DatabaseProject = Diploma.Database.Models.Projects.Project;

namespace Diploma.Infrastructure.QueryBuilders.Projects;

public class ProjectQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabaseProject>(
    context
    .Projects
    .AsNoTracking()
    .Include(i => i.LastProjectData)
    .Where(i => i.RemovedAt == null)
    )
{
    public ProjectQueryBuilder WithProjectIds(IEnumerable<Guid> items)
    {
        With(query => query.Where(i => items.Contains(i.ProjectId)));
        return this;
    }

    public ProjectQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.ProjectId == item.Value));
        return this;
    }

    public ProjectQueryBuilder WithIsVisible(bool? item)
    {
        if (item == null || item == false)
            return this;

        With(query => query.Where(i => i.LastProjectData.IsVisible == item));
        return this;
    }

    public ProjectQueryBuilder WithManagerPersonId(PersonId? item)
    {
        if (item is null)
            return this;

        With(query => query.Where(i =>
            context.ProjectManagers
                .Include(i => i.GrantEvent)
                .Where(pr => pr.PersonId == item.Value && pr.RevokeEventId == null)
                .Any(pr => i.ProjectId == pr.GrantEvent.ProjectId)
        ));
        return this;
    }

    public ProjectQueryBuilder WithDisciplines(IList<string> items)
    {
        return this;
    }

    public ProjectQueryBuilder WithInstitutions(IList<Guid> items)
    {
        return this;
    }


    public ProjectQueryBuilder WithOrderBy(
        Order order,
        ProjectOrderBy orderBy,
        QueryParametersPagination pagination)
    {
        With(query =>
        {
            return orderBy switch
            {
                ProjectOrderBy.Title => order == Order.Ascending
                    ? query.OrderBy(i => i.LastProjectData.Title)
                    : query.OrderByDescending(i => i.LastProjectData.Title),

                _ => order == Order.Ascending
                    ? query.OrderBy(i => i.CreatedAt)
                    : query.OrderByDescending(i => i.CreatedAt)
            };
        });
        Paginate(pagination);
        return this;
    }
}