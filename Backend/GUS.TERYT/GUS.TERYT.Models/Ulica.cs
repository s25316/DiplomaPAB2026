// Ignore Spelling: Ulica

namespace GUS.TERYT.Models;

public sealed class Ulica
{
    public sealed record Type(int Code, string Name);


    public string UlicaCode { get; init; } = null!;
    public string Name { get; init; } = null!;
    public Type? UlicaType { get; init; } = null;
}
