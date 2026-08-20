using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Models.Recruitments;
using Diploma.Shared.ProjectManagerRoles;
using Diploma.Shared.RecruitmentStatuses;
using MediatR;

namespace Diploma.Application.Recruitments.Commands.UseCases;

public class RecruitmentUpdateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IRecruitmentRepository recruitmentRepository,
    IProjectManagerRepository projectManagerRepository
    ) : IRequestHandler<RecruitmentUpdateHandler.Request, RecruitmentUpdateResult>
{
    public sealed record Request : IRequest<RecruitmentUpdateResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid RecruitmentId { get; init; }
        public required RecruitmentUpdateRequest Model { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Recruiter,
        ];

    public async Task<RecruitmentUpdateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new RecruitmentUpdateResult.Failure.NotFound();

        var person = personResult.Value;
        ArgumentNullException.ThrowIfNull(person.Id);

        var recruitmentResult = await recruitmentRepository.GetAsync(request.RecruitmentId, cancellationToken);

        if (!recruitmentResult.HasValue)
            return new RecruitmentUpdateResult.Failure.NotFound();

        var recruitment = recruitmentResult.Value;

        var personRoles = await projectManagerRepository.GetAsync(recruitment.ProjectId, cancellationToken);

        var countRoles = personRoles
            .Select(i => i.ProjectManagerRole)
            .ToHashSet()
            .Intersect(availableRoles)
            .Count();

        if (countRoles == 0)
            return new RecruitmentUpdateResult.Failure.Forbidden();

        if (recruitment.RecruitmentStatus.Id == request.Model.StatusId)
            return new RecruitmentUpdateResult.Success();

        recruitment.RecruitmentStatus = RecruitmentStatus.FromId(request.Model.StatusId);
        await recruitmentRepository.UpdateAsync(person.Id, recruitment, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new RecruitmentUpdateResult.Success();
    }
}