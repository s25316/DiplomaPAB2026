// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Szczegolna, Forma, Prawna, Nazwa
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Registers.FormaPrawna.Szczegolna;

public enum SzczegolnaFormaPrawnaNazwaParameter
{
    [XmlEnum("praw_szczegolnaFormaPrawna_Nazwa")]
    praw_szczegolnaFormaPrawna_Nazwa,

    [XmlEnum("lokpraw_szczegolnaFormaPrawna_Nazwa")]
    lokpraw_szczegolnaFormaPrawna_Nazwa,
}