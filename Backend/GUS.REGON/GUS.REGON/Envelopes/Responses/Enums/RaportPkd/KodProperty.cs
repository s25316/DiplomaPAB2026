// Ignore Spelling: Regon, Plugin, Raport, Raporty, PKD, Enums, Kod
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses.Enums.RaportPkd;

public enum KodProperty
{
    [XmlEnum("praw_pkdKod")]
    praw_pkdKod,

    [XmlEnum("lokpraw_pkdKod")]
    lokpraw_pkdKod,

    [XmlEnum("lokfiz_pkd_Kod")]
    lokfiz_pkd_Kod,

    [XmlEnum("fiz_pkd_Kod")]
    fiz_pkd_Kod,
}