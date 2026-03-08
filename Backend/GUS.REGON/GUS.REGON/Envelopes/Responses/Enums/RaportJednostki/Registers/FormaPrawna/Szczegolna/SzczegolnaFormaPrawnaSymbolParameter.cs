// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Szczegolna, Forma, Prawna, 
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaPrawna.Szczegolna;

public enum SzczegolnaFormaPrawnaSymbolParameter
{
    [XmlEnum("praw_szczegolnaFormaPrawna_Symbol")]
    praw_szczegolnaFormaPrawna_Symbol,

    [XmlEnum("lokpraw_szczegolnaFormaPrawna_Symbol")]
    lokpraw_szczegolnaFormaPrawna_Symbol,
}