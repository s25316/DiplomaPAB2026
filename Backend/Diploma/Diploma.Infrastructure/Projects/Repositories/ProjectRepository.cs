using Diploma.Database;
using Diploma.Database.Models.Projects;
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
            .WithId(item.ProjectId)
            .WithTitle(item.LastProjectData?.Title ?? string.Empty)
            .WithDescription(item.LastProjectData?.Description ?? string.Empty)
            .WithIsVisible(item.LastProjectData?.IsVisible ?? false)
            .WithCreatedAt(item.CreatedAt)
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
            CreatedAt = item.CreatedAt,
        };

        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = item.CreatedAt,
            PersonId = personId.Value,
            Project = project,
            ProjectEventTypeId = ProjectEvent.ProjectCreated.Id,
        };

        await context.Projects.AddAsync(project, cancellationToken);
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var data = new ProjectData
        {
            Title = item.Title,
            Description = item.Description,
            IsVisible = item.IsVisible,
            ProjectEvent = projectEvent,
        };

        project.LastProjectData = data;

        await context.ProjectDatas.AddAsync(data, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        item.Id = project.ProjectId;
    }

    public async Task<ExistingResult> UpdateAsync(
        PersonId personId,
        Project item,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);

        var project = await context
            .Projects
            .Include(i => i.LastProjectData)
            .Where(i => i.ProjectId == item.Id.Value && i.RemovedAt == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (project is null)
            return ExistingResult.NotFound;

        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = DateTimeOffset.Now,
            PersonId = personId.Value,
            ProjectId = item.Id.Value,
            ProjectEventTypeId = ProjectEvent.ProjectUpdated.Id,
        };

        var data = new ProjectData
        {
            Title = item.Title,
            Description = item.Description,
            IsVisible = item.IsVisible,
            ProjectEvent = projectEvent,
        };

        project.LastProjectData?.Next = data;
        project.LastProjectData = data;


        await context.ProjectDatas.AddAsync(data, cancellationToken);
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ExistingResult.Exist;
    }

    public async Task<ExistingResult> DeleteAsync(
        PersonId personId,
        Project item,
        CancellationToken cancellationToken = default)
    {
        item.ChangeVisibility(false);

        ArgumentNullException.ThrowIfNull(item.Id);

        var project = await context
            .Projects
            .Include(i => i.LastProjectData)
            .Where(i => i.ProjectId == item.Id.Value && i.RemovedAt == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (project is null)
            return ExistingResult.NotFound;

        project.RemovedAt = DateTimeOffset.Now;

        await context.SaveChangesAsync(cancellationToken);

        return ExistingResult.Exist;
    }
}