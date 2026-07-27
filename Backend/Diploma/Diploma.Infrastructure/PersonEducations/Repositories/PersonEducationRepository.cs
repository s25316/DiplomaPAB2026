using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.PersonEducations.Aggregates;
using Diploma.Domain.PersonEducations.ValueObjects;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Shared.Semesters;
using Microsoft.EntityFrameworkCore;
using DatabasePersonEducation = Diploma.Database.Models.Persons.PersonEvents.Audits.PersonEducation;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.PersonEducations.Repositories;

public class PersonEducationRepository(
    DiplomaDbContext context
    ) : IPersonEducationRepository
{
    public async Task CreateAsync(PersonEducation item, CancellationToken cancellationToken = default)
    {
        var @event = new DatabasePersonEvent
        {
            PersonId = item.PersonId,
            CreatedAt = DateTimeOffset.Now,
            PersonEventTypeId = SharedPersonEvent.CreateEmployment.Id,
        };

        var employment = new DatabasePersonEducation
        {
            PersonEvent = @event,
            EducationCourseId = item.EducationCourseId.Value,
            EducationCourseInstanceId = item.EducationCourseInstanceId?.Value,
            YearStart = item.Start.Year,
            SemesterStartId = item.Start.Semester.Id,
            YearEnd = item.End?.Year,
            SemesterEndId = item.End?.Semester.Id,
        };

        await context.PersonEvents.AddAsync(@event, cancellationToken);
        await context.PersonEducations.AddAsync(employment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        item.Id = employment.PersonEducationId;
    }

    public async Task<OptionalResult<PersonEducation>> GetAsync(PersonEducationId id, CancellationToken cancellationToken = default)
    {
        var query = PrepareQuery();
        var databseItem = await query
            .AsNoTracking()
            .FirstOrDefaultAsync(i =>
                i.PersonEducationId == id.Value || (i.Root != null && i.Root.PersonEducationId == id.Value),
                cancellationToken
            );

        if (databseItem is null)
            return OptionalResult<PersonEducation>.NotFound();

        var builder = new PersonEducation.Builder()
            .WithId(databseItem.Root?.PersonEducationId ?? databseItem.PersonEducationId)
            .WithLastSnapshotId(databseItem.PersonEducationId)
            .WithPersonId(databseItem.PersonEvent.PersonId)
            .WithEducationCourseId(databseItem.EducationCourseId)
            .WithEducationCourseInstanceId(databseItem.EducationCourseInstanceId)
            .WithStart(new EducationSemestr(
                databseItem.YearStart,
                Semester.FromId(databseItem.SemesterStartId)
            ));

        if (databseItem.YearEnd is not null && databseItem.SemesterEndId is not null)
        {
            builder.WithEnd(new EducationSemestr(
                databseItem.YearEnd.Value,
                Semester.FromId(databseItem.SemesterEndId.Value)
            ));
        }

        return OptionalResult.Success(builder.Build());
    }

    public async Task<int> TotalCountAsync(PersonId id, CancellationToken cancellationToken = default)
    {
        var query = PrepareQuery();
        return await query
            .AsNoTracking()
            .CountAsync(
                i => i.PersonEvent.PersonId == id.Value,
                cancellationToken
            );
    }

    public async Task<ExistingResult> UpdateAsync(
        PersonEducation item,
        CancellationToken cancellationToken = default
    ) => await UpdateAsync(
        SharedPersonEvent.UpdateEducation,
        item,
        cancellationToken
    );

    public async Task<ExistingResult> DeleteAsync(
        PersonEducation item,
        CancellationToken cancellationToken = default
    ) => await UpdateAsync(
        SharedPersonEvent.DeleteEducation,
        item,
        cancellationToken
    );

    private IQueryable<DatabasePersonEducation> PrepareQuery()
    {
        var deletingId = SharedPersonEvent.DeleteEducation.Id;
        return context
            .PersonEducations
            .Include(i => i.Root)
            .Include(i => i.PersonEvent)
            .Where(i =>
                i.PersonEvent.PersonEventTypeId != deletingId &&
                i.NextId == null
            );
    }

    private async Task<ExistingResult> UpdateAsync(
        SharedPersonEvent personEvent,
        PersonEducation item,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);
        var query = PrepareQuery();
        var databseItem = await query
            .FirstOrDefaultAsync(i =>
                i.PersonEducationId == item.LastSnapshotId.Value &&
                i.PersonEvent.PersonId == item.PersonId.Value,
                cancellationToken
            );

        if (databseItem is null)
            return ExistingResult.NotFound;


        var @event = new DatabasePersonEvent
        {
            PersonId = item.PersonId,
            CreatedAt = DateTimeOffset.Now,
            PersonEventTypeId = personEvent.Id,
        };

        var employment = new DatabasePersonEducation
        {
            PersonEvent = @event,
            RootId = item.Id.Value,
            EducationCourseId = item.EducationCourseId.Value,
            EducationCourseInstanceId = item.EducationCourseInstanceId?.Value,
            YearStart = item.Start.Year,
            SemesterStartId = item.Start.Semester.Id,
            YearEnd = item.End?.Year,
            SemesterEndId = item.End?.Semester.Id,
        };

        databseItem.Next = employment;

        await context.PersonEvents.AddAsync(@event, cancellationToken);
        await context.PersonEducations.AddAsync(employment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ExistingResult.Exist;
    }
}