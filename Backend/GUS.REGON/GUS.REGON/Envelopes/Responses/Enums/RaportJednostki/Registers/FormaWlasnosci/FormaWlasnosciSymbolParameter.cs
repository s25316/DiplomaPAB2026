// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Forma, Wlasnosci,
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaWlasnosci;

public enum FormaWlasnosciSymbolParameter
{
    [XmlEnum("praw_formaWlasnosci_Symbol")]
    praw_formaWlasnosci_Symbol,

    [XmlEnum("lokpraw_formaWlasnosci_Symbol")]
    lokpraw_formaWlasnosci_Symbol,
}