// Ignore Spelling: Wojewodztwo
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValidationAttributes;

namespace GUS.TERYT.Models.Requests.Parameters;

[WojewodztwoId]
public record WojewodztwoId(string Value)
{
    // [FromQuery]
    public static bool TryParse(string s, IFormatProvider provider, out WojewodztwoId? result)
    {
        result = new WojewodztwoId(s);
        return true;
    }

    public override string ToString() => Value;
}

public class WojewodztwoParameters : BaseParameters<WojewodztwoId>
{
    public string? SearchText { get; init; } = null;
}