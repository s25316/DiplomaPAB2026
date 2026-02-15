// Ignore Spelling: Gminy, Gmina
using Base.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace GUS.TERYT.Models.Requests.ValueObjects.Gminy;

[ModelBinder(typeof(GminaTypeIdBinder))]
public sealed record GminaTypeId
{
    public string Value { get; }


    private GminaTypeId(string value)
    {
        Value = value;
    }

    public static implicit operator string(GminaTypeId value) => value.Value;
    public static implicit operator GminaTypeId(string value) => Parse(value);

    public static bool TryParse([NotNullWhen(true)] string? value, [NotNullWhen(true)] out GminaTypeId? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        var trimmedValue = value.Trim();
        if (!Regexes.GminaType.IsMatch(trimmedValue))
        {
            result = null;
            return false;
        }

        result = new GminaTypeId(trimmedValue);
        return true;
    }

    public static GminaTypeId Parse(string value)
    {
        if (!TryParse(value, out var result))
        {
            throw new ResourceException.IncorrectFormat(PrepareErrorMessage(value));
        }
        return result;
    }

    internal static string PrepareErrorMessage(string value) => $"{Messages.GminaTypeIdErrorMessage}: {value}";
    internal static string GetDescription() => Messages.GminaTypeIdDescription;
    public override string ToString() => Value;
}