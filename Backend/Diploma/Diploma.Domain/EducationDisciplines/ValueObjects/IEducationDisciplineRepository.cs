namespace Diploma.Domain.EducationDisciplines.ValueObjects;

public interface IEducationDisciplineRepository
{
    Task<IDictionary<string, EducationDiscipline>> GetAsync(
        CancellationToken cancellationToken = default);
}