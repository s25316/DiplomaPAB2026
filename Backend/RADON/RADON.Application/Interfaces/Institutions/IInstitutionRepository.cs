using RADON.Models.Institutions.Responses;

namespace RADON.Application.Interfaces.Institutions;

public interface IInstitutionRepository
{
    Task CreateOrUpdateAsync(IEnumerable<Institution> items, CancellationToken cancellationToken = default);
}