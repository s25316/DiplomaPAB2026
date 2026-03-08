// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Numer, Lokalu
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters;

public enum NumerLokaluParameter
{
    [XmlEnum("fiz_adSiedzNumerLokalu")]
    fiz_adSiedzNumerLokalu,

    [XmlEnum("lokpraw_adSiedzNumerLokalu")]
    lokpraw_adSiedzNumerLokalu,

    [XmlEnum("praw_adSiedzNumerLokalu")]
    praw_adSiedzNumerLokalu,

    [XmlEnum("lokfiz_adSiedzNumerLokalu")]
    lokfiz_adSiedzNumerLokalu,
}