// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Podstawowa, Forma, Prawna, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaPrawna.Podstawowa;

public enum PodstawowaFormaPrawnaNazwaParameter
{
    [XmlEnum("praw_podstawowaFormaPrawna_Nazwa")]
    praw_podstawowaFormaPrawna_Nazwa,

    [XmlEnum("lokpraw_podstawowaFormaPrawna_Nazwa")]
    lokpraw_podstawowaFormaPrawna_Nazwa,
}