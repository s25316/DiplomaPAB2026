// Ignore Spelling: Regon, Plugin, Enums, Sesji
using System.Text.Json.Serialization;

namespace GUS.REGON.Models.Responses.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusSesji
{
    SesjaNieIstnieje = 0,
    SesjaIstnieje = 1
}