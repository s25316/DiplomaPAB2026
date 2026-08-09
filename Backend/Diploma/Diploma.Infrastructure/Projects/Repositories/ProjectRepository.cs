using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Shared.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using DatabaseProject = Diploma.Database.Models.Projects.Project;
using DatabaseProjectEvent = Diploma.Database.Models.Projects.ProjectEvents.ProjectEvent;
using Project = Diploma.Domain.Projects.Aggregates.Project;

namespace Diploma.Infrastructure.Projects.Repositories;

public class ProjectRepository(
    DiplomaDbContext context,
    ProjectQueryBuilder builder
    ) : IProjectRepository
{
    public async Task<OptionalResult<Project>> GetAsync(
        ProjectId projectId,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithProjectId(projectId)
            .Build();

        var item = await query.FirstOrDefaultAsync(cancellationToken);

        if (item is null)
            return OptionalResult<Project>.NotFound();

        var domain = new Project.Builder()
            .WithId(item.Root?.ProjectId ?? item.ProjectId)
            .WithLastSnapshotId(item.ProjectId)
            .WithTitle(item.Title)
            .WithDescription(item.Description)
            .WithIsVisible(item.IsVisible)
            .Build();

        return OptionalResult.Success(domain);
    }

    public async Task CreateAsync(
        PersonId personId,
        Project item,
        CancellationToken cancellationToken = default)
    {
        var project = new DatabaseProject
        {
            Title = item.Title,
            Description = item.Description,
            IsVisible = item.IsVisible,
        };

        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = DateTimeOffset.Now,
            PersonId = personId.Value,
            Project = project,
            ProjectEventTypeId = ProjectEvent.ProjectCreated.Id,
        };

        await context.Projects.AddAsync(project, cancellationToken);
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        item.Id = project.ProjectId;
    }

    public async Task<ExistingResult> UpdateAsync(
        PersonId personId,
        Project item,
        CancellationToken cancellationToken = default)
    {
        return await UpdateAsync(personId, item, ProjectEvent.ProjectUpdated, cancellationToken);
    }

    public async Task<ExistingResult> DeleteAsync(
        PersonId personId,
        Project item,
        CancellationToken cancellationToken = default)
    {
        item.ChangeVisibility(false);
        return await UpdateAsync(personId, item, ProjectEvent.ProjectRemoved, cancellationToken);
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

    private async Task<ExistingResult> UpdateAsync(
        PersonId personId,
        Project item,
        ProjectEvent @event,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);

        var isProjectExist = await IsProjectExistAsync(item.Id, cancellationToken);

        if (!isProjectExist)
            return ExistingResult.NotFound;

        var last = await context
            .Projects
            .Where(i =>
                i.NextId == null &&
                i.ProjectId == item.LastSnapshotId.Value ||
                (i.Root != null && i.Root.ProjectId == item.LastSnapshotId.Value))
            .FirstOrDefaultAsync(cancellationToken);

        if (last == null)
            return ExistingResult.NotFound;

        var next = new DatabaseProject
        {
            Title = item.Title,
            Description = item.Description,
            IsVisible = item.IsVisible,
        };

        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = DateTimeOffset.Now,
            PersonId = personId.Value,
            ProjectId = item.Id.Value,
            ProjectEventTypeId = @event.Id,
        };

        last.Next = next;

        await context.Projects.AddAsync(next, cancellationToken);
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ExistingResult.Exist;
    }
}