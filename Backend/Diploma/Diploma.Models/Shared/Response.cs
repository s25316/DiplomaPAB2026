using System.ComponentModel;

namespace Diploma.Models.Shared;

public record ResponsePagination
{
    [DefaultValue(1)]
    public required int Page { get; init; }

    [DefaultValue(100)]
    public required int ItemsPerPage { get; init; }

    [DefaultValue(0)]
    public required int TotalCount { get; init; }
}

public sealed class Response<TItem>
    where TItem : class
{
    public required IList<TItem> Items { get; init; } = [];

    public required ResponsePagination Pagination { get; init; }
}