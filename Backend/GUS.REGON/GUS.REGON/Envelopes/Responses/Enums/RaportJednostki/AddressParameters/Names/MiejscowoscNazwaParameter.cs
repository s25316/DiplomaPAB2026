// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Miejscowosc, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.AddressParameters.Names;

public enum MiejscowoscNazwaParameter
{
    [XmlEnum("fiz_adSiedzMiejscowosc_Nazwa")]
    fiz_adSiedzMiejscowosc_Nazwa,

    [XmlEnum("lokpraw_adSiedzMiejscowosc_Nazwa")]
    lokpraw_adSiedzMiejscowosc_Nazwa,

    [XmlEnum("praw_adSiedzMiejscowosc_Nazwa")]
    praw_adSiedzMiejscowosc_Nazwa,

    [XmlEnum("lokfiz_adSiedzMiejscowosc_Nazwa")]
    lokfiz_adSiedzMiejscowosc_Nazwa,
}