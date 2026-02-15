// Ignore Spelling: Powiat, Wojewodztwo
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValueObjects.Powiaty;
using GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;

namespace GUS.TERYT.Models.Requests.Parameters;

public class PowiatParameters : BaseParameters<PowiatId>
{
    public string? SearchText { get; init; } = null;
    public IList<int> TypeIds { get; init; } = [];
    public IList<WojewodztwoId> WojewodztwoIds { get; init; } = [];
}
