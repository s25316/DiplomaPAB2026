using Diploma.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Base;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;
using static Diploma.Models.ProjectRoles.ProjectRoleQueryParameters;
using DatabaseProjectRole = Diploma.Database.Models.Projects.ProjectRoles.ProjectRole;

namespace Diploma.Infrastructure.QueryBuilders.Projects;

public class ProjectRoleQueryBuilder(DiplomaDbContext context) : BaseQueryBuilder<DatabaseProjectRole>(
    context
    .ProjectRoles
    .AsNoTracking()
    .Include(i => i.LastProjectRoleData)
    .Include(i => i.Project)
    .ThenInclude(i => i.LastProjectData)
    .Where(i => i.RemovedAt == null)
    .Where(i => i.LastProjectRoleData != null)
    .Where(i => i.Project.RemovedAt == null)
    )
{
    public ProjectRoleQueryBuilder WithProjectIds(IEnumerable<Guid> items)
    {
        if (!items.Any())
            return this;

        With(query => query.Where(i => items.Contains(i.ProjectId)));
        return this;
    }

    public ProjectRoleQueryBuilder WithProjectRoleId(ProjectRoleId item)
    {
        With(query => query.Where(i => i.ProjectRoleId == item.Value));
        return this;
    }

    public ProjectRoleQueryBuilder WithProjectRoleIds(IEnumerable<Guid> items)
    {
        if (!items.Any())
            return this;

        With(query => query.Where(i => items.Contains(i.ProjectRoleId)));
        return this;
    }

    public ProjectRoleQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.ProjectId == item.Value));
        return this;
    }

    public ProjectRoleQueryBuilder WithIsVisible(bool? item)
    {
        if (item == null || item == false)
            return this;

        With(query => query.Where(i => i.Project.LastProjectData != null && i.Project.LastProjectData.IsVisible == item));
        return this;
    }

    public ProjectRoleQueryBuilder WithManagerPersonId(PersonId? item)
    {
        if (item is null)
            return this;

        With(query => query.Where(i =>
            context.ProjectManagers
                .Include(i => i.GrantEvent)
                .Any(pr => pr.PersonId == item.Value
                   && pr.RevokeEventId == null
                   && pr.GrantEvent != null
                   && pr.GrantEvent.ProjectId == i.ProjectId)
        ));
        return this;
    }

    public ProjectRoleQueryBuilder WithDisciplines(IList<string> items)
    {
        if (!items.Any())
            return this;

        return this;
    }

    public ProjectRoleQueryBuilder WithInstitutions(IList<Guid> items)
    {
        if (!items.Any())
            return this;

        return this;
    }

    public ProjectRoleQueryBuilder WithOrderBy(
        Order order,
        ProjectRoleOrderBy orderBy,
        QueryParametersPagination pagination)
    {
        With(query =>
        {
            return orderBy switch
            {
                ProjectRoleOrderBy.Title => order == Order.Ascending
                    ? query.OrderBy(i => i.LastProjectRoleData!.Title)
                    : query.OrderByDescending(i => i.LastProjectRoleData!.Title),

                _ => order == Order.Ascending
                    ? query.OrderBy(i => i.CreatedAt)
                    : query.OrderByDescending(i => i.CreatedAt)
            };
        });
        Paginate(pagination);
        return this;
    }
}