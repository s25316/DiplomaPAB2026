// Ignore Spelling: Powiat, Wojewodztwo
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValidationAttributes;

namespace GUS.TERYT.Models.Requests.Parameters;

[PowiatId]
public record PowiatId
{
    public string WojewodztwoCode { get; }
    public string PowiatCode { get; }


    private PowiatId(string wojewodztwoCode, string powiatCode)
    {
        WojewodztwoCode = wojewodztwoCode;
        PowiatCode = powiatCode;
    }


    // [FromQuery]
    public static bool TryParse(string s, IFormatProvider provider, out PowiatId? result)
    {
        var array = s.Split('.');

        var wojewodztwoCode = array.Any() ? array[0] : String.Empty;
        var powiatCode = array.Count() >= 2 ? array[1] : String.Empty;

        result = new PowiatId(wojewodztwoCode, powiatCode);
        return true;
    }

    public override string ToString() => $"{WojewodztwoCode}.{PowiatCode}";
}

public class PowiatParameters : BaseParameters<PowiatId>
{
    public string? SearchText { get; init; } = null;
    public IList<int> TypeIds { get; init; } = [];
    public IList<WojewodztwoId> WojewodztwoIds { get; init; } = [];
}
