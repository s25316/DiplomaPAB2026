// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Nietypowe, Miejsce, Lokalizacji
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters;

public enum NietypoweMiejsceLokalizacjiParameter
{
    [XmlEnum("fiz_adSiedzNietypoweMiejsceLokalizacji")]
    fiz_adSiedzNietypoweMiejsceLokalizacji,

    [XmlEnum("lokpraw_adSiedzNietypoweMiejsceLokalizacji")]
    lokpraw_adSiedzNietypoweMiejsceLokalizacji,

    [XmlEnum("praw_adSiedzNietypoweMiejsceLokalizacji")]
    praw_adSiedzNietypoweMiejsceLokalizacji,

    [XmlEnum("lokfiz_adSiedzNietypoweMiejsceLokalizacji")]
    lokfiz_adSiedzNietypoweMiejsceLokalizacji,
}