// Ignore Spelling: xmlns, wsa, Komunikat, Uslugi, Komunikat, Tresc, Kod, Danych, Sesji
using GUS.REGON.Configurations;

namespace GUS.REGON.Envelopes.Requests;

internal abstract partial class Request
{
    public sealed class GetValue(Endpoint endpoint) : Request
    {
        private const string PARAMETER_KOMUNIKAT_USLUGI = "KomunikatUslugi";
        private const string PARAMETER_KOMUNIKAT_TRESC = "KomunikatTresc";
        private const string PARAMETER_KOMUNIKAT_KOD = "KomunikatKod";
        private const string PARAMETER_STATUS_SESJI = "StatusSesji";
        private const string PARAMETER_STATUS_USLUGI = "StatusUslugi";
        private const string PARAMETER_STAN_DANYCH = "StanDanych";


        // UnAuthorize
        public string KomunikatUslugi() => Generate(PARAMETER_KOMUNIKAT_USLUGI);
        public string StatusSesji() => Generate(PARAMETER_STATUS_SESJI);
        public string StatusUslugi() => Generate(PARAMETER_STATUS_USLUGI);
        // Authorize
        public string KomunikatKod() => Generate(PARAMETER_KOMUNIKAT_KOD);
        public string KomunikatTresc() => Generate(PARAMETER_KOMUNIKAT_TRESC);
        public string StanDanych() => Generate(PARAMETER_STAN_DANYCH);

        private string Generate(string parameter) => $@"
            <soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:ns=""http://CIS/BIR/2014/07"">
            <soap:Header xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
            <wsa:To>{endpoint}</wsa:To>
            <wsa:Action>http://CIS/BIR/2014/07/IUslugaBIR/GetValue</wsa:Action>
            </soap:Header>
                <soap:Body>
                    <ns:GetValue>
                        <ns:pNazwaParametru>{parameter}</ns:pNazwaParametru>
                    </ns:GetValue>
                </soap:Body>
            </soap:Envelope>";
    }
}