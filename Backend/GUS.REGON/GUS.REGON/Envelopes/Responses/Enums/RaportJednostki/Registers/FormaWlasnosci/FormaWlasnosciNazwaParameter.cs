// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Forma, Wlasnosci, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaWlasnosci;

public enum FormaWlasnosciNazwaParameter
{
    [XmlEnum("praw_formaWlasnosci_Nazwa")]
    praw_formaWlasnosci_Nazwa,

    [XmlEnum("lokpraw_formaWlasnosci_Nazwa")]
    lokpraw_formaWlasnosci_Nazwa,
}