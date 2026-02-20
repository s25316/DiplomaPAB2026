// Ignore Spelling: Gminy, Gmina, Wojewodztwo, Powiat, Rodz
using Base.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace GUS.TERYT.Models.Requests.ValueObjects.Gminy;

[ModelBinder(typeof(GminaIdBinder))]
public sealed record GminaId
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


    public static implicit operator string(GminaId value) => value.ToString();
    public static implicit operator GminaId(string value) => Parse(value);

    public static bool TryParse([NotNullWhen(true)] string? value, [NotNullWhen(true)] out GminaId? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        value = value.Trim();
        if (!Regexes.Gmina.IsMatch(value))
        {
            result = null;
            return false;
        }
        var array = value.Split('.');

        var wojewodztwoCode = array[0];
        var powiatCode = array[1];
        var gminaSegment = array[2];

        var gminaCode = gminaSegment.Substring(0, 2);
        var gminaRodz = gminaSegment.Substring(2);
        result = new GminaId(wojewodztwoCode, powiatCode, gminaCode, gminaRodz);
        return true;
    }

    public static GminaId Parse(string value)
    {
        if (!TryParse(value, out var result))
        {
            throw new ResourceException.IncorrectFormat(PrepareErrorMessage(value));
        }
        return result;
    }

    internal static string PrepareErrorMessage(string value) => $"{Messages.GminaIdErrorMessage}: {value}";
    internal static string GetDescription() => Messages.GminaIdDescription;
    public override string ToString() => $"{WojewodztwoCode}.{PowiatCode}.{GminaCode}{GminaRodz}";
}