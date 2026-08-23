using Diploma.Application.RecruitmentMessages.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Models.RecruitmentMessages;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.RecruitmentMessages.Queries.UseCases;

public class RecruitmentMessageGetHandler(
    IPersonRepository personRepository,
    IProjectManagerRepository projectManagerRepository,
    IRecruitmentRepository recruitmentRepository,
    IRecruitmentMessageQueryService queryService
    ) : IRequestHandler<RecruitmentMessageGetHandler.Request, RecruitmentMessageQueryResult>
{
    public sealed record Request : IRequest<RecruitmentMessageQueryResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid RecruitmentId { get; init; }
        public required RecruitmentMessageQueryParameters Model { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Recruiter,
        ];


    public async Task<RecruitmentMessageQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new RecruitmentMessageQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new RecruitmentMessageQueryResult.Failure.ProfileInactive();

        var recruitmentResult = await recruitmentRepository.GetAsync(request.RecruitmentId, cancellationToken);

        if (!recruitmentResult.HasValue)
            return new RecruitmentMessageQueryResult.Failure.NotFound();

        var recruitment = recruitmentResult.Value;

        var isAuthorized = await IsAuthorizedAsync(request.PersonId, recruitment, cancellationToken);

        if (!isAuthorized)
            return new RecruitmentMessageQueryResult.Failure.Forbidden();

        var result = await queryService.GetAsync(request.RecruitmentId, request.Model, cancellationToken);
        return new RecruitmentMessageQueryResult.Success.Success(result);
    }

    private async Task<bool> IsAuthorizedAsync(
        PersonId personId,
        Recruitment recruitment,
        CancellationToken cancellationToken = default)
    {
        if (recruitment.PersonId == personId) return true;

        var personRoles = await projectManagerRepository.GetAsync(recruitment.ProjectId, cancellationToken);

        var countRoles = personRoles
            .Select(i => i.ProjectManagerRole)
            .ToHashSet()
            .Intersect(availableRoles)
            .Count();

        return countRoles > 0;
    }
}