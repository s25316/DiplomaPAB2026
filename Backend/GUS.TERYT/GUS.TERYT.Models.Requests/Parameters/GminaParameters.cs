// Ignore Spelling: Powiat, Wojewodztwo, Gmina, Rodz
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValidationAttributes;

namespace GUS.TERYT.Models.Requests.Parameters;

[GminaId]
public record GminaId
{
    public string WojewodztwoCode { get; }
    public string PowiatCode { get; }
    public string GminaCode { get; }
    public string GminaRodz { get; }


    private GminaId(string wojewodztwoCode, string powiatCode, string gminaCode, string gminaRodz)
    {
        WojewodztwoCode = wojewodztwoCode;
        PowiatCode = powiatCode;
        GminaCode = gminaCode;
        GminaRodz = gminaRodz;
    }


    // [FromQuery]
    public static bool TryParse(string s, IFormatProvider provider, out GminaId? result)
    {
        var array = s.Split('.');

        var wojewodztwoCode = array.Any() ? array[0] : String.Empty;
        var powiatCode = array.Count() >= 2 ? array[1] : String.Empty;
        var gminaSegment = array.Count() >= 3 ? array[2] : String.Empty;

        var gminaCode = gminaSegment.Length >= 2 ? gminaSegment.Substring(0, 2) : String.Empty;
        var gminaRodz = gminaSegment.Length >= 3 ? gminaSegment.Substring(2) : String.Empty;

        result = new GminaId(wojewodztwoCode, powiatCode, gminaCode, gminaRodz);
        return true;
    }

    public override string ToString() => $"{WojewodztwoCode}.{PowiatCode}.{GminaCode}{GminaRodz}";
}

[GminaTypeId]
public record GminaTypeId(string Value)
{
    // [FromQuery]
    public static bool TryParse(string s, IFormatProvider provider, out GminaTypeId? result)
    {
        result = new GminaTypeId(s);
        return true;
    }

    public override string ToString() => Value;
}


public class GminaParameters : BaseParameters<GminaId>
{
    public string? SearchText { get; init; } = null;
    public IEnumerable<GminaTypeId> TypeIds { get; init; } = [];
    public IEnumerable<PowiatId> PowiatIds { get; init; } = [];
}
