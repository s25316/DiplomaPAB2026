// Ignore Spelling: Gmina
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Models.Responses;
using Mapper;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation.Repositories;

public class GminaTypeRepository(TerytDbContext context, IMapper mapper) : IGminaTypeRepository
{
    public async Task<IDictionary<string, Gmina.Type>> GetAsync(CancellationToken cancellationToken = default)
    {
        var dbItems = await context.GminaRodzaje.ToListAsync(cancellationToken);
        return dbItems.ToDictionary(k => k.GminaRodzCode, mapper.Map<Gmina.Type>);
    }
}
