using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoleDisciplines.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Shared.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using DatabaseProjectEvent = Diploma.Database.Models.Projects.ProjectEvents.ProjectEvent;
using DatabaseProjectRoleDiscipline = Diploma.Database.Models.Projects.ProjectRoles.ProjectRoleEducationDiscipline;

namespace Diploma.Infrastructure.ProjectRoleDisciplines.Repositories;

public class ProjectRoleDisciplineRepository(
    DiplomaDbContext context,
    ProjectRoleEducationDisciplineQueryBuilder builder
    ) : IProjectRoleDisciplineRepository
{
    public async Task<OptionalResult<ProjectRoleDiscipline>> GetAsync(
        ProjectRoleDisciplineId id,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithProjectRoleDisciplineId(id)
            .Build();

        var databaseItem = await query
            .FirstOrDefaultAsync(cancellationToken);

        if (databaseItem is null)
            return OptionalResult<ProjectRoleDiscipline>.NotFound();

        return OptionalResult.Success(Map(databaseItem));
    }

    public async Task<IEnumerable<ProjectRoleDiscipline>> GetAsync(ProjectRoleId id, CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithProjectRoleId(id)
            .Build();

        var databaseItems = await query.ToListAsync(cancellationToken);
        return databaseItems.Select(Map);
    }

    public async Task<int> TotalCountAsync(
        ProjectRoleId id,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithProjectRoleId(id)
            .Build();

        return await query.CountAsync(cancellationToken);
    }

    public async Task CreateAsync(
        PersonId personId,
        ProjectRoleDiscipline item,
        CancellationToken cancellationToken = default)
    {
        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = DateTimeOffset.Now,
            PersonId = personId.Value,
            ProjectId = item.ProjectId.Value,
            ProjectEventTypeId = ProjectEvent.ProjectUpdated.Id,
        };

        var databaseItem = new DatabaseProjectRoleDiscipline
        {
            ProjectRoleId = item.ProjectRoleId.Value,
            EducationDisciplineCode = item.DisciplineCode,
            AddProjectEvent = projectEvent
        };

        await context.ProjectRoleEducationDisciplines.AddAsync(databaseItem, cancellationToken);
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        item.Id = databaseItem.ProjectRoleEducationDisciplineId;
    }

    public async Task<ExistingResult> DeleteAsync(
        PersonId personId,
        ProjectRoleDiscipline item,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);

        var databaseItem = await context
            .ProjectRoleEducationDisciplines
            .FirstOrDefaultAsync(i => i.ProjectRoleEducationDisciplineId == item.Id.Value);

        if (databaseItem is null)
            return ExistingResult.NotFound;

        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = DateTimeOffset.Now,
            PersonId = personId.Value,
            ProjectId = item.ProjectId.Value,
            ProjectEventTypeId = ProjectEvent.ProjectUpdated.Id,
        };

        databaseItem.RemoveProjectEvent = projectEvent;

        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ExistingResult.Exist;
    }

    private static ProjectRoleDiscipline Map(DatabaseProjectRoleDiscipline item) => new ProjectRoleDiscipline.Builder()
        .WithId(item.ProjectRoleEducationDisciplineId)
        .WithProjectId(item.ProjectRole.ProjectId)
        .WithProjectRoleId(item.ProjectRoleId)
        .WithDisciplineCode(item.EducationDisciplineCode)
        .WithCreatedAt(item.AddProjectEvent.CreatedAt)
        .Build();
}