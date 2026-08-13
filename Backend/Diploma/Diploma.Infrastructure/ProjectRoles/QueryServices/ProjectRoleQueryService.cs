using Diploma.Application.ProjectRoles.Queries.Interfaces;
using Diploma.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Models.ProjectRoles;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.ProjectRoles.QueryServices;

public class ProjectRoleQueryService(
    DiplomaDbContext context,
    ProjectRoleQueryBuilder builder
    ) : IProjectRoleQueryService
{
    public async Task<Response<ProjectRoleDto>> GetAsync(
        PersonId? personId,
        bool isPersonItems,
        bool? isVisible,
        ProjectRoleQueryParameters queryParameters,
        CancellationToken cancellationToken = default)
    {
        builder
            .WithProjectRoleIds(queryParameters.ProjectRoleIds)
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

            IsRecruted = context
                .Recruitments
                .Any(d =>
                    personId != null &&
                    d.PersonId == personId.Value &&
                    d.RecruitmentProjectRoles.Any(r => r.ProjectRole.ProjectId == i.ProjectId)
                ),

        }).ToListAsync(cancellationToken);


        var items = databaseItems.Select(i => new ProjectRoleDto
        {
            ProjectRoleId = i.Item.ProjectRoleId,
            ProjectId = i.Item.ProjectId,
            CreatedAt = i.Item.CreatedAt,
            Title = i.Item.LastProjectRoleData?.Title ?? string.Empty,
            Description = i.Item.LastProjectRoleData?.Description ?? string.Empty,
            IsAvailableRecruitment = i.Item.LastProjectRoleData?.IsAvailableRecruitment ?? false,
            IsRecruted = i.IsRecruted,

            Disciplines = i.Disciplines
            .Select(d => new ProjectRoleDto.ProjectRoleDiscipline
            {
                ProjectRoleDisciplineId = d.ProjectRoleEducationDisciplineId,
                Discipline = new Models.Dictionaries.DictionaryItem<string>
                {
                    Code = d.EducationDiscipline.Code,
                    Name = d.EducationDiscipline.Name,
                },
            }).ToHashSet().ToList(),

            EductionInstitutionIds = i.Institutions
            .Select(d => new ProjectRoleDto.ProjectRoleEductionInstitution
            {
                ProjectRoleEductionInstitutionId = d.ProjectRoleEducationInstitutionId,
                EductionInstitutionId = d.EducationInstitutionId,
            })
            .ToHashSet()
            .ToList(),
        }).ToList();

        return new Response<ProjectRoleDto>
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