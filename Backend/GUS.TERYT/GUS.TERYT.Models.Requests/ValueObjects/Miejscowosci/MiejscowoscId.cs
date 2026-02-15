// Ignore Spelling: Miejscowosci, Miejscowosc
using Base.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;

[ModelBinder(typeof(MiejscowoscIdBinder))]
public sealed record MiejscowoscId
{
    public string Value { get; }


    private MiejscowoscId(string value)
    {
        Value = value;
    }

    public static implicit operator string(MiejscowoscId value) => value.Value;
    public static implicit operator MiejscowoscId(string value) => Parse(value);

    public static bool TryParse([NotNullWhen(true)] string? value, [NotNullWhen(true)] out MiejscowoscId? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        var trimmedValue = value.Trim();
        if (!Regexes.Miejscowosc.IsMatch(trimmedValue))
        {
            result = null;
            return false;
        }

        result = new MiejscowoscId(trimmedValue);
        return true;
    }

    public static MiejscowoscId Parse(string value)
    {
        if (!TryParse(value, out var result))
        {
            throw new ResourceException.IncorrectFormat(PrepareErrorMessage(value));
        }
        return result;
    }

    internal static string PrepareErrorMessage(string value) => $"{Messages.MiejscowoscIdErrorMessage}: {value}";
    internal static string GetDescription() => Messages.MiejscowoscIdDescription;
    public override string ToString() => Value;
}