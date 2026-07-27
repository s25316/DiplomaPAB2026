using Diploma.Application.PersonUris.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Persons;
using Diploma.Models.PersonUris;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.PersonUris.QueryServices;

public class PersonUriQueryService(PersonUriQueryBuilder builder) : IPersonUriQueryService
{
    public async Task<Response<PersonUriDto>> GetAsync(
        PersonId personId,
        PersonUriQueryParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var baseQuery = builder
            .WithPersonId(personId)
            .Build();

        var query = builder
            .WithOrderBy(
                parameters.Order,
                parameters.OrderBy,
                parameters.Pagination
            ).Build();

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        var items = await query.ToListAsync(cancellationToken);

        return new Response<PersonUriDto>
        {
            Pagination = new ResponsePagination
            {
                Page = parameters.Pagination.Page,
                ItemsPerPage = parameters.Pagination.ItemsPerPage,
                TotalCount = totalCount,
            },
            Items = items.Select(i => new PersonUriDto
            {
                UriId = i.Root?.PersonUriId ?? i.PersonUriId,
                Uri = i.Uri,
                Name = i.Name,
                Description = i.Description,
                CreatedAt = i.Root?.PersonEvent.CreatedAt ?? i.PersonEvent.CreatedAt,
            }).ToList(),
        };
    }
}