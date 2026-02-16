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

    public TOut Map<TOut>(object? item)
    {
        ArgumentNullException.ThrowIfNull(item);
        var inputType = item.GetType();
        var outputType = typeof(TOut);
        var @delegate = GetDelegate(inputType, outputType);

        return (TOut)@delegate.DynamicInvoke(this, item)!;
    }


    private static FieldInfo GetFieldInfo(string fieldName)
    {
        return typeof(MappingConfiguration).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
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

    public Delegate GetDelegate(Type input, Type output)
    {
        if (!dictionary.TryGetValue((input, output), out var @delegate))
        {
            throw new InvalidOperationException($"Not Exist mapping for Types: From ({input.FullName}), To ({output.FullName})");
        }
        return @delegate;
    }
}
