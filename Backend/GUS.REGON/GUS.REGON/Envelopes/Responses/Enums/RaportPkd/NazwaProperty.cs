// Ignore Spelling: Regon, Plugin, Raport, Raporty, PKD, Enums, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses.Enums.RaportPkd;

public enum NazwaProperty
{
    [XmlEnum("praw_pkdNazwa")]
    praw_pkdNazwa,

    [XmlEnum("lokpraw_pkdNazwa")]
    lokpraw_pkdNazwa,

    [XmlEnum("lokfiz_pkd_Nazwa")]
    lokfiz_pkd_Nazwa,

    [XmlEnum("fiz_pkd_Nazwa")]
    fiz_pkd_Nazwa,
}