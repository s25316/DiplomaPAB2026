// Ignore Spelling: Miejscowosci, Miejscowosc
using Base.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;

[ModelBinder(typeof(MiejscowoscTypeIdBinder))]
public sealed record MiejscowoscTypeId
{
    public string Value { get; }


    private MiejscowoscTypeId(string value)
    {
        Value = value;
    }

    public static implicit operator string(MiejscowoscTypeId value) => value.Value;
    public static implicit operator MiejscowoscTypeId(string value) => Parse(value);

    public static bool TryParse([NotNullWhen(true)] string? value, [NotNullWhen(true)] out MiejscowoscTypeId? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        var trimmedValue = value.Trim();
        if (!Regexes.MiejscowoscType.IsMatch(trimmedValue))
        {
            result = null;
            return false;
        }

        result = new MiejscowoscTypeId(trimmedValue);
        return true;
    }

    public static MiejscowoscTypeId Parse(string value)
    {
        if (!TryParse(value, out var result))
        {
            throw new ResourceException.IncorrectFormat(PrepareErrorMessage(value));
        }
        return result;
    }

    internal static string PrepareErrorMessage(string value) => $"{Messages.MiejscowoscTypeIdErrorMessage}: {value}";
    internal static string GetDescription() => Messages.MiejscowoscTypeIdDescription;
    public override string ToString() => Value;
}