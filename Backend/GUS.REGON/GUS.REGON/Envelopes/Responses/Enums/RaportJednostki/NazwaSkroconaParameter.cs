// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Nazwa, Skrocona
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses.Enums.RaportJednostki;

public enum NazwaSkroconaParameter
{
    [XmlEnum("fiz_nazwaSkrocona")]
    fiz_nazwaSkrocona,

    [XmlEnum("praw_nazwaSkrocona")]
    praw_nazwaSkrocona,
}