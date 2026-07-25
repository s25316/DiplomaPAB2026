using Base.Models.ValueObjects.Regony;
using Diploma.Domain.Base.Aggregates;

namespace Diploma.Domain.Companies.Aggregates;

public sealed record CompanyId : BaseEntityId<Regon>
{
    public static implicit operator Regon(CompanyId value) => value.Value;
    public static implicit operator CompanyId(Regon value) => new() { Value = value };
}
public class Company : BaseEntity<CompanyId>
{
    public required DateOnly? StartDate { get; init; } = null;
    public required DateOnly? EndDate { get; init; } = null;
}