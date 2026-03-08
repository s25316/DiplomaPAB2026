// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Rozpoczecia
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Dates;

public enum DataRozpoczeciaParameter
{
    [XmlEnum("fiz_dataRozpoczeciaDzialalnosci")]
    fiz_dataRozpoczeciaDzialalnosci,

    [XmlEnum("lokpraw_dataRozpoczeciaDzialalnosci")]
    lokpraw_dataRozpoczeciaDzialalnosci,

    [XmlEnum("praw_dataRozpoczeciaDzialalnosci")]
    praw_dataRozpoczeciaDzialalnosci,

    [XmlEnum("lokfiz_dataRozpoczeciaDzialalnosci")]
    lokfiz_dataRozpoczeciaDzialalnosci,
}