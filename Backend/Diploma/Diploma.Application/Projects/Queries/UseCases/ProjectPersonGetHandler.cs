using Diploma.Application.Projects.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Projects;
using MediatR;

namespace Diploma.Application.Projects.Queries.UseCases;

public class ProjectPersonGetHandler(
    IPersonRepository personRepository,
    IProjectQueryService queryService
    ) : IRequestHandler<ProjectPersonGetHandler.Request, ProjectQueryResult>
{
    public sealed record Request : IRequest<ProjectQueryResult>
    {
        public required Guid PersonId { get; init; }
        public required ProjectQueryParameters Model { get; init; }
    }

    public async Task<ProjectQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new ProjectQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new ProjectQueryResult.Failure.ProfileInactive();

        var result = await queryService.GetAsync(
            request.PersonId,
            false,
            true,
            request.Model,
            cancellationToken);

        return new ProjectQueryResult.Success(result);
    }
}