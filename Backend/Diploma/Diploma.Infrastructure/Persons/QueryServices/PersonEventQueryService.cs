using Diploma.Application.Persons.Queries.Profile.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Persons;
using Diploma.Models.PersonEvents;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;
using SharedPersonEvent = Diploma.Shared.PersonEvents.PersonEvent;

namespace Diploma.Infrastructure.Persons.QueryServices;

public class PersonEventQueryService(
    PersonEventQueryBuilder builder
    ) : IPersonEventQueryService
{
    public async Task<Response<PersonEventDto>> GetAsync(
        PersonId personId,
        PersonEventQueryParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var baseQuery = builder
            .WithPersonId(personId)
            .Build();

        var query = builder
            .WithOrderBy(parameters.Pagination)
            .Build();

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        var items = await query.ToListAsync(cancellationToken);

        return new Response<PersonEventDto>
        {
            Items = items.Select(i => new PersonEventDto
            {
                PersonEventId = i.PersonEventId,
                Code = SharedPersonEvent.FromId(i.PersonEventTypeId).Id,
                Name = SharedPersonEvent.FromId(i.PersonEventTypeId).Name,
                CreatedAt = i.CreatedAt,
            }).ToList(),
            Pagination = new ResponsePagination
            {
                TotalCount = totalCount,
                ItemsPerPage = parameters.Pagination.ItemsPerPage,
                Page = parameters.Pagination.Page,
            },
        };
    }
}