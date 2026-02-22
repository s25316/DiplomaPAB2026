// Ignore Spelling: xmlns, wsa, Regon, Szukaj
using Base.Models.ValueObjects.Krsy;
using Base.Models.ValueObjects.Nipy;
using Base.Models.ValueObjects.Regony;
using GUS.REGON.Configurations;

namespace GUS.REGON.Envelopes.Requests;

internal abstract partial class Request
{
    public sealed class DaneSzukaj(Endpoint endpoint) : Request
    {
        public string Generate(Regon item) => Generate($"<dat:Regon>{item.Value}</dat:Regon>");
        public string Generate(Krs item) => Generate($"<dat:Krs>{item.Value}</dat:Krs>");
        public string Generate(Nip item) => Generate($"<dat:Nip>{item.Value}</dat:Nip>");

        private string Generate(string parameters) => $@"
            <soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:ns=""http://CIS/BIR/PUBL/2014/07"" xmlns:dat=""http://CIS/BIR/PUBL/2014/07/DataContract"">
            <soap:Header xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
                <wsa:To>{endpoint}</wsa:To>
                <wsa:Action>http://CIS/BIR/PUBL/2014/07/IUslugaBIRzewnPubl/DaneSzukaj</wsa:Action>
            </soap:Header>
            <soap:Body>
                <ns:DaneSzukaj>
                    <ns:pParametryWyszukiwania>
                        {parameters}
                    </ns:pParametryWyszukiwania>
                </ns:DaneSzukaj>
            </soap:Body>
            </soap:Envelope>";
    }
}