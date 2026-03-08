// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Gmina, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters.Names;

public enum GminaNazwaParameter
{
    [XmlEnum("fiz_adSiedzGmina_Nazwa")]
    fiz_adSiedzGmina_Nazwa,

    [XmlEnum("lokpraw_adSiedzGmina_Nazwa")]
    lokpraw_adSiedzGmina_Nazwa,

    [XmlEnum("praw_adSiedzGmina_Nazwa")]
    praw_adSiedzGmina_Nazwa,

    [XmlEnum("lokfiz_adSiedzGmina_Nazwa")]
    lokfiz_adSiedzGmina_Nazwa,
}