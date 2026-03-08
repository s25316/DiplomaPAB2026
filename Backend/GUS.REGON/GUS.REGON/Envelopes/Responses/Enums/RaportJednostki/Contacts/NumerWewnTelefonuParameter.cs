// Ignore Spelling: Regon, Plugin, Raport, Raporty, Jednostki, Enums, Numer, Wewn, Telefonu
using System.Xml.Serialization;

namespace GUS.REGON.Envelopes.Responses.Enums.RaportJednostki.Contacts;

public enum NumerWewnTelefonuParameter
{
    [XmlEnum("fiz_numerWewnetrznyTelefonu")]
    fiz_numerWewnetrznyTelefonu,

    [XmlEnum("praw_numerWewnetrznyTelefonu")]
    praw_numerWewnetrznyTelefonu,

    [XmlEnum("fizC_numerWewnetrznyTelefonu")]
    fizC_numerWewnetrznyTelefonu,
}