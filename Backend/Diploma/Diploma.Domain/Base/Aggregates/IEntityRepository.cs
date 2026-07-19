namespace Diploma.Domain.Base.Aggregates;

public interface IEntityRepository<TEntityId, TEntity>
    where TEntityId : BaseEntityId
    where TEntity : BaseEntity<TEntityId>
{
    Task<TEntity> GetAsync(TEntityId id, CancellationToken cancellationToken = default);
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}