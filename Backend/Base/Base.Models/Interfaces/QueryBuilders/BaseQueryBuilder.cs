namespace Base.Models.Interfaces.QueryBuilders;

public abstract class BaseQueryBuilder<TItem>
{
    protected IQueryable<TItem> query;


    protected BaseQueryBuilder(IQueryable<TItem> query)
    {
        this.query = query;
    }


    protected virtual void With(Func<IQueryable<TItem>, IQueryable<TItem>> func) => query = func(query);
    public virtual IQueryable<TItem> Build() => query;
}