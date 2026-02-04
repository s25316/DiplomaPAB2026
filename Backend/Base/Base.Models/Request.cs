using System.ComponentModel;

namespace Base.Models;

public abstract class Request<TItemId>
{
    public IEnumerable<TItemId> Ids { get; init; } = [];

    public Pagination Pagination { get; init; } = new();

    [DefaultValue(Order.Ascending)]
    public virtual Order Order { get; init; } = Order.Ascending;
}