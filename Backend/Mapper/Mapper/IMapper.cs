namespace Mapper;

public interface IMapper
{
    TOut Map<TOut>(object item);
}