// Ignore Spelling: Powiat
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Models.Responses;
using Mapper;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation.Repositories;

public class PowiatTypeRepository(TerytDbContext context, IMapper mapper) : IPowiatTypeRepository
{
    public async Task<IDictionary<int, Powiat.Type>> GetAsync(CancellationToken cancellationToken = default)
    {
        var dbItems = await context.PowiatTypes.ToListAsync(cancellationToken);
        return dbItems.ToDictionary(k => k.TypeCode, mapper.Map<Powiat.Type>);
    }
}