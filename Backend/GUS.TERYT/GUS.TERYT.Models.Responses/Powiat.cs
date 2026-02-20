// Ignore Spelling: Wojewodztwo, Powiat
using HotChocolate;

namespace GUS.TERYT.Models.Responses;

public sealed class Powiat
{
    [GraphQLName("PowiatType")]
    public sealed record Type(int Code, string Name);


    public string WojewodztwoCode { get; init; } = null!;
    public string PowiatCode { get; init; } = null!;
    public string Name { get; init; } = null!;
    public Type PowiatType { get; init; } = null!;
}