using Base.Models.ValueObjects.Regony;
using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.PersonEmployments;

public sealed record PersonEmploymentCreateRequest
{
    [Required]
    public required Regon Regon { get; init; }

    [Required]
    public required string Position { get; init; }

    [Required]
    public required string Descrition { get; init; }

    [Required]
    public required DateOnly From { get; init; }

    public required DateOnly? To { get; init; }
}

public abstract record PersonEmploymentCreateResult
{
    public sealed record Success : PersonEmploymentCreateResult;
    public abstract record Failure : PersonEmploymentCreateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
        public sealed record NotFoundCompany(Regon Regon) : Failure;
        public sealed record InvalidCompanyDates(DateOnly? Start, DateOnly? End) : Failure;
    };
}