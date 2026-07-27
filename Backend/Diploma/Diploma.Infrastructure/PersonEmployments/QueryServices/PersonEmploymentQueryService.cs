using Diploma.Application.PersonEmployments.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Infrastructure.QueryBuilders.Persons;
using Diploma.Models.PersonEmployments;
using Diploma.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.PersonEmployments.QueryServices;

public class PersonEmploymentQueryService(PersonEmploymentQueryBuilder builder) : IPersonEmploymentQueryService
{
    public async Task<Response<PersonEmploymentDto>> GetAsync(PersonId personId, PersonEmploymentQueryParameters parameters, CancellationToken cancellationToken = default)
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

        return new Response<PersonEmploymentDto>
        {
            Pagination = new ResponsePagination
            {
                Page = parameters.Pagination.Page,
                ItemsPerPage = parameters.Pagination.ItemsPerPage,
                TotalCount = totalCount,
            },
            Items = items.Select(i => new PersonEmploymentDto
            {
                EmploymentId = i.Root?.PersonEmploymentId ?? i.PersonEmploymentId,
                Regon = i.Regon,
                Position = i.Position,
                Description = i.Description,
                From = i.From,
                To = i.To,
            }).ToList(),
        };
    }
}