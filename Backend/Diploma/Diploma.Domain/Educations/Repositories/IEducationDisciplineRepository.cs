using Diploma.Domain.Educations.ValueObjects;

namespace Diploma.Domain.Educations.Repositories;

public interface IEducationDisciplineRepository
{
    IDictionary<string, EducationDiscipline> GetAsync(CancellationToken cancellationToken = default);
}