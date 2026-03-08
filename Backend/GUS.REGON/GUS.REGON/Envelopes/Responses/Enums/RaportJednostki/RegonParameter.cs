// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses.Enums.RaportJednostki;

public enum RegonParameter
{
    [XmlEnum("fiz_regon9")]
    fiz_regon9,

    [XmlEnum("lokpraw_regon14")]
    lokpraw_regon14,

    [XmlEnum("praw_regon14")]
    praw_regon14,

    [XmlEnum("lokfiz_regon14")]
    lokfiz_regon14,
}