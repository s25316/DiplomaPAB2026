// Ignore Spelling: Powiat, Wojewodztwo, Gmina, Rodz
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValueObjects.Gminy;
using GUS.TERYT.Models.Requests.ValueObjects.Powiaty;

namespace GUS.TERYT.Models.Requests.Parameters;

public class GminaParameters : BaseParameters<GminaId>
{
    public string? SearchText { get; init; } = null;
    public IList<GminaTypeId> TypeIds { get; init; } = [];
    public IList<PowiatId> PowiatIds { get; init; } = [];
}
