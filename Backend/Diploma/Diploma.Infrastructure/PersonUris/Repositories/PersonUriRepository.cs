using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Domain.PersonUris.Aggregates;
using Microsoft.EntityFrameworkCore;
using DatabasePersonEvent = Diploma.Database.Models.Persons.PersonEvents.PersonEvent;
using DatabasePersonUri = Diploma.Database.Models.Persons.PersonEvents.Audits.PersonUri;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.PersonUris.Repositories;

public class PersonUriRepository(
    DiplomaDbContext context
    ) : IPersonUriRepository
{
    public async Task CreateAsync(PersonUri item, CancellationToken cancellationToken = default)
    {
        var @event = new DatabasePersonEvent
        {
            PersonId = item.PersonId,
            CreatedAt = DateTimeOffset.Now,
            PersonEventTypeId = SharedPersonEvent.CreateUri.Id,
        };

        var employment = new DatabasePersonUri
        {
            PersonEvent = @event,
            Uri = item.Uri.ToString(),
            Name = item.Name,
            Description = item.Description,
        };

        await context.PersonEvents.AddAsync(@event, cancellationToken);
        await context.PersonUris.AddAsync(employment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        item.Id = employment.PersonUriId;
    }

    public async Task<OptionalResult<PersonUri>> GetAsync(PersonUriId id, CancellationToken cancellationToken = default)
    {
        var query = PrepareQuery();
        var databseItem = await query
            .AsNoTracking()
            .FirstOrDefaultAsync(i =>
                i.PersonUriId == id.Value || (i.Root != null && i.Root.PersonUriId == id.Value),
                cancellationToken
            );

        if (databseItem is null)
            return OptionalResult<PersonUri>.NotFound();

        var builder = new PersonUri.Builder()
            .WithId(databseItem.Root?.PersonUriId ?? databseItem.PersonUriId)
            .WithLastSnapshotId(databseItem.PersonUriId)
            .WithPersonId(databseItem.PersonEvent.PersonId)
            .WithUri(new Uri(databseItem.Uri))
            .WithName(databseItem.Name)
            .WithDescription(databseItem.Description);

        return OptionalResult.Success(builder.Build());
    }

    public async Task<ExistingResult> DeleteAsync(
        PersonUri item,
        CancellationToken cancellationToken = default
    ) => await UpdateAsync(
        SharedPersonEvent.DeleteUri,
        item,
        cancellationToken
    );

    public async Task<ExistingResult> UpdateAsync(
        PersonUri item,
        CancellationToken cancellationToken = default
    ) => await UpdateAsync(
        SharedPersonEvent.UpdateUri,
        item,
        cancellationToken
    );

    private IQueryable<DatabasePersonUri> PrepareQuery()
    {
        var deletingId = SharedPersonEvent.DeleteUri.Id;
        return context
            .PersonUris
            .Include(i => i.Root)
            .Include(i => i.PersonEvent)
            .Where(i =>
                i.PersonEvent.PersonEventTypeId != deletingId &&
                i.NextId == null
            );
    }

    private async Task<ExistingResult> UpdateAsync(
        SharedPersonEvent personEvent,
        PersonUri item,
        CancellationToken cancellationToken = default)
    {

        ArgumentNullException.ThrowIfNull(item.Id);
        var query = PrepareQuery();
        var databseItem = await query
            .FirstOrDefaultAsync(i =>
                i.PersonUriId == item.LastSnapshotId.Value &&
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

        var employment = new DatabasePersonUri
        {
            PersonEvent = @event,
            RootId = item.Id.Value,
            Uri = item.Uri.ToString(),
            Name = item.Name,
            Description = item.Description,
        };

        databseItem.Next = employment;

        await context.PersonEvents.AddAsync(@event, cancellationToken);
        await context.PersonUris.AddAsync(employment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ExistingResult.Exist;
    }
}