using System.ComponentModel.DataAnnotations;

namespace Diploma.Models.PersonUris;

public sealed record PersonUriCreateRequest
{
    [Required]
    [Url]
    public required string Uri { get; init; }

    [Required]
    public required string Name { get; init; }

    [Required]
    public required string Description { get; init; }
}

public abstract record PersonUriCreateResult
{
    public sealed record Success : PersonUriCreateResult;
    public abstract record Failure : PersonUriCreateResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}