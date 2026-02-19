// Ignore Spelling: Ulica, Miejscowosc
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;
using GUS.TERYT.Models.Requests.ValueObjects.Ulicy;

namespace GUS.TERYT.Models.Requests.Parameters;

public class UlicaParameters : BaseParameters<UlicaId>
{
    public string? SearchText { get; init; } = null;
    public IList<MiejscowoscId> MiejscowoscIds { get; init; } = [];
    public IList<int> TypeIds { get; init; } = [];
    public UlicaOrderBy OrderBy { get; init; } = UlicaOrderBy.Id;
}

public enum UlicaOrderBy
{
    Id,
    Name,
}