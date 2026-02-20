using Base.Models.Extensions;
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation.Repositories;

// TODO
public class ConnectionRepository(TerytDbContext context) : IConnectionRepository
{
    public async Task<Response<Connection>.ManyItems> GetAsync(ConnectionParameters parameters, CancellationToken cancellationToken = default)
    {
        var miejscowoscIds = parameters
            .Ids
            .Where(i => i.UlicaId is null)
            .Select(i => i.ToString());

        var connections = parameters
            .Ids
            .Where(i => i.UlicaId is not null)
            .Select(i => i.ToString());


        var responseConnection = new List<Connection>();

        var baseQueryMiejscowosci = context
            .Miejscowosci
            .AsNoTracking()
            .Where(i => miejscowoscIds.Contains(i.MiejscowoscCode));
        var miejscowosciTotalCount = await baseQueryMiejscowosci.CountAsync(cancellationToken);


        var baseQueryConnections = context
            .SimcUlics
            .AsNoTracking()
            .Where(i => connections.Contains(i.MiejscowoscCode + "." + i.UlicaCode));
        var connectionsTotalCount = await baseQueryConnections.CountAsync(cancellationToken);


        var totalCount = miejscowosciTotalCount + connectionsTotalCount;


        var dbConnections = await baseQueryMiejscowosci
            .Paginate(parameters.Pagination)
            .ToListAsync(cancellationToken);

        if (dbConnections.Count > 0)
        {
            responseConnection = dbConnections.Select(i => new Connection
            {
                MiejscowoscId = i.MiejscowoscCode,
                UlicaId = null,
            }).ToList();
        }

        if (responseConnection.Count >= parameters.Pagination.ItemsPerPage)
        {
            return Response.Prepare(responseConnection.Take(parameters.Pagination.ItemsPerPage), totalCount, )
        }

        var newPagination = new Pagination
        {
            ItemsPerPage = parameters.Pagination.ItemsPerPage,
            Page = 1,// ???
        };


        throw new NotImplementedException();
    }
}
