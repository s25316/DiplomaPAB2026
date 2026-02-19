// Ignore Spelling: Ulica
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Database;
using GUS.TERYT.Models.Responses;
using Mapper;
using Microsoft.EntityFrameworkCore;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation.Repositories;

public class UlicaTypeRepository(TerytDbContext context, IMapper mapper) : IUlicaTypeRepository
{
    public async Task<IDictionary<int, Ulica.Type>> GetAsync(CancellationToken cancellationToken = default)
    {
        var dbItems = await context.UlicTypes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return dbItems.ToDictionary(k => k.TypeCode, mapper.Map<Ulica.Type>);
    }
}