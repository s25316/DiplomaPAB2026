using Base.Models.ValueObjects.Regony;
using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.PersonEmployments;

public sealed record PersonEmploymentUpdateRequest
{
    [Required]
    public required string Position { get; init; }

    [Required]
    public required string Descrition { get; init; }

    [Required]
    public required DateOnly From { get; init; }

    public required DateOnly? To { get; init; }
}

public abstract record PersonEmploymentUpdateResult
{
    public sealed record Success : PersonEmploymentUpdateResult;
    public abstract record Failure : PersonEmploymentUpdateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
        public sealed record NotFoundCompany(Regon Regon) : Failure;
        public sealed record InvalidCompanyDates(DateOnly? Start, DateOnly? End) : Failure;
    };
}