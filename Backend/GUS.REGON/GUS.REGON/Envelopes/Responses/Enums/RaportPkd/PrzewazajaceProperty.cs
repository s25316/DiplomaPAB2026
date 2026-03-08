// Ignore Spelling: Regon, Plugin, Raport, Raporty, PKD, Enums, Przewazajace
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses.Enums.RaportPkd;

public enum PrzewazajaceProperty
{
    [XmlEnum("praw_pkdPrzewazajace")]
    praw_pkdPrzewazajace,

    [XmlEnum("lokpraw_pkdPrzewazajace")]
    lokpraw_pkdPrzewazajace,

    [XmlEnum("lokfiz_pkd_Przewazajace")]
    lokfiz_pkd_Przewazajace,

    [XmlEnum("fiz_pkd_Przewazajace")]
    fiz_pkd_Przewazajace,
}