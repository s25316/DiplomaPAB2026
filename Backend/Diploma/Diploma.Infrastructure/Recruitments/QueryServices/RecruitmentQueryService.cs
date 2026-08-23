using Diploma.Application.ProjectRoles.Queries.Interfaces;
using Diploma.Application.Projects.Queries.Interfaces;
using Diploma.Application.Recruitments.Queries.Interfaces;
using Diploma.Database.Models.Projects.Recruitments;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Models.ProjectRoles;
using Diploma.Models.Recruitments;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;
using SharedRecruitmentStatus = Diploma.Shared.RecruitmentStatuses.RecruitmentStatus;

namespace Diploma.Infrastructure.Recruitments.QueryServices;

public class RecruitmentQueryService(
    RecruitmentQueryBuilder builder,
    IProjectQueryService projectQueryService,
    IProjectRoleQueryService projectRoleQueryService
    ) : IRecruitmentQueryService
{
    public async Task<Response<RecruitmentDto>> GetByPersonIdAsync(
        Guid personId,
        RecruitmentQueryParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var baseQuery = builder
            .WithPersonId(personId)
            .WithStatusId(parameters.StatusId)
            .Build();

        var query = builder
            .WithOrderBy(parameters.Order, parameters.OrderBy, parameters.Pagination)
            .Build();

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        return await GetAsync(totalCount, parameters.Pagination, query, cancellationToken);
    }

    public async Task<Response<RecruitmentDto>> GetByProjectIdAsync(
        Guid projectId,
        RecruitmentQueryParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var baseQuery = builder
            .WithProjectId(projectId)
            .WithStatusId(parameters.StatusId)
            .Build();

        var query = builder
            .WithOrderBy(parameters.Order, parameters.OrderBy, parameters.Pagination)
            .Build();

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        return await GetAsync(totalCount, parameters.Pagination, query, cancellationToken);
    }

    private async Task<Response<RecruitmentDto>> GetAsync(
        int totalCount,
        QueryParametersPagination pagination,
        IQueryable<Recruitment> query,
        CancellationToken cancellationToken = default)
    {
        var databaseItems = await query
            .ToListAsync(cancellationToken);

        var projectIds = databaseItems.Select(i => i.ProjectId).ToList();
        var ProjectRoleIds = databaseItems.SelectMany(i => i.RecruitmentProjectRoles.Select(r => r.ProjectRoleId)).ToList();

        var projectDictionaries = await GetProjectsAsync(projectIds, cancellationToken);
        var projectRolesDictionary = await GetProjectRolesAsync(ProjectRoleIds, cancellationToken);

        var resultItems = new List<RecruitmentDto>();

        foreach (var databaseItem in databaseItems)
        {
            projectDictionaries.TryGetValue(databaseItem.ProjectId, out var project);
            var projectRoles = new List<RecruitmentDto.ProjectRoleRecruitmentDto>();

            foreach (var projectRole in databaseItem.RecruitmentProjectRoles)
            {
                if (projectRolesDictionary.TryGetValue(projectRole.ProjectRoleId, out var projectRoleItem))
                {
                    projectRoles.Add(projectRoleItem);
                }
            }

            var status = SharedRecruitmentStatus.FromId(databaseItem.LastRecruitmentStatusAudit!.RecruitmentStatusId);

            resultItems.Add(new RecruitmentDto
            {
                RecruitmentId = databaseItem.RecruitmentId,
                PersonId = databaseItem.PersonId,
                Project = project,
                ProjectRoles = projectRoles,
                Status = new Models.Dictionaries.DictionaryItem<int>
                {
                    Code = status.Id,
                    Name = status.Name,
                },
            });
        }

        return new Response<RecruitmentDto>
        {
            Items = resultItems,
            Pagination = new ResponsePagination
            {
                ItemsPerPage = pagination.ItemsPerPage,
                Page = pagination.Page,
                TotalCount = totalCount,
            },
        };
    }


    private async Task<Dictionary<Guid, RecruitmentDto.ProjectRecruitmentDto>> GetProjectsAsync(
        IList<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        var projectsResult = await projectQueryService.GetAsync(
            null,
            null,
            false,
            new Models.Projects.ProjectQueryParameters
            {
                ProjectIds = ids,
                IsRecruitmentActive = null,
                Disciplines = [],
                Institutions = [],
                Order = Order.Descending,
                OrderBy = Models.Projects.ProjectQueryParameters.ProjectOrderBy.Title,
                Pagination = new QueryParametersPagination
                {
                    Page = 1,
                    ItemsPerPage = ids.Count,
                }
            },
            cancellationToken);

        return projectsResult.Items.ToDictionary(
            k => k.ProjectId,
            v => new RecruitmentDto.ProjectRecruitmentDto
            {
                ProjectId = v.ProjectId,
                Title = v.Title,
                Description = v.Description,
                CreatedAt = v.CreatedAt,
                Disciplines = v.Disciplines,
                EductionInstitutionIds = v.EductionInstitutionIds,
            });
    }


    private async Task<Dictionary<Guid, RecruitmentDto.ProjectRoleRecruitmentDto>> GetProjectRolesAsync(
        IList<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        var projectsResult = await projectRoleQueryService.GetAsync(
            null,
            false,
            false,
            new ProjectRoleQueryParameters
            {
                ProjectRoleIds = ids,
                ProjectIds = [],
                Disciplines = [],
                Institutions = [],
                Order = Order.Descending,
                OrderBy = ProjectRoleQueryParameters.ProjectRoleOrderBy.Title,
                Pagination = new QueryParametersPagination
                {
                    Page = 1,
                    ItemsPerPage = ids.Count,
                }
            },
            cancellationToken);

        return projectsResult.Items.ToDictionary(
            k => k.ProjectRoleId,
            v => new RecruitmentDto.ProjectRoleRecruitmentDto
            {
                ProjectRoleId = v.ProjectRoleId,
                Title = v.Title,
                Description = v.Description,
                CreatedAt = v.CreatedAt,
                Disciplines = v.Disciplines,
                EductionInstitutionIds = v.EductionInstitutionIds,
            });
    }
}