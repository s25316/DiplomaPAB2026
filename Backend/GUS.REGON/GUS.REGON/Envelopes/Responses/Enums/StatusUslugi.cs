// Ignore Spelling: Regon, Plugin, Enums, Uslugi
using System.Text.Json.Serialization;

namespace GUS.REGON.Models.Responses.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusUslugi
{
    UslugaNiedostepna = 0,
    UslugaDostepna = 1,
    PrzerwaTechniczna = 2,
}