using Diploma.Database;
using Diploma.Database.Models.Projects.Recruitments;
using Diploma.Domain.Base.Results;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Microsoft.EntityFrameworkCore;
using DatabaseRecruitment = Diploma.Database.Models.Projects.Recruitments.Recruitment;
using Recruitment = Diploma.Domain.Recruitments.Aggregates.Recruitment;
using SharedRecruitmentStatus = Diploma.Shared.RecruitmentStatuses.RecruitmentStatus;

namespace Diploma.Infrastructure.Recruitments.Repositories;

public class RecruitmentRepository(
    DiplomaDbContext context,
    RecruitmentQueryBuilder builder
    ) : IRecruitmentRepository
{
    public async Task<OptionalResult<Recruitment>> GetAsync(
        RecruitmentId id,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithRecruitmentId(id)
            .Build();

        var databaseResult = await query
            .FirstOrDefaultAsync(cancellationToken);

        if (databaseResult is null)
            return OptionalResult<Recruitment>.NotFound();

        return OptionalResult.Success(Map(databaseResult));
    }


    public async Task<OptionalResult<Recruitment>> GetAsync(
        PersonId personId,
        ProjectId projectId,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithPersonId(personId)
            .WithProjectId(projectId)
            .Build();

        var databaseResult = await query
            .FirstOrDefaultAsync(cancellationToken);

        if (databaseResult is null)
            return OptionalResult<Recruitment>.NotFound();

        return OptionalResult.Success(Map(databaseResult));
    }

    public async Task CreateAsync(
        Recruitment item,
        CancellationToken cancellationToken = default)
    {
        var databaseRecruitment = new DatabaseRecruitment
        {
            ProjectId = item.ProjectId.Value,
            PersonId = item.PersonId.Value,
            CreatedAt = item.CreatedAt,
        };

        var roles = item.ProjectRoleIds.Select(i => new RecruitmentProjectRole
        {
            Recruitment = databaseRecruitment,
            CreatedAt = item.CreatedAt,
            ProjectRoleId = i.Value
        });

        var statusAudit = new RecruitmentStatusAudit
        {
            PersonId = item.PersonId.Value,
            RecruitmentStatusId = item.RecruitmentStatus.Id,
            CreatedAt = item.CreatedAt,
            Recruitment = databaseRecruitment,
        };

        await context.Recruitments.AddAsync(databaseRecruitment, cancellationToken);
        await context.RecruitmentProjectRoles.AddRangeAsync(roles, cancellationToken);
        await context.RecruitmentStatusAudits.AddAsync(statusAudit, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        databaseRecruitment.LastRecruitmentStatusAudit = statusAudit;
        await context.SaveChangesAsync(cancellationToken);

        item.Id = databaseRecruitment.RecruitmentId;
    }

    public async Task<ExistingResult> UpdateAsync(
        PersonId personId,
        Recruitment item,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);

        var databaseItem = await context
            .Recruitments
            .Include(i => i.LastRecruitmentStatusAudit)
            .FirstOrDefaultAsync(i => i.RecruitmentId == item.Id.Value, cancellationToken);

        if (databaseItem is null)
            return ExistingResult.NotFound;

        var statusAudit = new RecruitmentStatusAudit
        {
            PersonId = personId.Value,
            RecruitmentStatusId = item.RecruitmentStatus.Id,
            CreatedAt = DateTimeOffset.Now,
            Recruitment = databaseItem,
        };

        databaseItem.LastRecruitmentStatusAudit?.Next = statusAudit;
        databaseItem.LastRecruitmentStatusAudit = statusAudit;

        await context.RecruitmentStatusAudits.AddAsync(statusAudit, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ExistingResult.Exist;
    }

    private static Recruitment Map(DatabaseRecruitment item) => new Recruitment.Builder()
        .WithId(item.RecruitmentId)
        .WithProjectId(item.ProjectId)
        .WithPersonId(item.PersonId)
        .WithCreatedAt(item.CreatedAt)
        .WithRecruitmentStatus(SharedRecruitmentStatus.FromId(item.LastRecruitmentStatusAudit!.RecruitmentStatusId))
        .WithProjectRoleIds(item.RecruitmentProjectRoles.Select(i => new ProjectRoleId { Value = i.ProjectRoleId }))
        .Build();
}