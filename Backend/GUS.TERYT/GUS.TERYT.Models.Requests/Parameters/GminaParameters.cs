// Ignore Spelling: Powiat, Wojewodztwo, Gmina, Rodz
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValueObjects.Gminy;
using GUS.TERYT.Models.Requests.ValueObjects.Powiaty;
using GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;

namespace GUS.TERYT.Models.Requests.Parameters;

public class GminaParameters : BaseParameters<GminaId>
{
    public string? SearchText { get; init; } = null;
    public IList<WojewodztwoId> WojewodztwoIds { get; init; } = [];
    public IList<PowiatId> PowiatIds { get; init; } = [];
    public IList<GminaTypeId> TypeIds { get; init; } = [];
    public GminaOrderBy OrderBy { get; init; } = GminaOrderBy.Id;
}

public enum GminaOrderBy
{
    Id,
    Name,
}