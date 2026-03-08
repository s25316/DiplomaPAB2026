// ignore Spelling: Zaloguj, xmlns, wsa
using GUS.REGON.Configurations;
using GUS.REGON.ValueObjects;

namespace GUS.REGON.Envelopes.Requests;

internal abstract partial record Request
{
    public sealed record Zaloguj(Endpoint endpoint) : Request
    {
        public string Generate(Key key) => $@"
            <soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:ns=""http://CIS/BIR/PUBL/2014/07"">
            <soap:Header xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
                <wsa:Action>http://CIS/BIR/PUBL/2014/07/IUslugaBIRzewnPubl/Zaloguj</wsa:Action>
                <wsa:To>{endpoint}</wsa:To>
            </soap:Header>
            <soap:Body>
                <ns:Zaloguj>
                    <ns:pKluczUzytkownika>{key.Value}</ns:pKluczUzytkownika>
                </ns:Zaloguj>
            </soap:Body>
            </soap:Envelope>";
    }
}