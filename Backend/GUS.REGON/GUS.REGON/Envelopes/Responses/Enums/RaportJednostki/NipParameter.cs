// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses.Enums.RaportJednostki;

public enum NipParameter
{
    [XmlEnum("lokpraw_nip")]
    lokpraw_nip,

    [XmlEnum("praw_nip")]
    praw_nip,
}