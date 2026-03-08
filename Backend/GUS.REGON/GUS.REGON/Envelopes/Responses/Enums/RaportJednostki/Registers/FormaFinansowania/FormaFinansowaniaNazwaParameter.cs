// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Forma, Finansowania, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaFinansowania;

public enum FormaFinansowaniaNazwaParameter
{
    [XmlEnum("praw_formaFinansowania_Nazwa")]
    praw_formaFinansowania_Nazwa,

    [XmlEnum("lokfiz_formaFinansowania_Nazwa")]
    lokfiz_formaFinansowania_Nazwa,

    [XmlEnum("lokpraw_formaFinansowania_Nazwa")]
    lokpraw_formaFinansowania_Nazwa,
}