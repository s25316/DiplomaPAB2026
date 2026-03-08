// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Dzialalnosci
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses.Enums.RaportJednostki;

public enum DzialalnosciParameter
{
    [XmlEnum("praw_jednostekLokalnych")]
    praw_jednostekLokalnych,

    [XmlEnum("lokfiz_dzialalnosci")]
    lokfiz_dzialalnosci,

    [XmlEnum("lokpraw_dzialalnosci")]
    lokpraw_dzialalnosci,
}