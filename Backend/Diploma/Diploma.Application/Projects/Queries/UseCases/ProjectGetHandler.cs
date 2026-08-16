using Diploma.Application.Projects.Queries.Interfaces;
using Diploma.Models.Projects;
using MediatR;

namespace Diploma.Application.Projects.Queries.UseCases;

public class ProjectGetHandler(
    IProjectQueryService queryService
    ) : IRequestHandler<ProjectGetHandler.Request, ProjectQueryResult>
{
    public sealed record Request : IRequest<ProjectQueryResult>
    {
        public Guid? PersonId { get; init; }
        public required ProjectQueryParameters Model { get; init; }
    }


    public async Task<ProjectQueryResult> Handle(ProjectGetHandler.Request request, CancellationToken cancellationToken)
    {
        var result = await queryService.GetAsync(
            request.PersonId,
            true,
            false,
            request.Model,
            cancellationToken);

        return new ProjectQueryResult.Success(result);
    }
}