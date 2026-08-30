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

public class ProjectRoleQueryBuilder : BaseQueryBuilder<DatabaseProjectRole>
{
    private readonly DiplomaDbContext context;

    public ProjectRoleQueryBuilder(DiplomaDbContext context) : base(
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
        this.context = context;
    }

    protected ProjectRoleQueryBuilder(
        DiplomaDbContext context,
        IQueryable<DatabaseProjectRole> query) : base(query)
    {
        this.context = context;
    }

    public ProjectRoleQueryBuilder WithProjectIds(IEnumerable<Guid> items)
    {
        if (!items.Any())
            return new ProjectRoleQueryBuilder(context, query);

        With(query => query.Where(i => items.Contains(i.ProjectId)));
        return new ProjectRoleQueryBuilder(context, query);
    }

    public ProjectRoleQueryBuilder WithProjectRoleId(ProjectRoleId item)
    {
        return new ProjectRoleQueryBuilder(context, query.Where(i => i.ProjectRoleId == item.Value));
    }

    public ProjectRoleQueryBuilder WithProjectRoleIds(IEnumerable<Guid> items)
    {
        if (!items.Any())
            return new ProjectRoleQueryBuilder(context, query);

        With(query => query.Where(i => items.Contains(i.ProjectRoleId)));
        return new ProjectRoleQueryBuilder(context, query);
    }

    public ProjectRoleQueryBuilder WithProjectId(ProjectId item)
    {
        With(query => query.Where(i => i.ProjectId == item.Value));
        return new ProjectRoleQueryBuilder(context, query);
    }

    public ProjectRoleQueryBuilder WithIsVisible(bool? item)
    {
        if (item == null || item == false)
            return new ProjectRoleQueryBuilder(context, query);

        With(query => query.Where(i => i.Project.LastProjectData != null && i.Project.LastProjectData.IsVisible == item));
        return new ProjectRoleQueryBuilder(context, query);
    }

    public ProjectRoleQueryBuilder WithManagerPersonId(PersonId? item)
    {
        if (item is null)
            return new ProjectRoleQueryBuilder(context, query);

        With(query => query.Where(i =>
            context.ProjectManagers
                .Include(i => i.GrantEvent)
                .Any(pr => pr.PersonId == item.Value
                   && pr.RevokeEventId == null
                   && pr.GrantEvent != null
                   && pr.GrantEvent.ProjectId == i.ProjectId)
        ));
        return new ProjectRoleQueryBuilder(context, query);
    }

    public ProjectRoleQueryBuilder WithDisciplines(IList<string> items)
    {
        if (!items.Any())
            return new ProjectRoleQueryBuilder(context, query);

        With(query => query.Where(i => context
            .ProjectRoleEducationDisciplines
            .AsNoTracking()
            .Where(d => d.RemoveProjectEventId == null)
            .Where(d => items.Contains(d.EducationDisciplineCode))
            .Any(d => d.ProjectRoleId == i.ProjectRoleId)
        ));
        return new ProjectRoleQueryBuilder(context, query);
    }

    public ProjectRoleQueryBuilder WithInstitutions(IList<Guid> items)
    {
        if (!items.Any())
            return new ProjectRoleQueryBuilder(context, query);

        With(query => query.Where(i => context
            .ProjectRoleEducationInstitutions
            .AsNoTracking()
            .Where(d => d.RemoveProjectEventId == null)
            .Where(d => items.Contains(d.EducationInstitutionId))
            .Any(d => d.ProjectRoleId == i.ProjectRoleId)
        ));
        return new ProjectRoleQueryBuilder(context, query);
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
        return new ProjectRoleQueryBuilder(context, query);
    }
}