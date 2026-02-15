using DefaultValueAttribute = System.ComponentModel.DefaultValueAttribute;

namespace Base.Models.Interfaces.Repositories;

public abstract class BaseParameters<TItemId>
{
    public IList<TItemId> Ids { get; init; } = [];

    public Pagination Pagination { get; init; } = new();

    [DefaultValue(Order.Ascending)]
    public virtual Order Order { get; init; } = Order.Ascending;
}