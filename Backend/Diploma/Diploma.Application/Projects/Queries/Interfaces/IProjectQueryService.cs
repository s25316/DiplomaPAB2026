using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Projects;
using Diploma.Models.Shared;

namespace Diploma.Application.Projects.Queries.Interfaces;

public interface IProjectQueryService
{
    Task<Response<ProjectDto>> GetAsync(
        PersonId? personId,
        bool? isVisible,
        bool isPersonItems,
        ProjectQueryParameters queryParameters,
        CancellationToken cancellationToken = default);
}