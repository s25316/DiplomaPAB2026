// Ignore Spelling: Teryt
using GUS.TERYT.Files.Models.Source;

namespace GUS.TERYT.Files;

public class TerytSourceReader<T> : BaseFileReader
    where T : Teryt
{
    private static readonly Dictionary<Type, Func<string, Teryt>> mappers = new()
    {
        { typeof(Teryt.Terc), (Func<string, Teryt.Terc>)Teryt.Terc.Parse },
        { typeof(Teryt.Simc), (Func<string, Teryt.Simc>)Teryt.Simc.Parse },
        { typeof(Teryt.Ulic), (Func<string, Teryt.Ulic>)Teryt.Ulic.Parse }
    };

    private readonly Func<string, T> mappingFunc;


    public TerytSourceReader(string filePath) : base(filePath)
    {
        if (!mappers.TryGetValue(typeof(T), out var mappingFunc))
        {
            throw new NotImplementedException($"Unknown type for mapping {nameof(T)}");
        }
        this.mappingFunc = (Func<string, T>)mappingFunc;
    }

    public TerytSourceReader(StreamReader streamReader) : base(streamReader)
    {
        if (!mappers.TryGetValue(typeof(T), out var mappingFunc))
        {
            throw new NotImplementedException($"Unknown type for mapping {nameof(T)}");
        }
        this.mappingFunc = (value) => (T)mappingFunc(value);
    }


    public async IAsyncEnumerable<T> ReadAsync()
    {
        await foreach (var line in ReadRawAsync())
        {
            yield return mappingFunc(line);
        }
    }

    public async Task<IEnumerable<T>> ReadAllAsync()
    {
        var items = new List<T>();

        await foreach (var item in ReadAsync())
        {
            items.Add(item);
        }

        return items;
    }
}