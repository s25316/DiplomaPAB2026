using Base.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Diploma.Domain.ValueObjects;

public sealed record Email
{
    private const string REGEX = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public string Value { get; }


    public Email(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ResourceException.IncorrectFormat($"Wartośc e-mail pusta");

        if (!IsMatch(value))
            throw new ResourceException.IncorrectFormat($"Niepoprawny format e-mail: {value}");

        Value = value;
    }


    public static bool IsMatch(string value) => Regex.IsMatch(value, REGEX, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    public static bool TryParse(string? value, [NotNullWhen(true)] out Email? email)
    {
        email = null;
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!IsMatch(value))
            return false;

        email = new Email(value);
        return true;
    }
}