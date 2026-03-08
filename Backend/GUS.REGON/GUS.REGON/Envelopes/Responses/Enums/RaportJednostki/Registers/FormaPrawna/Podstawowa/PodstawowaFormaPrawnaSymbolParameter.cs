// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Podstawowa, Forma, Prawna, 
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaPrawna.Podstawowa;

public enum PodstawowaFormaPrawnaSymbolParameter
{
    [XmlEnum("praw_podstawowaFormaPrawna_Symbol")]
    praw_podstawowaFormaPrawna_Symbol,

    [XmlEnum("lokpraw_podstawowaFormaPrawna_Symbol")]
    lokpraw_podstawowaFormaPrawna_Symbol,
}