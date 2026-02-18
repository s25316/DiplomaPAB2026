// Ignore Spelling: Miejscowosc

namespace GUS.TERYT.Models.Responses;

public sealed class Miejscowosc
{
    public sealed record Type(string Code, string Name);


    public string MiejscowoscId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Type MiejscowoscType { get; set; } = null!;
}