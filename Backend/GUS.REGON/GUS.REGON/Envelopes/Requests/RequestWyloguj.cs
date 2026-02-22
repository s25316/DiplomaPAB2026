// Ignore Spelling: Wyloguj, xmlns, wsa
using GUS.REGON.Configurations;

namespace GUS.REGON.Envelopes.Requests;

internal abstract partial class Request
{
    public sealed class Wyloguj(Endpoint endpoint) : Request
    {
        public string Generate(string sessionId) => $@"
            <soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:ns=""http://CIS/BIR/PUBL/2014/07"">
            <soap:Header xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
                <wsa:To>{endpoint}</wsa:To>
                <wsa:Action>http://CIS/BIR/PUBL/2014/07/IUslugaBIRzewnPubl/Wyloguj</wsa:Action>
            </soap:Header>
            <soap:Body>
                <ns:Wyloguj>
                    <ns:pIdentyfikatorSesji>{sessionId}</ns:pIdentyfikatorSesji>
                </ns:Wyloguj>
            </soap:Body>
            </soap:Envelope>";
    }
}