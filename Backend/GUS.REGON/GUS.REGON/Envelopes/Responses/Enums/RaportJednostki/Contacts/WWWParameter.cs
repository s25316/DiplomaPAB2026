// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Contacts;

public enum WWWParameter
{
    [XmlEnum("fiz_adresStronyinternetowej")]
    fiz_adresStronyinternetowej,

    [XmlEnum("praw_adresStronyinternetowej")]
    praw_adresStronyinternetowej,

    [XmlEnum("fizC_adresStronyInternetowej")]
    fizC_adresStronyInternetowej,
}