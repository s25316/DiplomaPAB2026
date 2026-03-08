// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Wpisu
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Dates;

public enum DataWpisuParameter
{
    [XmlEnum("fiz_dataWpisuDoREGONDzialalnosci")]
    fiz_dataWpisuDoREGONDzialalnosci,

    [XmlEnum("lokpraw_dataWpisuDoREGON")]
    lokpraw_dataWpisuDoREGON,

    [XmlEnum("praw_dataWpisuDoREGON")]
    praw_dataWpisuDoREGON,

    [XmlEnum("lokfiz_dataWpisuDoREGON")]
    lokfiz_dataWpisuDoREGON,
}