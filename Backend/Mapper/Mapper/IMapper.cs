namespace Mapper;

public interface IMapper
{
    TOut Map<TOut>(object? item);
    IEnumerable<TOut> MapEnumerable<TOut>(IEnumerable<object> items);
}