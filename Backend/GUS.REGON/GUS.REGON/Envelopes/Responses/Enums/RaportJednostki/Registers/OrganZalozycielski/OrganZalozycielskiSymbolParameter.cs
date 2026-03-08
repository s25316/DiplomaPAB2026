// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Zalozycielski
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.OrganZalozycielski;

public enum OrganZalozycielskiSymbolParameter
{
    [XmlEnum("praw_organZalozycielski_Symbol")]
    praw_organZalozycielski_Symbol,

    [XmlEnum("lokpraw_organZalozycielski_Symbol")]
    lokpraw_organZalozycielski_Symbol,
}