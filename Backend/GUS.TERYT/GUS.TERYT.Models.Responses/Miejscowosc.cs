// Ignore Spelling: Miejscowosc

using HotChocolate;

namespace GUS.TERYT.Models.Responses;

public sealed class Miejscowosc
{
    [GraphQLName("MiejscowoscType")]
    public sealed record Type(string Code, string Name);


    public string MiejscowoscId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Type MiejscowoscType { get; set; } = null!;
}