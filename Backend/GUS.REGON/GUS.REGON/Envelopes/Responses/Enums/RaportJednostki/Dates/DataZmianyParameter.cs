// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Zmiany
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Dates;

public enum DataZmianyParameter
{
    [XmlEnum("fiz_dataZaistnieniaZmianyDzialalnosci")]
    fiz_dataZaistnieniaZmianyDzialalnosci,

    [XmlEnum("lokpraw_dataZaistnieniaZmiany")]
    lokpraw_dataZaistnieniaZmiany,

    [XmlEnum("praw_dataZaistnieniaZmiany")]
    praw_dataZaistnieniaZmiany,

    [XmlEnum("lokfiz_dataZaistnieniaZmiany")]
    lokfiz_dataZaistnieniaZmiany,
}