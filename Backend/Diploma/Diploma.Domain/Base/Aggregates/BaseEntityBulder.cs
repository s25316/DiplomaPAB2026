namespace Diploma.Domain.Base.Aggregates;

public class BaseEntityBulder<TEntity, TEntityId>(TEntity? entity = null)
    where TEntityId : BaseEntityId
    where TEntity : BaseEntity<TEntityId>, new()
{
    private readonly TEntity entity = entity ?? new();


    public void With(Action<TEntity> action)
    {
        if (!entity.HasEnabledEvents)
            entity.HasEnabledEvents = false;

        action(entity);
    }

    public TEntity Build()
    {
        entity.HasEnabledEvents = true;
        return entity;
    }
}