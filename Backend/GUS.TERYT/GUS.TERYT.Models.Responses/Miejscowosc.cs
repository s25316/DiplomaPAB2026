// Ignore Spelling: Wojewodztwo, Powiat, Rodz Gmina Miejscowosc

using HotChocolate;

namespace GUS.TERYT.Models.Responses;

public sealed class Miejscowosc
{
    [GraphQLName("MiejscowoscType")]
    public sealed record Type(string Code, string Name);


    public string WojewodztwoCode { get; init; } = null!;
    public string PowiatCode { get; init; } = null!;
    public string GminaCode { get; init; } = null!;
    public string GminaRodzCode { get; init; } = null!;
    public string MiejscowoscId { get; init; } = null!;
    public string Name { get; init; } = null!;
    public Type MiejscowoscType { get; init; } = null!;
}