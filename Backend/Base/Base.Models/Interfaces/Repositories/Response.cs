namespace Base.Models.Interfaces.Repositories;

public static class Response
{
    public static Response<TItem>.ManyItems Prepare<TItem>(
        IEnumerable<TItem> items,
        int totalCount,
        Pagination pagination
    ) => new(items, totalCount, pagination.Page, pagination.ItemsPerPage);
}

public abstract record Response<TItem>
{
    public record ManyItems(IEnumerable<TItem> Items, int TotalCount, int Page, int ItemsPerPage) : Response<TItem>;
}