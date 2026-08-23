using Diploma.Application.Recruitments.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Recruitments;
using MediatR;

namespace Diploma.Application.Recruitments.Queries.UseCases;

public class RecruitmentPersonGetHandler(
    IPersonRepository personRepository,
    IRecruitmentQueryService queryService
    ) : IRequestHandler<RecruitmentPersonGetHandler.Request, RecruitmentQueryResult>
{
    public sealed record Request : IRequest<RecruitmentQueryResult>
    {
        public required Guid PersonId { get; init; }
        public required RecruitmentQueryParameters Model { get; init; }
    }


    public async Task<RecruitmentQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new RecruitmentQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new RecruitmentQueryResult.Failure.ProfileInactive();

        var result = await queryService.GetByPersonIdAsync(request.PersonId, request.Model, cancellationToken);
        return new RecruitmentQueryResult.Success.Success(result);
    }
}