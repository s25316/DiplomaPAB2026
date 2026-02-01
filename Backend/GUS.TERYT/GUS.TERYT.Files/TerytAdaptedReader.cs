// Ignore Spelling: Teryt
using AdaptedSimc = GUS.TERYT.Files.Models.Adapted.Teryt.Simc;
using AdaptedSimcUlicIds = GUS.TERYT.Files.Models.Adapted.Teryt.SimcUlicIds;
using AdaptedTerc = GUS.TERYT.Files.Models.Adapted.Teryt.Terc;
using AdaptedTeryt = GUS.TERYT.Files.Models.Adapted.Teryt;
using AdaptedUlic = GUS.TERYT.Files.Models.Adapted.Teryt.Ulic;
using AdaptedUlicInfo = GUS.TERYT.Files.Models.Adapted.Teryt.UlicInfo;
using SourceSimc = GUS.TERYT.Files.Models.Source.Teryt.Simc;
using SourceTerc = GUS.TERYT.Files.Models.Source.Teryt.Terc;
using SourceUlic = GUS.TERYT.Files.Models.Source.Teryt.Ulic;

namespace GUS.TERYT.Files;

public class TerytAdaptedReader<T> : BaseFileReader
    where T : AdaptedTeryt
{
    private static readonly Dictionary<Type, Func<string, AdaptedTeryt>> mappers = new()
    {
        { typeof(AdaptedTerc), (value) =>  AdaptedTerc.Parse(SourceTerc.Parse(value)) },
        { typeof(AdaptedSimc), (value) =>  AdaptedSimc.Parse(SourceSimc.Parse(value)) },
        { typeof(AdaptedUlic), (value) =>  AdaptedUlic.Parse(SourceUlic.Parse(value)) },
        { typeof(AdaptedUlicInfo), (value) =>  AdaptedUlicInfo.Parse(SourceUlic.Parse(value)) },
        { typeof(AdaptedSimcUlicIds), (value) =>  AdaptedSimcUlicIds.Parse(SourceUlic.Parse(value)) },
    };

    private readonly Func<string, T> mappingFunc;

    public TerytAdaptedReader(string filePath) : base(filePath)
    {
        if (!mappers.TryGetValue(typeof(T), out var mapFunc))
        {
            throw new NotImplementedException($"Unknown type for mapping {nameof(T)}");
        }
        this.mappingFunc = (value) => (T)mapFunc(value);
    }

    public TerytAdaptedReader(StreamReader streamReader) : base(streamReader)
    {
        if (!mappers.TryGetValue(typeof(T), out var mapFunc))
        {
            throw new NotImplementedException($"Unknown type for mapping {nameof(T)}");
        }
        this.mappingFunc = (value) => (T)mapFunc(value);
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
