using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Shared.ProjectEvents;
using Diploma.Shared.ProjectManagerRoles;
using Microsoft.EntityFrameworkCore;
using DatabaseProjectEvent = Diploma.Database.Models.Projects.ProjectEvents.ProjectEvent;
using DatabaseProjectManager = Diploma.Database.Models.Projects.ProjectManagers.ProjectManager;

namespace Diploma.Infrastructure.ProjectManagers.Repositories;

public class ProjectManagerRepository(
    DiplomaDbContext context,
    ProjectManagerQueryBuilder builder
    ) : IProjectManagerRepository
{
    public async Task<IEnumerable<ProjectManager>> GetAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default)
    {
        var isProjectExist = await IsProjectExistAsync(projectId, cancellationToken);

        if (!isProjectExist)
            return [];

        var query = builder
            .WithProjectId(projectId)
            .Build();

        var items = await query.ToListAsync(cancellationToken);

        return items.Select(Map);
    }

    public async Task<IEnumerable<ProjectManager>> GetAsync(
        PersonId personId,
        ProjectId projectId,
        CancellationToken cancellationToken = default)
    {
        var isProjectExist = await IsProjectExistAsync(projectId, cancellationToken);

        if (!isProjectExist)
            return [];

        var query = builder
            .WithProjectId(projectId)
            .WithPersonId(personId)
            .Build();

        var items = await query.ToListAsync(cancellationToken);

        return items.Select(Map);
    }

    public async Task<ProjectManagerResult.Grant> GrantAsync(
        PersonId personId,
        ProjectManager item,
        CancellationToken cancellationToken = default)
    {
        var isProjectExist = await IsProjectExistAsync(item.ProjectId, cancellationToken);

        if (!isProjectExist)
            return new ProjectManagerResult.Grant.Failure.ProjectNotExist();

        var existingRole = await context
            .ProjectManagers
            .Include(i => i.GrantEvent)
            .Where(i =>
                i.ProjectManagerTypeId == item.ProjectManagerRole.Id &&
                i.GrantEvent.ProjectEventId == item.ProjectId.Value &&
                i.RevokeEvent == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingRole is not null)
            return new ProjectManagerResult.Grant.Failure.RecordExist();

        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = item.Grant,
            PersonId = personId.Value,
            ProjectId = item.ProjectId.Value,
            ProjectEventTypeId = ProjectEvent.GrandRole.Id,
        };

        var projectManager = new DatabaseProjectManager
        {
            PersonId = item.PersonId.Value,
            ProjectManagerTypeId = item.ProjectManagerRole.Id,
            GrantEvent = projectEvent,
        };

        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.ProjectManagers.AddAsync(projectManager, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        item.Id = projectManager.ProjectManagerId;

        return new ProjectManagerResult.Grant.Success();
    }

    public async Task<ExistingResult> RevokeAsync(
        PersonId personId,
        ProjectManager item,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);

        var projectManager = await context
            .ProjectManagers
            .Where(i => i.ProjectManagerId == item.Id.Value)
            .FirstOrDefaultAsync(cancellationToken);

        if (projectManager is null)
            return ExistingResult.NotFound;

        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = DateTimeOffset.Now,
            PersonId = personId.Value,
            ProjectId = item.ProjectId.Value,
            ProjectEventTypeId = ProjectEvent.RevokeRole.Id,
        };

        projectManager.RevokeEvent = projectEvent;
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ExistingResult.Exist;
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

    private static ProjectManager Map(DatabaseProjectManager item) => new ProjectManager.Builder()
        .WithId(item.ProjectManagerId)
        .WithProjectId(item.GrantEvent.ProjectId)
        .WithPersonId(item.GrantEvent.PersonId)
        .WithGrant(item.GrantEvent.CreatedAt)
        .WithRevoke(item.RevokeEvent?.CreatedAt)
        .WithProjectManagerRole(ProjectManagerRole.FromId(item.ProjectManagerTypeId))
        .Build();
}