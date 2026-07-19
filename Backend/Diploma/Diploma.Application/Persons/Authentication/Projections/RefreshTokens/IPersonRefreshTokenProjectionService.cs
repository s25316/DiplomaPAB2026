using Diploma.Domain.Base.Results;

namespace Diploma.Application.Persons.Authentication.Projections.RefreshTokens;

public interface IPersonRefreshTokenProjectionService
{
    Task<OptionalResult<PersonRefreshTokenProjection>> GetAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<OptionalResult<PersonRefreshTokenProjection>> GetAsync(Guid id, CancellationToken cancellationToken = default);
}