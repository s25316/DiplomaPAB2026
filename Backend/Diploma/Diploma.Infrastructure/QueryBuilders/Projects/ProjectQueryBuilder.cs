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
    .Where(i => i.LastProjectData != null)
    )
{
    public ProjectQueryBuilder WithProjectIds(IEnumerable<Guid> items)
    {
        if (!items.Any())
            return this;

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

        With(query => query.Where(i => i.LastProjectData != null && i.LastProjectData.IsVisible == item));
        return this;
    }

    public ProjectQueryBuilder WithManagerPersonId(PersonId? item)
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

    public ProjectQueryBuilder WithDisciplines(IList<string> items)
    {
        if (!items.Any())
            return this;

        With(query => query.Where(i => context
            .ProjectRoleEducationDisciplines
            .AsNoTracking()
            .Include(d => d.ProjectRole)
            .Where(d => d.RemoveProjectEventId == null)
            .Where(d => items.Contains(d.EducationDisciplineCode))
            .Any(d => d.ProjectRole.ProjectId == i.ProjectId)
        ));
        return this;
    }

    public ProjectQueryBuilder WithIsRecruitmentActive(bool? value)
    {
        if (value is not null)
            return this;

        With(query => query.Where(i => context
            .ProjectRoleEducationDisciplines
            .AsNoTracking()
            .Include(d => d.ProjectRole)
            .ThenInclude(d => d.LastProjectRoleData)
            .Where(d =>
                d.ProjectRole.LastProjectRoleData != null &&
                d.ProjectRole.LastProjectRoleData.IsAvailableRecruitment == value
            ).Any(d => d.ProjectRole.ProjectId == i.ProjectId)
        ));
        return this;
    }

    public ProjectQueryBuilder WithInstitutions(IList<Guid> items)
    {
        if (!items.Any())
            return this;

        With(query => query.Where(i => context
            .ProjectRoleEducationInstitutions
            .AsNoTracking()
            .Include(d => d.ProjectRole)
            .Where(d => d.RemoveProjectEventId == null)
            .Where(d => items.Contains(d.EducationInstitutionId))
            .Any(d => d.ProjectRole.ProjectId == i.ProjectId)
        ));
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
                    ? query.OrderBy(i => i.LastProjectData!.Title)
                    : query.OrderByDescending(i => i.LastProjectData!.Title),

                _ => order == Order.Ascending
                    ? query.OrderBy(i => i.CreatedAt)
                    : query.OrderByDescending(i => i.CreatedAt)
            };
        });
        Paginate(pagination);
        return this;
    }
}