// Ignore Spelling: Regon, Plugin, Enums, Typ, Jednostki
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace GUS.REGON.Models.Responses.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TypJednostki
{
    [XmlEnum("P")]
    P = 1,

    [XmlEnum("F")]
    F,

    [XmlEnum("LP")]
    LP,

    [XmlEnum("LF")]
    LF,
}