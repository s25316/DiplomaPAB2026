// Ignore Spelling: Miejscowosc
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Infrastructure.QueryBuilders;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Responses;
using Mapper;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation.Repositories;

public class MiejscowoscRepository(TerytDbContext context, IMapper mapper) : IMiejscowoscRepository
{
    public async Task<Response<Miejscowosc>.ManyItems> GetAsync(MiejscowoscParameters parameters, CancellationToken cancellationToken = default)
    {
        var builder = new MiejscowoscQueryBuilder(context)
            .WithSearchText(parameters.SearchText)
            .WithWojewodztwoIds(parameters.WojewodztwoIds)
            .WithPowiatIds(parameters.PowiatIds)
            .WithGminaIds(parameters.GminaIds)
            .WithUlicaIds(parameters.UlicaIds)
            .WithIds(parameters.Ids)
            .WithTypeIds(parameters.TypeIds);

        var baseQuery = builder.Build();
        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var query = builder
            .WithPagination(parameters.Pagination, parameters.Order, parameters.OrderBy)
            .Build();
        var dbItems = await query.ToListAsync(cancellationToken);
        var items = mapper.MapEnumerable<Miejscowosc>(dbItems);

        return Response.Prepare(items, totalCount, parameters.Pagination);
    }
}