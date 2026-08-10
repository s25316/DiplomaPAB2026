using Diploma.Application.Projects.Queries.Interfaces;
using Diploma.Models.Projects;
using MediatR;

namespace Diploma.Application.Projects.Queries.UseCases;

public class ProjectGetHandler(
    IProjectQueryService projectQueryService
    ) : IRequestHandler<ProjectGetHandler.Request, ProjectQueryResult>
{
    public sealed record Request : IRequest<ProjectQueryResult>
    {
        public required ProjectQueryParameters Model { get; init; }
    }

    public async Task<ProjectQueryResult> Handle(ProjectGetHandler.Request request, CancellationToken cancellationToken)
    {
        var result = await projectQueryService.GetAsync(
            null,
            true,
            request.Model,
            cancellationToken);

        return new ProjectQueryResult.Success(result);
    }
}