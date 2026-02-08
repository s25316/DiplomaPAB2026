namespace Base.Models.Interfaces.Repositories;

public static class Response
{
    public static Response<TItem>.ManyItems Prepare<TItem>(
        IEnumerable<TItem> items,
        int totalCount,
        Pagination pagination
    ) => new(items, totalCount, pagination);
}

public abstract record Response<TItem>
{
    public record ManyItems(IEnumerable<TItem> Items, int TotalCount, Pagination Pagination) : Response<TItem>
    {
        public IEnumerable<TItem> Items { get; } = Items;
        public int TotalCount { get; } = TotalCount;
        public int Page { get; } = Pagination.Page;
        public int ItemsPerPage { get; } = Pagination.ItemsPerPage;
    }
}