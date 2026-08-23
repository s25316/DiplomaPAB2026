using Diploma.Application.Recruitments.Queries.Interfaces;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Projects.Aggregates;
using Diploma.Models.Recruitments;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.Recruitments.Queries.UseCases;

public class RecruitmentProjectGetHandler(
    IPersonRepository personRepository,
    IRecruitmentQueryService queryService,
    IProjectManagerRepository projectManagerRepository
    ) : IRequestHandler<RecruitmentProjectGetHandler.Request, RecruitmentQueryResult>
{
    public sealed record Request : IRequest<RecruitmentQueryResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid ProjectId { get; init; }
        public required RecruitmentQueryParameters Model { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Recruiter,
        ];


    public async Task<RecruitmentQueryResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new RecruitmentQueryResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new RecruitmentQueryResult.Failure.ProfileInactive();

        var personRoles = await projectManagerRepository.GetAsync((ProjectId)request.ProjectId, cancellationToken);

        var countRoles = personRoles
            .Select(i => i.ProjectManagerRole)
            .ToHashSet()
            .Intersect(availableRoles)
            .Count();

        if (countRoles == 0)
            return new RecruitmentQueryResult.Failure.Forbidden();

        var result = await queryService.GetByProjectIdAsync(request.ProjectId, request.Model, cancellationToken);
        return new RecruitmentQueryResult.Success.Success(result);
    }
}