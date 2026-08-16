using Diploma.Application.ProjectRoles.Queries.Interfaces;
using Diploma.Models.ProjectRoles;
using MediatR;

namespace Diploma.Application.ProjectRoles.Queries.UseCases;

public class ProjectRoleGetHandler(
    IProjectRoleQueryService queryService
    ) : IRequestHandler<ProjectRoleGetHandler.Request, ProjectRoleQueryResult>
{
    public sealed record Request : IRequest<ProjectRoleQueryResult>
    {
        public required Guid? PersonId { get; init; }
        public required ProjectRoleQueryParameters Model { get; init; }
    }


    public async Task<ProjectRoleQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var result = await queryService.GetAsync(
            request.PersonId,
            false,
            true,
            request.Model,
            cancellationToken);

        return new ProjectRoleQueryResult.Success(result);
    }
}