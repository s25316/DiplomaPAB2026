// Ignore Spelling: Ulica
using HotChocolate;

namespace GUS.TERYT.Models.Responses;

public sealed class Ulica
{
    [GraphQLName("UlicaType")]
    public sealed record Type(int Code, string Name);


    public string UlicaId { get; init; } = null!;
    public string Name { get; init; } = null!;
    public Type? UlicaType { get; init; } = null;
}