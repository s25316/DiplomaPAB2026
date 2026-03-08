// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Miejscowosc, Poczty, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters.Names;

public enum MiejscowoscPocztyNazwaParameter
{
    [XmlEnum("fiz_adSiedzMiejscowoscPoczty_Nazwa")]
    fiz_adSiedzMiejscowoscPoczty_Nazwa,

    [XmlEnum("lokpraw_adSiedzMiejscowoscPoczty_Nazwa")]
    lokpraw_adSiedzMiejscowoscPoczty_Nazwa,

    [XmlEnum("praw_adSiedzMiejscowoscPoczty_Nazwa")]
    praw_adSiedzMiejscowoscPoczty_Nazwa,

    [XmlEnum("lokfiz_adSiedzMiejscowoscPoczty_Nazwa")]
    lokfiz_adSiedzMiejscowoscPoczty_Nazwa,
}