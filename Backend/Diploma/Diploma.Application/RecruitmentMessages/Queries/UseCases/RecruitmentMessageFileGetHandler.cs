using Diploma.Application.Interfaces.Blobs;
using Diploma.Application.RecruitmentMessages.Commands.Repositories;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Models.RecruitmentMessages;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.RecruitmentMessages.Queries.UseCases;

public class RecruitmentMessageFileGetHandler(
    IPersonRepository personRepository,
    IProjectManagerRepository projectManagerRepository,
    IRecruitmentRepository recruitmentRepository,
    IRecruitmentMessageRepository recruitmentMessageRepository,
    IBlobStorage blobStorage
    ) : IRequestHandler<RecruitmentMessageFileGetHandler.Request, RecruitmentMessageFileResult>
{
    public sealed record Request : IRequest<RecruitmentMessageFileResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid RecruitmentMessageId { get; init; }
    }


    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Recruiter,
        ];


    public async Task<RecruitmentMessageFileResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new RecruitmentMessageFileResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new RecruitmentMessageFileResult.Failure.ProfileInactive();

        var recruitmentMessageResult = await recruitmentMessageRepository.GetAsync(request.RecruitmentMessageId, cancellationToken);

        if (!recruitmentMessageResult.HasValue)
            return new RecruitmentMessageFileResult.Failure.NotFound();

        var recruitmentMessage = recruitmentMessageResult.Value;

        if (string.IsNullOrWhiteSpace(recruitmentMessage.File))
            return new RecruitmentMessageFileResult.Failure.NotFound();

        var recruitmentResult = await recruitmentRepository.GetAsync(recruitmentMessage.RecruitmentId, cancellationToken);

        if (!recruitmentResult.HasValue)
            return new RecruitmentMessageFileResult.Failure.NotFound();

        var recruitment = recruitmentResult.Value;

        var isAuthorized = await IsAuthorizedAsync(request.PersonId, recruitment, cancellationToken);

        if (!isAuthorized)
            return new RecruitmentMessageFileResult.Failure.Forbidden();


        var fileResult = await blobStorage.GetAsync(
            recruitmentMessage.MessageId.ToString(),
            recruitmentMessage.File,
            cancellationToken);

        if (!fileResult.HasValue)
            return new RecruitmentMessageFileResult.Failure.NotFound();

        return new RecruitmentMessageFileResult.Success(fileResult.Value);
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