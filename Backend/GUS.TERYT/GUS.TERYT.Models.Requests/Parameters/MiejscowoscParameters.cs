// Ignore Spelling: Miejscowosc, Gmina
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValueObjects.Gminy;
using GUS.TERYT.Models.Requests.ValueObjects.Miejscowosci;

namespace GUS.TERYT.Models.Requests.Parameters;

public class MiejscowoscParameters : BaseParameters<MiejscowoscId>
{
    public string? SearchText { get; init; } = null;
    public IList<MiejscowoscTypeId> TypeIds { get; init; } = [];
    public IList<GminaId> GminaIds { get; init; } = [];
}