using Diploma.Application.Persons.Authentication.Projections.RefreshTokens;
using Diploma.Database;
using Diploma.Domain.Base.Results;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.Persons.Authentication.Projections;

public class PersonRefreshTokenProjectionService(
    DiplomaDbContext context
    ) : IPersonRefreshTokenProjectionService
{
    private static readonly OptionalResult<PersonRefreshTokenProjection> NotFound = OptionalResult.NotFound<PersonRefreshTokenProjection>();


    public async Task<OptionalResult<PersonRefreshTokenProjection>> GetAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var databaseItem = await context
            .PersonRefreshTokens
            .AsNoTracking()
            .Include(i => i.LoginInEvent)
            .Include(i => i.LogOutEvent)
            .Where(i => i.RefreshToken == refreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        if (databaseItem is null)
            return NotFound;

        return OptionalResult.Success(new PersonRefreshTokenProjection
        {
            PersonRefreshTokenId = databaseItem.PersonRefreshTokenId,
            RefreshToken = databaseItem.RefreshToken,
            ExpiresAt = databaseItem.ExpiresAt,
            PersonId = databaseItem.LoginInEvent.PersonId,
            CreatedAt = databaseItem.LoginInEvent.CreatedAt,
            LogOutAt = databaseItem.LogOutEvent?.CreatedAt,
        });
    }

    public async Task<OptionalResult<PersonRefreshTokenProjection>> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var databaseItem = await context
            .PersonRefreshTokens
            .AsNoTracking()
            .Include(i => i.LoginInEvent)
            .Include(i => i.LogOutEvent)
            .Where(i => i.PersonRefreshTokenId == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (databaseItem is null)
            return NotFound;

        return OptionalResult.Success(new PersonRefreshTokenProjection
        {
            PersonRefreshTokenId = databaseItem.PersonRefreshTokenId,
            RefreshToken = databaseItem.RefreshToken,
            ExpiresAt = databaseItem.ExpiresAt,
            PersonId = databaseItem.LoginInEvent.PersonId,
            CreatedAt = databaseItem.LoginInEvent.CreatedAt,
            LogOutAt = databaseItem.LogOutEvent?.CreatedAt,
        });
    }
}