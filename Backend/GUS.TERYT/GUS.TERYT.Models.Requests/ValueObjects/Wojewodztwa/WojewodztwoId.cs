// Ignore Spelling: Wojewodztwo, Wojewodztwa, Voivodeship
using Base.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;

/// <summary>
/// Represents a unique Voivodeship (Province) identifier according to the TERYT classification.
/// </summary>
/// <remarks>
/// The expected format is a two-digit string (e.g., "02", "14").
/// </remarks>
[ModelBinder(typeof(WojewodztwoIdBinder))]
public sealed record WojewodztwoId
{
    public string Value { get; }


    private WojewodztwoId(string value)
    {
        Value = value;
    }

    public static implicit operator string(WojewodztwoId value) => value.Value;
    public static implicit operator WojewodztwoId(string value) => Parse(value);

    public static bool TryParse([NotNullWhen(true)] string? value, [NotNullWhen(true)] out WojewodztwoId? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        var trimmedValue = value.Trim();
        if (!Regexes.Wojewodztwo.IsMatch(trimmedValue))
        {
            result = null;
            return false;
        }

        result = new WojewodztwoId(trimmedValue);
        return true;
    }

    public static WojewodztwoId Parse(string value)
    {
        if (!TryParse(value, out var result))
        {
            throw new ResourceException.IncorrectFormat(PrepareErrorMessage(value));
        }
        return result;
    }

    internal static string PrepareErrorMessage(string value) => $"{Messages.WojewodztwoIdErrorMessage}: {value}";
    internal static string GetDescription() => Messages.WojewodztwoIdDecription;
    public override string ToString() => Value;
}