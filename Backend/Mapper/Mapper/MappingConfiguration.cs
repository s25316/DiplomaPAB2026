namespace Mapper;

public abstract class MappingConfiguration
{
    private readonly Dictionary<(Type In, Type Out), Delegate> dictionary = [];


    protected void AddConfiguration<TIn, TOut>(Func<TIn, TOut> map)
    {
        Func<IMapper, TIn, TOut> mapFunc = (mapper, input) => map(input);
        AddConfiguration(mapFunc);
    }

    protected void AddConfiguration<TIn, TOut>(Func<IMapper, TIn, TOut> map)
    {
        if (dictionary.ContainsKey((typeof(TIn), typeof(TOut))))
        {
            throw new ArgumentException($"Exist mapping for Types: From ({typeof(TIn).FullName}), To ({typeof(TOut).FullName})");
        }

        dictionary[(typeof(TIn), typeof(TOut))] = map;
    }
}
