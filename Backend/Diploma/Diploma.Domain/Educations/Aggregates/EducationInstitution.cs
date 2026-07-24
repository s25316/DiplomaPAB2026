using Diploma.Domain.Base.Aggregates;

namespace Diploma.Domain.Educations.Aggregates;

public sealed record EducationInstitutionId : BaseEntityId<Guid>
{
    public static implicit operator Guid(EducationInstitutionId value) => value.Value;
    public static implicit operator EducationInstitutionId(Guid value) => new() { Value = value };
}
public class EducationInstitution : BaseEntity<EducationInstitutionId>
{
    public required DateOnly StartDate { get; init; }
    public required DateOnly? LiquidationStartDate { get; init; } = null;
    public required DateOnly? LiquidationDate { get; init; } = null;
}