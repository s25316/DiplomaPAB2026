// Ignore Spelling: Wojewodztwo
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Models.Requests.ValueObjects.Wojewodztwa;

namespace GUS.TERYT.Models.Requests.Parameters;

public class WojewodztwoParameters : BaseParameters<WojewodztwoId>
{
    public string? SearchText { get; init; } = null;
    public WojewodztwoOrderBy OrderBy { get; init; } = WojewodztwoOrderBy.Id;
}

public enum WojewodztwoOrderBy
{
    Id,
    Name
}