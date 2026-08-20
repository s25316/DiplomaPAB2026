using Diploma.Application.Interfaces.Blobs;
using Diploma.Application.Interfaces.Database;
using Diploma.Application.RecruitmentMessages.Commands.Repositories;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectManagers.Aggregates;
using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Models.RecruitmentMessages;
using Diploma.Shared.ProjectManagerRoles;
using MediatR;

namespace Diploma.Application.RecruitmentMessages.Commands.UseCases;

public class RecruitmentMessageCreateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IRecruitmentRepository recruitmentRepository,
    IProjectManagerRepository projectManagerRepository,
    IRecruitmentMessageRepository recruitmentMessageRepository,
    IBlobStorage blobStorage
    ) : IRequestHandler<RecruitmentMessageCreateHandler.Request, RecruitmentMessageCreateResult>
{
    public sealed record Request : IRequest<RecruitmentMessageCreateResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid RecruitmentId { get; init; }
        public required RecruitmentMessageCreateRequest Model { get; init; }
    }

    private static readonly IEnumerable<ProjectManagerRole> availableRoles = [
        ProjectManagerRole.Creator,
        ProjectManagerRole.Admin,
        ProjectManagerRole.Recruiter,
        ];

    public async Task<RecruitmentMessageCreateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new RecruitmentMessageCreateResult.Failure.NotFound();

        var person = personResult.Value;
        ArgumentNullException.ThrowIfNull(person.Id);

        var recruitmentResult = await recruitmentRepository.GetAsync(request.RecruitmentId, cancellationToken);

        if (!recruitmentResult.HasValue)
            return new RecruitmentMessageCreateResult.Failure.NotFound();

        var recruitment = recruitmentResult.Value;

        var hasAccess = await HasAccessAsync(request.PersonId, recruitment, cancellationToken);

        if (!hasAccess)
            return new RecruitmentMessageCreateResult.Failure.Forbidden();

        ArgumentNullException.ThrowIfNull(recruitment.Id);
        var messageId = await recruitmentMessageRepository.CreateAsync(new RecruitmentMessageInput
        {
            PersonId = recruitment.PersonId,
            RecruitmentId = recruitment.Id,
            Message = request.Model.Message,
            File = request.Model.File?.FileName,
        }, cancellationToken);

        if (request.Model.File is not null)
            await blobStorage.SaveAsync(messageId.ToString(), request.Model.File, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);
        return new RecruitmentMessageCreateResult.Success();
    }

    private async Task<bool> HasAccessAsync(
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