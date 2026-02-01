using System.Reflection;

namespace Mapper;

public sealed class Mapper : IMapper
{
    private const string KEY_FIELD = "dictionary";
    private static readonly FieldInfo fieldInfo = GetFieldInfo(KEY_FIELD);

    private readonly Dictionary<(Type In, Type Out), Delegate> dictionary = [];

    public Mapper(IEnumerable<MappingConfiguration> configurations)
    {
        foreach (var configuration in configurations)
        {
            AppendToDictionary(configuration);
        }
    }

    public TOut Map<TIn, TOut>(TIn item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        var @delegate = GetMappingFunc<TIn, TOut>();
        return @delegate(this, item);
    }

    public IEnumerable<TOut> Map<TIn, TOut>(IEnumerable<TIn> items)
    {
        if (items == null || !items.Any()) return [];
        return items.Select(Map<TIn, TOut>);
    }



    private static FieldInfo GetFieldInfo(string fieldName)
    {
        return typeof(MappingConfiguration).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new ArgumentException($"Not found: {KEY_FIELD}");
    }

    private void AppendToDictionary(MappingConfiguration configuration)
    {
        var instanceField = fieldInfo.GetValue(configuration);
        if (instanceField is not Dictionary<(Type In, Type Out), Delegate> implementationDictionary)
        {
            throw new ArgumentException($"{KEY_FIELD} has not correct type, {instanceField?.GetType().FullName}");
        }

        foreach (var (key, @delegate) in implementationDictionary)
        {
            if (dictionary.ContainsKey(key))
            {
                throw new ArgumentException($"Exist mapping for Types: From ({key.In.FullName}), To ({key.Out.FullName}); Duplication in class: {configuration.GetType().FullName}");
            }

            dictionary[key] = @delegate;
        }
    }

    private Func<IMapper, TIn, TOut> GetMappingFunc<TIn, TOut>()
    {
        if (!dictionary.TryGetValue(((typeof(TIn), typeof(TOut))), out var @delegate))
        {
            throw new ArgumentException($"Not Exist mapping for Types: From ({typeof(TIn).FullName}), To ({typeof(TOut).FullName})");
        }
        return (Func<IMapper, TIn, TOut>)@delegate;
    }
}
