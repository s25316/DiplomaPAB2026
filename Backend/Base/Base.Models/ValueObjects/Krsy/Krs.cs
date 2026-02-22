// Ignore Spelling: Krsy, Krs
using Base.Models.Exceptions;
using Base.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Base.Models.ValueObjects.Krsy;

[ModelBinder(typeof(KrsBinder))]
public sealed record Krs
{
    private static readonly Regex regex = new(@"^\d{10}$");

    public string Value { get; }


    private Krs(string value)
    {
        Value = value;
    }


    public static implicit operator Krs(string value) => Parse(value);
    public static implicit operator string(Krs value) => value.Value;

    public static bool TryParse([NotNullWhen(true)] string? value, [NotNullWhen(true)] out Krs? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        var localValue = value.CutomizeInput();
        if (regex.IsMatch(localValue))
        {
            result = new Krs(localValue);
            return true;
        }

        result = null;
        return false;
    }

    public static Krs Parse(string value)
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
        return $"{Messages.KrsErrorMessage}: {localValue}";
    }

    internal static string GetDescription() => Messages.KrsDescription;
    public override string ToString() => Value;
}