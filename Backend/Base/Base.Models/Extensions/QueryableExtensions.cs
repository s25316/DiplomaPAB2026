// Ignore Spelling: Queryable
using Base.Models.Interfaces.Repositories;

namespace Base.Models.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, Pagination pagination) => query
        .Skip(pagination.ItemsPerPage * (pagination.Page - 1))
        .Take(pagination.ItemsPerPage);
}