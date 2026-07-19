using Diploma.Models.Shared;
using System.Text.RegularExpressions;

namespace Diploma.Infrastructure.QueryBuilders.Base;

public abstract class BaseQueryBuilder<TItem>
{
    protected IQueryable<TItem> query;


    protected BaseQueryBuilder(IQueryable<TItem> query)
    {
        this.query = query;
    }


    protected virtual void With(Func<IQueryable<TItem>, IQueryable<TItem>> func) => query = func(query);
    public virtual IQueryable<TItem> Build() => query;


    protected virtual IEnumerable<string> SplitToUpperInvariant(string? searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return [];

        searchText = searchText.ToUpperInvariant();
        searchText = Regex.Replace(searchText, @"[^a-zA-Z0-9]", " ");
        return searchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }

    protected virtual void Paginate(QueryParametersPagination pagination)
    {
        With(query => query
           .Skip(pagination.ItemsPerPage * (pagination.Page - 1))
           .Take(pagination.ItemsPerPage));
    }
}