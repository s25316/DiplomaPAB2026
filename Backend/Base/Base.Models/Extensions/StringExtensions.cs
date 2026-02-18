namespace Base.Models.Extensions;

public static class StringExtensions
{
    public static IEnumerable<string> SplitSearchText(this string value) => value
        .ToLowerInvariant()
        .Split([',', ' '], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}