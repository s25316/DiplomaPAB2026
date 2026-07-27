using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.PersonEmployments.Aggregates;
using Microsoft.EntityFrameworkCore;
using DatabasePersonEmployment = Diploma.Database.Models.Persons.PersonEvents.Audits.PersonEmployment;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.PersonEmployments.Repositories;

public class PersonEmploymentRepository(
    DiplomaDbContext context
    ) : IPersonEmploymentRepository
{
    public async Task CreateAsync(
        PersonEmployment item,
        CancellationToken cancellationToken = default)
    {
        var @event = new DatabasePersonEvent
        {
            PersonId = item.PersonId,
            CreatedAt = DateTimeOffset.Now,
            PersonEventTypeId = SharedPersonEvent.CreateEmployment.Id,
        };

        var employment = new DatabasePersonEmployment
        {
            PersonEvent = @event,
            Regon = item.Regon.Value,
            Position = item.Position,
            Description = item.Description,
            From = item.From,
            To = item.To,
        };

        await context.PersonEvents.AddAsync(@event, cancellationToken);
        await context.PersonEmployments.AddAsync(employment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        item.Id = employment.PersonEmploymentId;
    }

    public async Task<OptionalResult<PersonEmployment>> GetAsync(
        PersonEmploymentId id,
        CancellationToken cancellationToken = default)
    {
        var query = PrepareQuery();
        var databseItem = await query
            .AsNoTracking()
            .FirstOrDefaultAsync(i =>
                i.PersonEmploymentId == id.Value || (i.Root != null && i.Root.PersonEmploymentId == id.Value),
                cancellationToken
            );

        if (databseItem is null)
            return OptionalResult<PersonEmployment>.NotFound();

        var builder = new PersonEmployment.Builder()
            .WithId(databseItem.Root?.PersonEmploymentId ?? databseItem.PersonEmploymentId)
            .WithLastSnapshotId(databseItem.PersonEmploymentId)
            .WithPersonId(databseItem.PersonEvent.PersonId)
            .WithRegon(databseItem.Regon)
            .WithPosition(databseItem.Position)
            .WithDescription(databseItem.Description)
            .WithFrom(databseItem.From)
            .WithTo(databseItem.To);

        return OptionalResult.Success(builder.Build());
    }


    public async Task<ExistingResult> UpdateAsync(
        PersonEmployment item,
        CancellationToken cancellationToken = default
    ) => await UpdateAsync(
        SharedPersonEvent.UpdateEmployment,
        item,
        cancellationToken
    );


    public async Task<ExistingResult> DeleteAsync(
        PersonEmployment item,
        CancellationToken cancellationToken = default
    ) => await UpdateAsync(
        SharedPersonEvent.DeleteEmployment,
        item,
        cancellationToken
    );

    private IQueryable<DatabasePersonEmployment> PrepareQuery()
    {
        var deletingId = SharedPersonEvent.DeleteEmployment.Id;
        return context
            .PersonEmployments
            .Include(i => i.Root)
            .Include(i => i.PersonEvent)
            .Where(i =>
                i.PersonEvent.PersonEventTypeId != deletingId &&
                i.NextId == null
            );
    }

    private async Task<ExistingResult> UpdateAsync(
        SharedPersonEvent personEvent,
        PersonEmployment item,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item.Id);
        var query = PrepareQuery();
        var databseItem = await query
            .FirstOrDefaultAsync(i =>
                i.PersonEmploymentId == item.LastSnapshotId.Value &&
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

        var employment = new DatabasePersonEmployment
        {
            PersonEvent = @event,
            RootId = item.Id.Value,
            Regon = item.Regon.Value,
            Position = item.Position,
            Description = item.Description,
            From = item.From,
            To = item.To,
        };

        databseItem.Next = employment;

        await context.PersonEvents.AddAsync(@event, cancellationToken);
        await context.PersonEmployments.AddAsync(employment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ExistingResult.Exist;
    }
}