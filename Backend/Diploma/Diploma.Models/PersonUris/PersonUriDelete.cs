namespace Diploma.Models.PersonUris;

public abstract record PersonUriDeleteResult
{
    public sealed record Success : PersonUriDeleteResult;
    public abstract record Failure : PersonUriDeleteResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
    };
}