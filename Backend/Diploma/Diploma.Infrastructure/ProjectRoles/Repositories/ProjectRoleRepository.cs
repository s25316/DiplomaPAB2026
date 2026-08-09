using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Shared.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using DatabaseProjectEvent = Diploma.Database.Models.Projects.ProjectEvents.ProjectEvent;
using DatabaseProjectRole = Diploma.Database.Models.Projects.ProjectRoles.ProjectRole;

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
            .WithId(item.Root?.ProjectRoleId ?? item.ProjectRoleId)
            .WithLastSnapshotId(item.ProjectRoleId)
            .WithTitle(item.Title)
            .WithDescription(item.Description)
            .WithIsAvailableRecruitment(item.IsAvailableRecruitment)
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
            Title = item.Title,
            Description = item.Description,
            IsAvailableRecruitment = item.IsAvailableRecruitment,
            ProjectEvent = projectEvent,
        };

        await context.ProjectRoles.AddAsync(projectRole, cancellationToken);
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        item.Id = projectRole.ProjectRoleId;
    }

    public async Task<ExistingResult> UpdateAsync(
        PersonId personId,
        ProjectRole item,
        CancellationToken cancellationToken = default)
    {
        var isProjectExist = await IsProjectExistAsync(item.ProjectId, cancellationToken);

        if (!isProjectExist)
            return ExistingResult.NotFound;

        return await UpdateAsync(personId, item, ProjectEvent.ProjectRoleUpdated, cancellationToken);
    }


    public async Task<ExistingResult> DeleteAsync(
        PersonId personId,
        ProjectRole item,
        CancellationToken cancellationToken = default)
    {
        var isProjectExist = await IsProjectExistAsync(item.ProjectId, cancellationToken);

        if (!isProjectExist)
            return ExistingResult.NotFound;

        item.ChangeAvailableRecruitment(false);
        return await UpdateAsync(personId, item, ProjectEvent.ProjectRoleRemoved, cancellationToken);
    }

    private async Task<bool> IsProjectExistAsync(ProjectId projectId, CancellationToken cancellationToken = default)
    {
        var project = await context
            .Projects
            .AsNoTracking()
            .Where(i => i.ProjectId == projectId.Value && i.Previous == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (project is null)
            return false;

        var projectRemoved = await context
            .ProjectEvents
            .AsNoTracking()
            .Where(i => i.ProjectId == projectId.Value && i.ProjectEventTypeId == ProjectEvent.ProjectRemoved.Id)
            .FirstOrDefaultAsync(cancellationToken);

        return projectRemoved is null;
    }

    public async Task<ExistingResult> UpdateAsync(
        PersonId personId,
        ProjectRole item,
        ProjectEvent @event,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);

        var databaseItem = await context
            .ProjectRoles
            .Include(i => i.ProjectEvent)
            .Where(i => i.NextId == null)
            .Where(i => i.ProjectEvent.ProjectEventTypeId != ProjectEvent.ProjectRoleRemoved.Id)
            .Where(i =>
                i.ProjectRoleId == item.Id.Value ||
                (i.Root != null && i.Root.ProjectRoleId == item.Id.Value))
            .FirstOrDefaultAsync(cancellationToken);

        if (databaseItem == null)
            return ExistingResult.NotFound;


        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = DateTimeOffset.Now,
            PersonId = personId.Value,
            ProjectId = item.ProjectId.Value,
            ProjectEventTypeId = @event.Id,
        };

        var projectRole = new DatabaseProjectRole
        {
            Title = item.Title,
            Description = item.Description,
            IsAvailableRecruitment = item.IsAvailableRecruitment,
            ProjectEvent = projectEvent,
        };

        databaseItem.Next = projectRole;

        await context.ProjectRoles.AddAsync(projectRole, cancellationToken);
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ExistingResult.Exist;
    }
}