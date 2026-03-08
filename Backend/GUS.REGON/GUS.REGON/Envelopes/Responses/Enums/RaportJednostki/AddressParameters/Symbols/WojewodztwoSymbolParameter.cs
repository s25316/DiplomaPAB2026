// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Wojewodztwo
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters.Symbols;

public enum WojewodztwoSymbolParameter
{
    [XmlEnum("fiz_adSiedzWojewodztwo_Symbol")]
    fiz_adSiedzWojewodztwo_Symbol,

    [XmlEnum("lokpraw_adSiedzWojewodztwo_Symbol")]
    lokpraw_adSiedzWojewodztwo_Symbol,

    [XmlEnum("praw_adSiedzWojewodztwo_Symbol")]
    praw_adSiedzWojewodztwo_Symbol,

    [XmlEnum("lokfiz_adSiedzWojewodztwo_Symbol")]
    lokfiz_adSiedzWojewodztwo_Symbol,
}