namespace RADON.Models.Shared;

public record ResponsePagination
{
    public required int Page { get; init; }
    public required int ItemsPerPage { get; init; }
    public required int TotalCount { get; init; }
}

public sealed class Response<TItem>
    where TItem : class
{
    public required IList<TItem> Items { get; init; } = [];
    public required ResponsePagination Pagination { get; init; }
}