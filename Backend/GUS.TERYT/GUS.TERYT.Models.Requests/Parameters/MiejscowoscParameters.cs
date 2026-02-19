// Ignore Spelling: Miejscowosc, Gmina, Wojewodztwo, Powiat, Ulica
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValueObjects.Gminy;
using GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;
using GUS.TERYT.Models.Requests.ValueObjects.Powiaty;
using GUS.TERYT.Models.Requests.ValueObjects.Ulicy;
using GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;

namespace GUS.TERYT.Models.Requests.Parameters;

public class MiejscowoscParameters : BaseParameters<MiejscowoscId>
{
    public string? SearchText { get; init; } = null;
    public IList<WojewodztwoId> WojewodztwoIds { get; init; } = [];
    public IList<PowiatId> PowiatIds { get; init; } = [];
    public IList<GminaId> GminaIds { get; init; } = [];
    public IList<UlicaId> UlicaIds { get; init; } = [];
    public IList<MiejscowoscTypeId> TypeIds { get; init; } = [];
    public MiejscowoscOrderBy OrderBy { get; init; } = MiejscowoscOrderBy.Id;
}

public enum MiejscowoscOrderBy
{
    Id,
    Name,
}