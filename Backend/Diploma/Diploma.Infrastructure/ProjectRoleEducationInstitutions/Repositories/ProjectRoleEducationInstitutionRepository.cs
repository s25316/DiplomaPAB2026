using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoleEducationInstitutions.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Diploma.Shared.ProjectEvents;
using Microsoft.EntityFrameworkCore;
using DatabaseItem = Diploma.Database.Models.Projects.ProjectRoles.ProjectRoleEducationInstitution;
using DatabaseProjectEvent = Diploma.Database.Models.Projects.ProjectEvents.ProjectEvent;

namespace Diploma.Infrastructure.ProjectRoleEducationInstitutions.Repositories;

public class ProjectRoleEducationInstitutionRepository(
    DiplomaDbContext context,
    ProjectRoleEducationInstitutionQueryBuilder builder
    ) : IProjectRoleEducationInstitutionRepository
{
    public async Task<OptionalResult<ProjectRoleEducationInstitution>> GetAsync(
        ProjectRoleEducationInstitutionId id,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithProjectRoleDisciplineId(id)
            .Build();

        var databaseItem = await query
            .FirstOrDefaultAsync(cancellationToken);

        if (databaseItem is null)
            return OptionalResult<ProjectRoleEducationInstitution>.NotFound();

        return OptionalResult.Success(Map(databaseItem));
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

    public async Task CreateAsync(PersonId personId,
        ProjectRoleEducationInstitution item,
        CancellationToken cancellationToken = default)
    {
        var projectEvent = new DatabaseProjectEvent
        {
            CreatedAt = DateTimeOffset.Now,
            PersonId = personId.Value,
            ProjectId = item.ProjectId.Value,
            ProjectEventTypeId = ProjectEvent.ProjectUpdated.Id,
        };

        var databaseItem = new DatabaseItem
        {
            ProjectRoleId = item.ProjectRoleId.Value,
            EducationInstitutionId = item.EducationInstitutionId.Value,
            AddProjectEvent = projectEvent
        };

        await context.ProjectRoleEducationInstitutions.AddAsync(databaseItem, cancellationToken);
        await context.ProjectEvents.AddAsync(projectEvent, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        item.Id = databaseItem.ProjectRoleEducationInstitutionId;
    }

    public async Task<ExistingResult> DeleteAsync(
        PersonId personId,
        ProjectRoleEducationInstitution item,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);

        var databaseItem = await context
            .ProjectRoleEducationInstitutions
            .FirstOrDefaultAsync(i => i.ProjectRoleEducationInstitutionId == item.Id.Value);

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

    private static ProjectRoleEducationInstitution Map(DatabaseItem item) => new ProjectRoleEducationInstitution.Builder()
        .WithId(item.ProjectRoleEducationInstitutionId)
        .WithProjectRoleId(item.ProjectRoleId)
        .WithProjectId(item.ProjectRole.ProjectId)
        .WithCreatedAt(item.AddProjectEvent.CreatedAt)
        .Build();
}