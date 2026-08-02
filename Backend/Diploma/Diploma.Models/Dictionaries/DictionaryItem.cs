namespace Diploma.Models.Dictionaries;

public sealed record DictionaryItem<T>
{
    public required T Code { get; init; }
    public required string Name { get; init; }
}