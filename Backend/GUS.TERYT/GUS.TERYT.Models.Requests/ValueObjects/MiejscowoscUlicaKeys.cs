// Ignore Spelling: Miejscowosc, Ulica, MiejscowoscUlica
using GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;
using GUS.TERYT.Models.Requests.ValueObjects.Ulicy;

namespace GUS.TERYT.Models.Requests.ValueObjects;

public sealed record MiejscowoscUlicaKeys(MiejscowoscId MiejscowoscId, UlicaId? UlicaId);