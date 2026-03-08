// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Kod, Pocztowy
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters;

public enum KodPocztowyParameter
{
    [XmlEnum("fiz_adSiedzKodPocztowy")]
    fiz_adSiedzKodPocztowy,

    [XmlEnum("lokpraw_adSiedzKodPocztowy")]
    lokpraw_adSiedzKodPocztowy,

    [XmlEnum("praw_adSiedzKodPocztowy")]
    praw_adSiedzKodPocztowy,

    [XmlEnum("lokfiz_adSiedzKodPocztowy")]
    lokfiz_adSiedzKodPocztowy,
}