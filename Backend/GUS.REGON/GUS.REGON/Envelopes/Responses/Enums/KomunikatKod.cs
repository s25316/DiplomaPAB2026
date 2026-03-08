// Ignore Spelling: Regon, Plugin, Enums, Komunikat, Kod
using System.Text.Json.Serialization;

namespace GUS.REGON.Models.Responses.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum KomunikatKod
{
    KodCaptcha = 1,
    DaneSzukajWieleIdentyfikatorow = 2,
    NieZnalezionoPodmiotów = 4,
    BrakUprawnienDoRaportu = 5,
    BrakSesji = 7,
}