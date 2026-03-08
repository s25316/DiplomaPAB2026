// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Numer, Nieruchomosci
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters;

public enum NumerNieruchomosciParameter
{
    [XmlEnum("fiz_adSiedzNumerNieruchomosci")]
    fiz_adSiedzNumerNieruchomosci,

    [XmlEnum("lokpraw_adSiedzNumerNieruchomosci")]
    lokpraw_adSiedzNumerNieruchomosci,

    [XmlEnum("praw_adSiedzNumerNieruchomosci")]
    praw_adSiedzNumerNieruchomosci,

    [XmlEnum("lokfiz_adSiedzNumerNieruchomosci")]
    lokfiz_adSiedzNumerNieruchomosci,
}