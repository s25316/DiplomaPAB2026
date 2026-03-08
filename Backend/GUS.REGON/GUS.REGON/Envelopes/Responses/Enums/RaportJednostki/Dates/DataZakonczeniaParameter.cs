// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Zakonczenia
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Dates;

public enum DataZakonczeniaParameter
{
    [XmlEnum("fiz_dataZakonczeniaDzialalnosci")]
    fiz_dataZakonczeniaDzialalnosci,

    [XmlEnum("lokpraw_dataZakonczeniaDzialalnosci")]
    lokpraw_dataZakonczeniaDzialalnosci,

    [XmlEnum("praw_dataZakonczeniaDzialalnosci")]
    praw_dataZakonczeniaDzialalnosci,

    [XmlEnum("lokfiz_dataZakonczeniaDzialalnosci")]
    lokfiz_dataZakonczeniaDzialalnosci,
}