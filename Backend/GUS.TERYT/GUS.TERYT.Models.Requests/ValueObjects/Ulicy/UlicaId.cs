// Ignore Spelling: Ulicy, Ulica
using Base.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace GUS.TERYT.Models.Requests.ValueObjects.Ulicy;

[ModelBinder(typeof(UlicaIdBinder))]
public sealed record UlicaId
{
    public string Value { get; }


    private UlicaId(string value)
    {
        Value = value;
    }

    public static implicit operator string(UlicaId value) => value.Value;
    public static implicit operator UlicaId(string value) => Parse(value);

    public static bool TryParse([NotNullWhen(true)] string? value, [NotNullWhen(true)] out UlicaId? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        var trimmedValue = value.Trim();
        if (!Regexes.Ulica.IsMatch(trimmedValue))
        {
            result = null;
            return false;
        }

        result = new UlicaId(trimmedValue);
        return true;
    }

    public static UlicaId Parse(string value)
    {
        if (!TryParse(value, out var result))
        {
            throw new ResourceException.IncorrectFormat(PrepareErrorMessage(value));
        }
        return result;
    }

    internal static string PrepareErrorMessage(string value) => $"{Messages.UlicaIdErrorMessage}: {value}";
    internal static string GetDescription() => Messages.UlicaIdDescription;
    public override string ToString() => Value;
}