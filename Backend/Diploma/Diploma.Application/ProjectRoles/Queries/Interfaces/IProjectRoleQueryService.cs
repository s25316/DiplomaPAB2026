using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.ProjectRoles;
using Diploma.Models.Shared;

namespace Diploma.Application.ProjectRoles.Queries.Interfaces;

public interface IProjectRoleQueryService
{
    Task<Response<ProjectRoleDto>> GetAsync(
        PersonId? personId,
        bool isPersonItems,
        bool? isVisible,
        ProjectRoleQueryParameters queryParameters,
        CancellationToken cancellationToken = default);
}