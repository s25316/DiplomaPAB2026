// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Ulica
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters.Symbols;

public enum UlicaSymbolParameter
{
    [XmlEnum("fiz_adSiedzUlica_Symbol")]
    fiz_adSiedzUlica_Symbol,

    [XmlEnum("lokpraw_adSiedzUlica_Symbol")]
    lokpraw_adSiedzUlica_Symbol,

    [XmlEnum("praw_adSiedzUlica_Symbol")]
    praw_adSiedzUlica_Symbol,

    [XmlEnum("lokfiz_adSiedzUlica_Symbol")]
    lokfiz_adSiedzUlica_Symbol,
}