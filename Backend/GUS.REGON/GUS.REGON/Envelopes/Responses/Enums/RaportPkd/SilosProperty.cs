// Ignore Spelling: Regon, Plugin, Raport, Raporty, PKD, Enums
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses.Enums.RaportPkd;

public enum SilosProperty
{
    [XmlEnum("fiz_Silos_Symbol")]
    fiz_Silos_Symbol,

    [XmlEnum("lokfiz_Silos_Symbol")]
    lokfiz_Silos_Symbol,
}