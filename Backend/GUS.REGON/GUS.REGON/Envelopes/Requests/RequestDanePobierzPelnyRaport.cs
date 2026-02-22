// Ignore Spelling: xmlns, wsa, regon, Pobierz, Pelny, Raport
using Base.Models.ValueObjects.Regony;
using GUS.REGON.Configurations;

namespace GUS.REGON.Envelopes.Requests;

internal abstract partial class Request
{
    public sealed class DanePobierzPelnyRaport(Endpoint endpoint) : Request
    {
        public string Generate(Regon regon, string reportName) => $@"
            <soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:ns=""http://CIS/BIR/PUBL/2014/07"">
            <soap:Header xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
            <wsa:To>{endpoint}</wsa:To>
            <wsa:Action>http://CIS/BIR/PUBL/2014/07/IUslugaBIRzewnPubl/DanePobierzPelnyRaport</wsa:Action>            
            </soap:Header>
                <soap:Body>
                    <ns:DanePobierzPelnyRaport>
                        <ns:pRegon>{regon.Value}</ns:pRegon>         
                        <ns:pNazwaRaportu>{reportName}</ns:pNazwaRaportu>
                    </ns:DanePobierzPelnyRaport>
                </soap:Body>
            </soap:Envelope>";
    }
}
