// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Powiat
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters.Symbols;

public enum PowiatSymbolParameter
{
    [XmlEnum("fiz_adSiedzPowiat_Symbol")]
    fiz_adSiedzPowiat_Symbol,

    [XmlEnum("lokpraw_adSiedzPowiat_Symbol")]
    lokpraw_adSiedzPowiat_Symbol,

    [XmlEnum("praw_adSiedzPowiat_Symbol")]
    praw_adSiedzPowiat_Symbol,

    [XmlEnum("lokfiz_adSiedzPowiat_Symbol")]
    lokfiz_adSiedzPowiat_Symbol,
}