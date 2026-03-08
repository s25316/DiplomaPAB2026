// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Kraj, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters.Names;

public enum KrajNazwaParameter
{
    [XmlEnum("fiz_adSiedzKraj_Nazwa")]
    fiz_adSiedzKraj_Nazwa,

    [XmlEnum("lokpraw_adSiedzKraj_Nazwa")]
    lokpraw_adSiedzKraj_Nazwa,

    [XmlEnum("praw_adSiedzKraj_Nazwa")]
    praw_adSiedzKraj_Nazwa,

    [XmlEnum("lokfiz_adSiedzKraj_Nazwa")]
    lokfiz_adSiedzKraj_Nazwa,
}