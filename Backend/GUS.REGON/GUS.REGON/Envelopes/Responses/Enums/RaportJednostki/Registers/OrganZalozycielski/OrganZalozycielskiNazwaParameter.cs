// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Zalozycielski, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.OrganZalozycielski;

public enum OrganZalozycielskiNazwaParameter
{
    [XmlEnum("praw_organZalozycielski_Nazwa")]
    praw_organZalozycielski_Nazwa,

    [XmlEnum("lokpraw_organZalozycielski_Nazwa")]
    lokpraw_organZalozycielski_Nazwa,
}