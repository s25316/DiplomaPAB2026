namespace Base.Models;

public static class Response
{
    public static Response<TItem> Prepare<TItem>(
        IEnumerable<TItem> items,
        int totalCount,
        Pagination pagination
    ) => new(items, totalCount, pagination);
}

public class Response<TItem>(IEnumerable<TItem> items, int totalCount, Pagination pagination)
{
    public IEnumerable<TItem> Items { get; } = items;
    public int TotalCount { get; } = totalCount;
    public int Page { get; } = pagination.Page;
    public int ItemsPerPage { get; } = pagination.ItemsPerPage;
}