using Diploma.Application.ProjectRoles.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.ProjectRoles;
using MediatR;

namespace Diploma.Application.ProjectRoles.Queries.UseCases;

public class ProjectRolePersonGetHandler(
    IPersonRepository personRepository,
    IProjectRoleQueryService queryService
    ) : IRequestHandler<ProjectRolePersonGetHandler.Request, ProjectRoleQueryResult>
{
    public sealed record Request : IRequest<ProjectRoleQueryResult>
    {
        public Guid PersonId { get; init; }
        public required ProjectRoleQueryParameters Model { get; init; }
    }


    public async Task<ProjectRoleQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectRoleQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectRoleQueryResult.Failure.ProfileInactive();

        var result = await queryService.GetAsync(
            request.PersonId,
            false,
            null,
            request.Model,
            cancellationToken);

        return new ProjectRoleQueryResult.Success(result);
    }
}