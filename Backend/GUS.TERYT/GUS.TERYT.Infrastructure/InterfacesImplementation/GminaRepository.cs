// Ignore Spelling: Gmina
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Responses;

namespace GUS.TERYT.Infrastructure.InterfacesImplementation;

public class GminaRepository : IGminaRepository
{
    public Task<Response<Gmina>> GetAsync(GminaParameters parameters, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
