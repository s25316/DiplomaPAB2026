// Ignore Spelling: Ulica
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValueObjects.Ulicy;

namespace GUS.TERYT.Models.Requests.Parameters;

public class UlicaParameters : BaseParameters<UlicaId>
{
    public string? SearchText { get; init; } = null;
    public IList<int> TypeIds { get; init; } = [];
}