using Base.Exceptions;
using Diploma.Application.Persons.Commands.Interfaces;
using Diploma.Database;
using Diploma.Domain.Base.Results;
using Diploma.Infrastructure.QueryBuilders.Persons;
using Diploma.Models.Shared;
using Diploma.Shared.Verifications;
using Microsoft.EntityFrameworkCore;
using DatabasePersonOperation = Diploma.Database.Models.Persons.PersonOperations.PersonOperation;
using SharedPersonOperation = Diploma.Shared.PersonOperations.PersonOperation;

namespace Diploma.Infrastructure.Persons;

public class PersonOperationRepository(
    DiplomaDbContext context,
    PersonOperationQueryBuilder queryBuilder
    ) : IPersonOperationRepository
{
    private const string NOT_FOUND = "Opracja za podanym id nie jest wyszukana: {0}.";
    private const string ACTIVATED = "Opracja za podanym id jest aktywowana: {0}.";

    private static readonly OptionalResult<PersonOperationItem> NotFound = OptionalResult<PersonOperationItem>.NotFound();

    public async Task<Response<PersonOperationItem>> GetAsync(
        PersonOperationInput.Filters filters,
        CancellationToken cancellationToken = default)
    {
        var baseQueryBuilder = queryBuilder
            .WithPersonId(filters.PersonId)
            .WithPersonOperations(filters.PersonOperations);

        var baseQuery = baseQueryBuilder.Build();
        var query = baseQueryBuilder
            .WithOrderBy(filters.Order, filters.Pagination)
            .Build();

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        var dbItems = await query.ToListAsync(cancellationToken);

        return new Response<PersonOperationItem>
        {
            Items = dbItems.Select(i => new PersonOperationItem
            {
                PersonOperationId = new PersonOperationId(i.PersonOperationId),
                PersonId = i.PersonId,
                Value = i.Value,
                CreatedAt = i.CreatedAt,
                ExpiresAt = i.ExpiresAt,
                ActivatedAt = i.ActivatedAt,
                Verification = Verification.FromId(i.VerificationMethodId),
                PersonOperation = SharedPersonOperation.FromId(i.PersonOperationTypeId),
            }).ToList(),
            Pagination = new ResponsePagination
            {
                ItemsPerPage = filters.Pagination.ItemsPerPage,
                Page = filters.Pagination.Page,
                TotalCount = totalCount,
            },
        };
    }

    public async Task<OptionalResult<PersonOperationItem>> GetAsync(
        Guid personOperationId,
        CancellationToken cancellationToken = default)
    {
        var databaseItem = await context
            .PersonOperations
            .AsNoTracking()
            .FirstOrDefaultAsync(i =>
                i.PersonOperationId == personOperationId,
                cancellationToken);

        if (databaseItem is null)
            return NotFound;


        return OptionalResult.Success(new PersonOperationItem
        {
            PersonOperationId = new PersonOperationId(databaseItem.PersonOperationId),
            PersonId = databaseItem.PersonId,
            Value = databaseItem.Value,
            CreatedAt = databaseItem.CreatedAt,
            ExpiresAt = databaseItem.ExpiresAt,
            ActivatedAt = databaseItem.ActivatedAt,
            Verification = Verification.FromId(databaseItem.VerificationMethodId),
            PersonOperation = SharedPersonOperation.FromId(databaseItem.PersonOperationTypeId),
        });
    }

    public async Task<PersonOperationId> CreateAsync(
        PersonOperationInput.Creating input,
        CancellationToken cancellationToken = default)
    {
        var databaseOperation = new DatabasePersonOperation
        {
            PersonId = input.PersonId,
            Value = input.Value,
            CreatedAt = input.CreatedAt,
            ExpiresAt = input.ExpiresAt,
            VerificationMethodId = input.Verification.Id,
            PersonOperationTypeId = input.PersonOperation.Id,
        };

        await context.PersonOperations.AddAsync(databaseOperation, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new PersonOperationId(databaseOperation.PersonOperationId);
    }

    public async Task ActivateAsync(
        PersonOperationId operationId,
        CancellationToken cancellationToken = default)
    {
        var personOperationId = operationId.Value;
        var databaseItem = await context
            .PersonOperations
            .FirstOrDefaultAsync(i =>
                i.PersonOperationId == personOperationId,
                cancellationToken)
            ?? throw new ResourceException.NotFound(string.Format(NOT_FOUND, personOperationId));

        if (databaseItem.ActivatedAt.HasValue)
            throw new ResourceException.InvalidOperation(string.Format(ACTIVATED, personOperationId));

        databaseItem.ActivatedAt = DateTimeOffset.Now;
        await context.SaveChangesAsync(cancellationToken);
    }
}