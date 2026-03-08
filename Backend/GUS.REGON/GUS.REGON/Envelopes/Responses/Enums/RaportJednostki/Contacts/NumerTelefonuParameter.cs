// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Numer, Telefonu
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Contacts;

public enum NumerTelefonuParameter
{
    [XmlEnum("fiz_numerTelefonu")]
    fiz_numerTelefonu,

    [XmlEnum("praw_numerTelefonu")]
    praw_numerTelefonu,

    [XmlEnum("fizC_numerTelefonu")]
    fizC_numerTelefonu,
}