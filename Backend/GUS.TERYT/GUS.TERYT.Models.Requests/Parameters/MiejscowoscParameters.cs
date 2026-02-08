// Ignore Spelling: Miejscowosc, Gmina
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValidationAttributes;

namespace GUS.TERYT.Models.Requests.Parameters;

[MiejscowoscId]
public record MiejscowoscId(string Value)
{
    // [FromQuery]
    public static bool TryParse(string s, IFormatProvider provider, out MiejscowoscId? result)
    {
        result = new MiejscowoscId(s);
        return true;
    }

    public override string ToString() => Value;
}

[MiejscowoscTypeId]
public record MiejscowoscTypeId(string Value)
{
    // [FromQuery]
    public static bool TryParse(string s, IFormatProvider provider, out MiejscowoscTypeId? result)
    {
        result = new MiejscowoscTypeId(s);
        return true;
    }

    public override string ToString() => Value;
}

public class MiejscowoscParameters : BaseParameters<MiejscowoscId>
{
    public string? SearchText { get; init; } = null;
    public IEnumerable<MiejscowoscTypeId> TypeIds { get; init; } = [];
    public IEnumerable<GminaId> GminaIds { get; init; } = [];
}