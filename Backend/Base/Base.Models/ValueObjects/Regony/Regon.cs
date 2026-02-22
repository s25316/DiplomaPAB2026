// Ignore Spelling: Regony, Regon
using Base.Models.Exceptions;
using Base.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Base.Models.ValueObjects.Regony;

[ModelBinder(typeof(RegonBinder))]
public sealed record Regon
{
    private static readonly Regex regex = new(@"^(\d{9}|\d{14})$");

    public string Value { get; }


    private Regon(string value)
    {
        Value = value;
    }


    public static implicit operator Regon(string value) => Parse(value);
    public static implicit operator string(Regon value) => value.Value;

    public static bool TryParse([NotNullWhen(true)] string? value, [NotNullWhen(true)] out Regon? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        var localValue = value.CutomizeInput();
        if (regex.IsMatch(localValue))
        {
            result = new Regon(localValue);
            return true;
        }

        result = null;
        return false;
    }

    public static Regon Parse(string value)
    {
        if (!TryParse(value, out var result))
        {
            throw new ResourceException.IncorrectFormat(PrepareErrorMessage(value));
        }
        return result;
    }

    internal static string PrepareErrorMessage(string value)
    {
        var localValue = value.CutomizeInput();
        return $"{Messages.RegonErrorMessage}: {localValue}";
    }

    internal static string GetDescription() => Messages.RegonDescription;
    public override string ToString() => Value;
}