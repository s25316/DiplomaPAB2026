namespace Mapper;

public interface IMapper
{
    TOut Map<TIn, TOut>(TIn item);
    IEnumerable<TOut> Map<TIn, TOut>(IEnumerable<TIn> items);
}