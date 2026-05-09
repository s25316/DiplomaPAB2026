namespace RADON.Models.Dictionaries.Responses;

public sealed record DictionaryItem
{
    public required string Code { get; init; }
    public required string Name { get; init; }
};