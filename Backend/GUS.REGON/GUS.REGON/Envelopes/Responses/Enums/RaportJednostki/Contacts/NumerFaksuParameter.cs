// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Numer, Faksu
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Contacts;

public enum NumerFaksuParameter
{
    [XmlEnum("fiz_numerFaksu")]
    fiz_numerFaksu,

    [XmlEnum("praw_numerFaksu")]
    praw_numerFaksu,

    [XmlEnum("fizC_numerFaksu")]
    fizC_numerFaksu,
}