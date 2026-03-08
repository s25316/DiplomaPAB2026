// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Wpisu, Rejestru, Ewidencji
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Dates;

public enum DataWpisuDoRejestruEwidencjiParameter
{
    [XmlEnum("fizC_dataWpisuDoRejestruEwidencji")]
    fizC_dataWpisuDoRejestruEwidencji,

    [XmlEnum("fizP_dataWpisuDoRejestruEwidencji")]
    fizP_dataWpisuDoRejestruEwidencji,

    [XmlEnum("lokpraw_dataWpisuDoRejestruEwidencji")]
    lokpraw_dataWpisuDoRejestruEwidencji,

    [XmlEnum("lokfiz_dataWpisuDoRejestruEwidencji")]
    lokfiz_dataWpisuDoRejestruEwidencji,
}