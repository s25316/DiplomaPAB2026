using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.PersonUris;

public sealed record PersonUriUpdateRequest
{
    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Description { get; init; }
}

public abstract record PersonUriUpdateResult
{
    public sealed record Success : PersonUriUpdateResult;
    public abstract record Failure : PersonUriUpdateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}