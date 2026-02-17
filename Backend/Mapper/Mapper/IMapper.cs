namespace Mapper;

public interface IMapper
{
    TOut Map<TOut>(object? item);

    IEnumerable<TOut> Map<TOut>(IEnumerable<object> items);
    IReadOnlyCollection<TOut> Map<TOut>(IReadOnlyCollection<object> items);

    List<TOut> Map<TOut>(List<object> items);
    IList<TOut> Map<TOut>(IList<object> items);
    IReadOnlyList<TOut> Map<TOut>(IReadOnlyList<object> items);

    public HashSet<TOut> Map<TOut>(HashSet<object> items);
    public ISet<TOut> Map<TOut>(ISet<object> items);
    public IReadOnlySet<TOut> Map<TOut>(IReadOnlySet<object> items);
}