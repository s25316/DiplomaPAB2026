using Base.Models.ValueObjects.Regony;

namespace GUS.REGON.Infrastructure.Interfaces;

public interface IQueryRepository
{
    Task CreateAsync(Regon regon, CancellationToken cancellationToken = default);
    Task UpdateAsync(Regon regon, CancellationToken cancellationToken = default);
}