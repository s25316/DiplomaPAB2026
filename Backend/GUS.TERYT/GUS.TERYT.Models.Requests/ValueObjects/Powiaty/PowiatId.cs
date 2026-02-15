// Ignore Spelling: Powiaty, Powiat, Wojewodztwo
using Base.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace GUS.TERYT.Models.Requests.ValueObjects.Powiaty;

/// <summary>
/// Represents a unique District identifier (Powiat) based on TERYT hierarchy.
/// </summary>
/// <remarks>
/// Format: <c>WW.PP</c> where:
/// <list type="bullet">
/// <item><description>WW: Wojewodztwo Code (2 digits)</description></item>
/// <item><description>PP: Powiat Code (2 digits)</description></item>
/// </list>
/// </remarks>
/// <example>Example: "14.65" (for Warsaw)</example>

[ModelBinder(typeof(PowiatIdBinder))]
public sealed record PowiatId
{
    public string WojewodztwoCode { get; }
    public string PowiatCode { get; }


    private PowiatId(string wojewodztwoCode, string powiatCode)
    {
        WojewodztwoCode = wojewodztwoCode;
        PowiatCode = powiatCode;
    }


    public static implicit operator string(PowiatId value) => value.ToString();
    public static implicit operator PowiatId(string value) => Parse(value);

    public static bool TryParse([NotNullWhen(true)] string value, [NotNullWhen(true)] out PowiatId? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        value = value.Trim();
        if (!Regexes.Powiat.IsMatch(value))
        {
            result = null;
            return false;
        }
        var array = value.Split('.');

        var wojewodztwoCode = array[0];
        var powiatCode = array[1];

        result = new PowiatId(wojewodztwoCode, powiatCode);
        return true;
    }

    public static PowiatId Parse(string value)
    {
        if (!TryParse(value, out var result))
        {
            throw new ResourceException.IncorrectFormat(PrepareErrorMessage(value));
        }
        return result;
    }

    internal static string PrepareErrorMessage(string value) => $"{Messages.PowiatIdErrorMessage}: {value}";
    internal static string GetDescription() => Messages.PowiatIdDescription;
    public override string ToString() => $"{WojewodztwoCode}.{PowiatCode}";
}