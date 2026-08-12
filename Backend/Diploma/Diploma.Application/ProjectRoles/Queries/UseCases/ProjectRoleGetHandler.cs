using Diploma.Models.ProjectRoles;
using Diploma.Models.Projects;
using MediatR;

namespace Diploma.Application.ProjectRoles.Queries.UseCases;

public class ProjectRoleGetHandler : IRequestHandler<ProjectRoleGetHandler.Request, ProjectRoleQueryResult>
{
    public sealed record Request : IRequest<ProjectRoleQueryResult>
    {
        public Guid? PersonId { get; init; }
        public required ProjectQueryParameters Model { get; init; }
    }

    public Task<ProjectRoleQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}