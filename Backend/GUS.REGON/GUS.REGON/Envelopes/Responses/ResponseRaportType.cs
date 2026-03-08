// Ignore Spelling: Regon, Raport, dane
using GUS.REGON.Models.Responses.Enums;
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses;

public abstract partial record Response
{
    [XmlRoot(ElementName = "dane", Namespace = "http://CIS/BIR/PUBL/2014/07", IsNullable = false)]
    public sealed record RaportType : Response
    {
        [XmlElement("Typ", IsNullable = true)]
        public TypJednostki? Typ { get; init; }
    }
}