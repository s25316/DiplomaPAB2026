using Diploma.Application.Projects.Queries.Interfaces;
using Diploma.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Models.Projects;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.Projects.QueryServices;

public class ProjectQueryService(
    DiplomaDbContext context,
    ProjectQueryBuilder builder
    ) : IProjectQueryService
{
    public async Task<Response<ProjectDto>> GetAsync(
        PersonId? personId,
        bool? isVisible,
        bool isPersonItems,
        ProjectQueryParameters queryParameters,
        CancellationToken cancellationToken = default)
    {
        builder
            .WithProjectIds(queryParameters.ProjectIds)
            .WithIsVisible(isVisible)
            .WithDisciplines(queryParameters.Disciplines)
            .WithInstitutions(queryParameters.Institutions);

        if (isPersonItems)
            builder.WithManagerPersonId(personId);

        var baseQuery = builder.Build();
        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var query = builder
            .WithOrderBy(queryParameters.Order, queryParameters.OrderBy, queryParameters.Pagination)
            .Build();

        var databaseItems = await query.Select(i => new
        {
            Item = i,
            Disciplines = context
                .ProjectRoleEducationDisciplines
                .Include(i => i.ProjectRole)
                .Include(i => i.EducationDiscipline)
                .Where(d => d.ProjectRole.ProjectId == i.ProjectId)
                .Where(d => d.ProjectRole.RemovedAt == null)
                .Where(d => d.RemoveProjectEventId == null)
                .Select(d => new
                {
                    d.ProjectRoleEducationDisciplineId,
                    d.EducationDiscipline
                })
                .ToList(),
            Institutions = context
                .ProjectRoleEducationInstitutions
                .Include(i => i.ProjectRole)
                .Where(d => d.ProjectRole.ProjectId == i.ProjectId)
                .Where(d => d.ProjectRole.RemovedAt == null)
                .Where(d => d.RemoveProjectEventId == null)
                .Select(d => new
                {
                    d.ProjectRoleEducationInstitutionId,
                    d.EducationInstitutionId,
                })
                .ToList(),

            IsAvailableRecruitment = context
                .ProjectRoles
                .Include(d => d.LastProjectRoleData)
                .Where(d => d.ProjectId == i.ProjectId)
                .Where(d => d.RemovedAt == null)
                .Any(d => d.LastProjectRoleData != null && d.LastProjectRoleData.IsAvailableRecruitment),

            IsRecruted = context
                .Recruitments
                .Any(d =>
                    personId != null &&
                    d.PersonId == personId.Value &&
                    d.RecruitmentProjectRoles.Any(r => r.ProjectRole.ProjectId == i.ProjectId)
                ),

        }).ToListAsync(cancellationToken);

        var items = databaseItems.Select(i => new ProjectDto
        {
            ProjectId = i.Item.ProjectId,
            CreatedAt = i.Item.CreatedAt,
            Title = i.Item.LastProjectData?.Title ?? string.Empty,
            Description = i.Item.LastProjectData?.Description ?? string.Empty,
            IsVisible = i.Item.LastProjectData?.IsVisible ?? false,
            IsAvailableRecruitment = i.IsAvailableRecruitment,

            Disciplines = i
                .Disciplines
                .Select(d => new Models.Dictionaries.DictionaryItem<string>
                {
                    Code = d.EducationDiscipline.Code,
                    Name = d.EducationDiscipline.Name,
                }).ToHashSet()
                .ToList(),

            EductionInstitutionIds = i
                .Institutions
                .Select(d => d.EducationInstitutionId)
                .ToHashSet()
                .ToList(),

            IsRecruted = i.IsRecruted,
        }).ToList();

        return new Response<ProjectDto>
        {
            Items = items,
            Pagination = new ResponsePagination
            {
                ItemsPerPage = queryParameters.Pagination.ItemsPerPage,
                Page = queryParameters.Pagination.Page,
                TotalCount = totalCount,
            },
        };
    }
}