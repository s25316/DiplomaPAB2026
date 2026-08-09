using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Projects;

public sealed record ProjectUpdateRequest
{
    [Required]
    public required string Title { get; init; }

    [Required]
    public required string Description { get; init; }

    [Required]
    public required bool IsVisible { get; init; }
}

public abstract record ProjectUpdateResult
{
    public sealed record Success : ProjectUpdateResult;
    public abstract record Failure : ProjectUpdateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}