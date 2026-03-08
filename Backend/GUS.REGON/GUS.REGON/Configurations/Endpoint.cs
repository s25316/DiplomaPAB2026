namespace GUS.REGON.Configurations;

internal sealed record Endpoint(Uri Value)
{
    private const string TESTING_ENDPOINT = "https://wyszukiwarkaregontest.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc";
    private const string PRODUCTION_ENDPOINT = "https://wyszukiwarkaregon.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc";


    public static readonly Endpoint Testing = new(new Uri(TESTING_ENDPOINT));
    public static readonly Endpoint Production = new(new Uri(PRODUCTION_ENDPOINT));

    public override string ToString() => Value.ToString();
}