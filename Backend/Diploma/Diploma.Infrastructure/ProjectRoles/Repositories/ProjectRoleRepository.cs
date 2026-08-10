using Diploma.Database;
using Diploma.Database.Models.Projects.ProjectRoles;
using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Shared.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using DatabaseProjectEvent = Diploma.Database.Models.Projects.ProjectEvents.ProjectEvent;
using DatabaseProjectRole = Diploma.Database.Models.Projects.ProjectRoles.ProjectRole;
using ProjectRole = Diploma.Domain.ProjectRoles.Aggregates.ProjectRole;


namespace Diploma.Infrastructure.ProjectRoles.Repositories;

public class ProjectRoleRepository(
    DiplomaDbContext context,
    ProjectRoleQueryBuilder builder
    ) : IProjectRoleRepository
{
    public async Task<OptionalResult<ProjectRole>> GetAsync(
        ProjectRoleId projectRoleId,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithProjectRoleId(projectRoleId)
            .Build();

        var item = await query.FirstOrDefaultAsync(cancellationToken);

        if (item is null)
            return OptionalResult<ProjectRole>.NotFound();

        var domain = new ProjectRole.Builder()
            .WithId(item.ProjectRoleId)
            .WithTitle(item.LastProjectRoleData?.Title ?? string.Empty)
            .WithDescription(item.LastProjectRoleData?.Description ?? string.Empty)
            .WithIsAvailableRecruitment(item.LastProjectRoleData?.IsAvailableRecruitment ?? false)
            .WithCreatedAt(item.CreatedAt)
            .Build();

        return OptionalResult.Success(domain);
    }

    public async Task<int> TotalCountAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithProjectId(projectId)
            .Build();

        return await query.CountAsync(cancellationToken);
    }

    public async Task CreateAsync(
        PersonId personId,
        ProjectRole item,
        CancellationToken cancellationToken = default)
    {
        var isProjectExist = await IsProjectExistAsync(item.ProjectId, cancellationToken);

        if (!isProjectExist)
            return;

        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = DateTimeOffset.Now,
            PersonId = personId.Value,
            ProjectId = item.ProjectId.Value,
            ProjectEventTypeId = ProjectEvent.ProjectRoleCreated.Id,
        };

        var projectRole = new DatabaseProjectRole
        {
            CreatedAt = item.CreatedAt
        };

        await context.ProjectRoles.AddAsync(projectRole, cancellationToken);
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var data = new ProjectRoleData
        {
            Title = item.Title,
            Description = item.Description,
            IsAvailableRecruitment = item.IsAvailableRecruitment,
            ProjectEvent = projectEvent,
            ProjectRole = projectRole,
        };

        projectRole.LastProjectRoleData = data;

        await context.ProjectRoleDatas.AddAsync(data, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        item.Id = projectRole.ProjectRoleId;
    }

    public async Task<ExistingResult> UpdateAsync(
        PersonId personId,
        ProjectRole item,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);

        var isProjectExist = await IsProjectExistAsync(item.ProjectId, cancellationToken);

        if (!isProjectExist)
            return ExistingResult.NotFound;

        var projectRole = await context
            .ProjectRoles
            .Where(i => i.ProjectRoleId == item.Id.Value)
            .FirstOrDefaultAsync(cancellationToken);

        if (projectRole is null)
            return ExistingResult.NotFound;


        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = DateTimeOffset.Now,
            PersonId = personId.Value,
            ProjectId = item.ProjectId.Value,
            ProjectEventTypeId = ProjectEvent.ProjectRoleCreated.Id,
        };

        var data = new ProjectRoleData
        {
            Title = item.Title,
            Description = item.Description,
            IsAvailableRecruitment = item.IsAvailableRecruitment,
            ProjectEvent = projectEvent,
            ProjectRole = projectRole,
        };

        projectRole.LastProjectRoleData?.Next = data;
        projectRole.LastProjectRoleData = data;

        await context.ProjectRoleDatas.AddAsync(data, cancellationToken);
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ExistingResult.Exist;
    }


    public async Task<ExistingResult> DeleteAsync(
        PersonId personId,
        ProjectRole item,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);

        var isProjectExist = await IsProjectExistAsync(item.ProjectId, cancellationToken);

        if (!isProjectExist)
            return ExistingResult.NotFound;

        var projectRole = await context
            .ProjectRoles
            .Where(i => i.ProjectRoleId == item.Id.Value)
            .FirstOrDefaultAsync(cancellationToken);

        if (projectRole is null)
            return ExistingResult.NotFound;

        projectRole.RemovedAt = DateTimeOffset.Now;
        await context.SaveChangesAsync(cancellationToken);
        return ExistingResult.Exist;
    }

    private async Task<bool> IsProjectExistAsync(ProjectId projectId, CancellationToken cancellationToken = default)
    {
        var project = await context
            .Projects
            .AsNoTracking()
            .Where(i => i.ProjectId == projectId.Value && i.RemovedAt == null)
            .FirstOrDefaultAsync(cancellationToken);

        return project is not null;
    }
}