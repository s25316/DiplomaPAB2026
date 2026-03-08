// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Adres
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Contacts;

public enum AdresEmailParameter
{
    [XmlEnum("fiz_adresEmail")]
    fiz_adresEmail,

    [XmlEnum("praw_adresEmail")]
    praw_adresEmail,

    [XmlEnum("fizC_adresEmail")]
    fizC_adresEmail,

    [XmlEnum("fiz_adresEmail2")]
    fiz_adresEmail2,

    [XmlEnum("praw_adresEmail2")]
    praw_adresEmail2,
}