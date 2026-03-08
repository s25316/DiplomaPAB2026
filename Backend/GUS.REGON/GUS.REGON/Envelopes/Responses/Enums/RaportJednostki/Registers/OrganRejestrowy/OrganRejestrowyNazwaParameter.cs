// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Rejestrowy, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.OrganRejestrowy;

public enum OrganRejestrowyNazwaParameter
{
    [XmlEnum("fizC_OrganRejestrowy_Nazwa")]
    fizC_OrganRejestrowy_Nazwa,

    [XmlEnum("fizP_OrganRejestrowy_Nazwa")]
    fizP_OrganRejestrowy_Nazwa,

    [XmlEnum("lokpraw_organRejestrowy_Nazwa")]
    lokpraw_organRejestrowy_Nazwa,

    [XmlEnum("praw_organRejestrowy_Nazwa")]
    praw_organRejestrowy_Nazwa,

    [XmlEnum("lokfiz_organRejestrowy_Nazwa")]
    lokfiz_organRejestrowy_Nazwa,
}