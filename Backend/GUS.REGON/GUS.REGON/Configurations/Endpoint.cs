namespace GUS.REGON.Configurations;

internal abstract record Endpoint(string Value)
{
    private const string TESTING_ENDPOINT = "https://wyszukiwarkaregontest.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc";
    private const string PRODUCTION_ENDPOINT = "https://wyszukiwarkaregon.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc";


    public record Testing() : Endpoint(TESTING_ENDPOINT);
    public record Production() : Endpoint(PRODUCTION_ENDPOINT);


    public override string ToString() => Value;
}