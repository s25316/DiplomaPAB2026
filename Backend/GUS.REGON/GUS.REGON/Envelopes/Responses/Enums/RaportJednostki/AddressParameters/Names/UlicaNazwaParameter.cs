// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Ulica, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters.Names;

public enum UlicaNazwaParameter
{
    [XmlEnum("fiz_adSiedzUlica_Nazwa")]
    fiz_adSiedzUlica_Nazwa,

    [XmlEnum("lokpraw_adSiedzUlica_Nazwa")]
    lokpraw_adSiedzUlica_Nazwa,

    [XmlEnum("praw_adSiedzUlica_Nazwa")]
    praw_adSiedzUlica_Nazwa,

    [XmlEnum("lokfiz_adSiedzUlica_Nazwa")]
    lokfiz_adSiedzUlica_Nazwa,
}