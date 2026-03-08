// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Zawieszenia
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Dates;

public enum DataZawieszeniaParameter
{
    [XmlEnum("fiz_dataZawieszeniaDzialalnosci")]
    fiz_dataZawieszeniaDzialalnosci,

    [XmlEnum("lokpraw_dataZawieszeniaDzialalnosci")]
    lokpraw_dataZawieszeniaDzialalnosci,

    [XmlEnum("praw_dataZawieszeniaDzialalnosci")]
    praw_dataZawieszeniaDzialalnosci,

    [XmlEnum("lokfiz_dataZawieszeniaDzialalnosci")]
    lokfiz_dataZawieszeniaDzialalnosci,
}