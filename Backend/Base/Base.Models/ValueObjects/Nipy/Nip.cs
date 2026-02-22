// Ignore Spelling: Nipy, Nip
using Base.Models.Exceptions;
using Base.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Base.Models.ValueObjects.Nipy;

[ModelBinder(typeof(NipBinder))]
public sealed record Nip
{
    private static readonly Regex regex = new(@"^\d{10}$");

    public string Value { get; }


    private Nip(string value)
    {
        Value = value;
    }


    public static implicit operator Nip(string value) => Parse(value);
    public static implicit operator string(Nip value) => value.Value;

    public static bool TryParse([NotNullWhen(true)] string? value, [NotNullWhen(true)] out Nip? result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = null;
            return false;
        }

        var localValue = value.CutomizeInput();
        if (regex.IsMatch(localValue))
        {
            result = new Nip(localValue);
            return true;
        }

        result = null;
        return false;
    }

    public static Nip Parse(string value)
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
        return $"{Messages.NipErrorMessage}: {localValue}";
    }

    internal static string GetDescription() => Messages.NipDescription;
    public override string ToString() => Value;
}