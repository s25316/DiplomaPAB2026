using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.Projects;

public sealed record ProjectCreateRequest
{
    [Required]
    public required string Title { get; init; }

    [Required]
    public required string Description { get; init; }
}

public abstract record ProjectCreateResult
{
    public sealed record Success : ProjectCreateResult;
    public abstract record Failure : ProjectCreateResult
    {
        public sealed record ProfileIsEmpty : Failure;
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}