// Ignore Spelling: xmlns, wsa, Komunikat, Uslugi, Komunikat, Tresc, Kod, Danych, Sesji
using GUS.REGON.Configurations;

namespace GUS.REGON.Envelopes.Requests;

internal abstract partial record Request
{
    public sealed record GetValue(Endpoint endpoint) : Request
    {
        private const string PARAMETER_KOMUNIKAT_USLUGI = "KomunikatUslugi";
        private const string PARAMETER_KOMUNIKAT_TRESC = "KomunikatTresc";
        private const string PARAMETER_KOMUNIKAT_KOD = "KomunikatKod";
        private const string PARAMETER_STATUS_SESJI = "StatusSesji";
        private const string PARAMETER_STATUS_USLUGI = "StatusUslugi";
        private const string PARAMETER_STAN_DANYCH = "StanDanych";


        // UnAuthorize
        /// <returns>string</returns>
        public string KomunikatUslugi() => Generate(PARAMETER_KOMUNIKAT_USLUGI);

        /// <returns>enum StatusUslugi</returns>
        public string StatusUslugi() => Generate(PARAMETER_STATUS_USLUGI);

        /// <returns>enum StatusSesji</returns>
        public string StatusSesji() => Generate(PARAMETER_STATUS_SESJI);

        // Authorize

        /// <returns>enum KomunikatKod?</returns>
        public string KomunikatKod() => Generate(PARAMETER_KOMUNIKAT_KOD);

        /// <returns>string?</returns>
        public string KomunikatTresc() => Generate(PARAMETER_KOMUNIKAT_TRESC);

        /// <returns>current date DateOnly? in format "dd-MM-yyyy"</returns>
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