// Ignore Spelling: Wojewodztwo, Powiat

namespace GUS.TERYT.Models;

public sealed class Powiat
{
    public sealed record Type(int Code, string Name);


    public string WojewodztwoCode { get; init; } = null!;
    public string PowiatCode { get; init; } = null!;
    public string Name { get; init; } = null!;
    public Type PowiatType { get; init; } = null!;
}
