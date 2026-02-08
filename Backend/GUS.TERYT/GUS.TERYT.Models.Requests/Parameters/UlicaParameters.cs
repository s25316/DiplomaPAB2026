// Ignore Spelling: Ulica
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValidationAttributes;

namespace GUS.TERYT.Models.Requests.Parameters;

[UlicaId]
public record UlicaId(string Value)
{
    // [FromQuery]
    public static bool TryParse(string s, IFormatProvider provider, out UlicaId? result)
    {
        result = new UlicaId(s);
        return true;
    }

    public override string ToString() => Value;
}

public class UlicaParameters : BaseParameters<UlicaId>
{
    public string? SearchText { get; init; } = null;
    public IEnumerable<int> TypeIds { get; init; } = [];
}