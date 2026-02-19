// Ignore Spelling: Miejscowosc
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Models.Responses;
using Mapper;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation.Repositories;

public class MiejscowoscTypeRepository(TerytDbContext context, IMapper mapper) : IMiejscowoscTypeRepository
{
    public async Task<IDictionary<string, Miejscowosc.Type>> GetAsync(CancellationToken cancellationToken = default)
    {
        var dbItems = await context.MiejscowoscRodzaje
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return dbItems.ToDictionary(k => k.TypeCode, mapper.Map<Miejscowosc.Type>);
    }
}
