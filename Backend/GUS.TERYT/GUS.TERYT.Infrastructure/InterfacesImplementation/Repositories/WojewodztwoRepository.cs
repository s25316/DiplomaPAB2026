// Ignore Spelling: Wojewodztwo
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Infrastructure.QueryBuilders;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Responses;
using Mapper;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation.Repositories;

public class WojewodztwoRepository(TerytDbContext context, IMapper mapper) : IWojewodztwoRepository
{
    public async Task<Response<Wojewodztwo>.ManyItems> GetAsync(
        WojewodztwoParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var builder = new WojewodztwoQueryBuilder(context)
            .WithSearchText(parameters.SearchText)
            .WithIds(parameters.Ids);

        var baseQuery = builder.Build();
        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var query = builder
            .WithPagination(parameters.Pagination, parameters.Order, parameters.OrderBy)
            .Build();
        var dbItems = await query.ToListAsync(cancellationToken);
        var items = mapper.MapEnumerable<Wojewodztwo>(dbItems);

        return Response.Prepare(items, totalCount, parameters.Pagination);
    }
}