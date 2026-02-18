// Ignore Spelling: Gmina, Powiat, Wojewodztwo
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Infrastructure.QueryBuilders;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Responses;
using Mapper;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation.Repositories;

public class GminaRepository(TerytDbContext context, IMapper mapper) : IGminaRepository
{
    public async Task<Response<Gmina>> GetAsync(GminaParameters parameters, CancellationToken cancellationToken = default)
    {
        var builder = new GminaQueryBuilder(context)
            .WithSearchText(parameters.SearchText)
            .WithWojewodztwoIds(parameters.WojewodztwoIds)
            .WithPowiatIds(parameters.PowiatIds)
            .WithIds(parameters.Ids)
            .WithTypeIds(parameters.TypeIds);

        var baseQuery = builder.Build();
        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var query = builder
            .WithPagination(parameters.Pagination, parameters.Order, parameters.OrderBy)
            .Build();
        var dbItems = await query.ToListAsync(cancellationToken);
        var items = mapper.MapEnumerable<Gmina>(dbItems);

        return Response.Prepare(items, totalCount, parameters.Pagination);
    }
}
