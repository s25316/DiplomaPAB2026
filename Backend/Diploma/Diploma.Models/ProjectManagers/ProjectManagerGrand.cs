using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.ProjectManagers;

public sealed record ProjectManagerGrandRequest
{
    [Required]
    public required Guid PersonId { get; init; }

    [Required]
    [Range(1, 5)]
    public required int RoleId { get; init; }
}

public abstract record ProjectManagerGrandResult
{
    public sealed record Success : ProjectManagerGrandResult;
    public abstract record Failure : ProjectManagerGrandResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}