using Base.Models.Interfaces.Repositories;
using GreenDonut.Data;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation.Repositories;

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
        int itemsPerPage = parameters.Pagination.ItemsPerPage;
        int pageIndex = parameters.Pagination.Page - 1;
        int skipGlobal = pageIndex * itemsPerPage;

        if (skipGlobal > totalCount)
        {
            return Response.Prepare<Connection>([], totalCount, parameters.Pagination);
        }

        int skipMiejscowosci = Math.Min(miejscowosciTotalCount, skipGlobal);
        int takeMiejscowosci = Math.Max(0, Math.Min(miejscowosciTotalCount - skipMiejscowosci, itemsPerPage));

        int skipConnections = Math.Max(0, skipGlobal - miejscowosciTotalCount);
        int takeConnections = itemsPerPage - takeMiejscowosci;

        if (takeMiejscowosci > 0)
        {
            var dbConnections = await baseQueryMiejscowosci
                .AsNoTracking()
                .Skip(skipMiejscowosci)
                .Take(takeMiejscowosci)
                .ToListAsync(cancellationToken);

            responseConnection = dbConnections.Select(i => new Connection
            {
                MiejscowoscId = i.MiejscowoscCode,
                UlicaId = null,
            }).ToList();
        }

        if (responseConnection.Count >= parameters.Pagination.ItemsPerPage)
        {
            return Response.Prepare(responseConnection, totalCount, parameters.Pagination);
        }

        if (takeConnections > 0)
        {
            var dbConnections = await baseQueryConnections
                .AsNoTracking()
                .Skip(skipConnections)
                .Take(takeConnections)
                .ToListAsync(cancellationToken);

            var responseConnection2 = dbConnections.Select(i => new Connection
            {
                MiejscowoscId = i.MiejscowoscCode,
                UlicaId = i.UlicaCode,
            }).ToList();
            responseConnection.AddRange(responseConnection2);
        }
        return Response.Prepare(responseConnection, totalCount, parameters.Pagination);
    }
}