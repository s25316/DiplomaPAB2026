using RADON.Application.Interfaces.Base;
using RADON.Models.Institutions;
using RADON.Models.Institutions.Responses;

namespace RADON.Application.Interfaces.Institutions;

public interface IInstitutionRepository : IRepository<Institution, QueryParameters>
{
    Task<IEnumerable<Institution>> GetAllAsync(CancellationToken cancellationToken = default);
}