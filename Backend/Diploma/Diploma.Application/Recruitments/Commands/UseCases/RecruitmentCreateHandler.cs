using Diploma.Application.Interfaces.Blobs;
using Diploma.Application.Interfaces.Database;
using Diploma.Application.RecruitmentMessages.Commands.Repositories;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.ProjectRoles.Aggregates;
using Diploma.Domain.Recruitments.Aggregates;
using Diploma.Models.Recruitments;
using MediatR;
using DomainRecruitment = Diploma.Domain.Recruitments.Aggregates.Recruitment;

namespace Diploma.Application.Recruitments.Commands.UseCases;

public class RecruitmentCreateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IProjectRoleRepository projectRoleRepository,
    IRecruitmentRepository recruitmentRepository,
    IRecruitmentMessageRepository recruitmentMessageRepository,
    IBlobStorage blobStorage
    ) : IRequestHandler<RecruitmentCreateHandler.Request, RecruitmentCreateResult>
{
    public sealed record Request : IRequest<RecruitmentCreateResult>
    {
        public required Guid PersonId { get; init; }
        public required RecruitmentCreateRequest Model { get; init; }
    }


    public async Task<RecruitmentCreateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new RecruitmentCreateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasIdentityData)
            return new RecruitmentCreateResult.Failure.ProfileIsEmpty();

        if (!request.Model.ProjectRoleIds.Any())
            return new RecruitmentCreateResult.Failure.EmptyProjectRoles();

        Guid? projectId = null;

        foreach (var projectRoleId in request.Model.ProjectRoleIds)
        {
            var projectRoleResult = await projectRoleRepository.GetAsync(projectRoleId, cancellationToken);

            if (!projectRoleResult.HasValue)
                return new RecruitmentCreateResult.Failure.NotFound();

            var projectRole = projectRoleResult.Value;

            if (!projectId.HasValue)
                projectId = projectRole.ProjectId;

            if (projectId != projectRole.ProjectId.Value)
                return new RecruitmentCreateResult.Failure.NotSameProject();

            if (!projectRole.IsAvailableRecruitment)
                return new RecruitmentCreateResult.Failure.NotAvailableRecruitment();
        }

        if (!projectId.HasValue)
            return new RecruitmentCreateResult.Failure.EmptyProjectRoles();

        var existingItemResult = await recruitmentRepository.GetAsync(request.PersonId, projectId, cancellationToken);

        if (existingItemResult.HasValue)
            return new RecruitmentCreateResult.Failure.IsExistRecruitment();

        var recruitment = DomainRecruitment.Create(
            request.PersonId,
            projectId,
            request.Model.ProjectRoleIds.Select(i => (ProjectRoleId)i)
            );

        await recruitmentRepository.CreateAsync(recruitment, cancellationToken);

        ArgumentNullException.ThrowIfNull(recruitment.Id);
        var messageId = await recruitmentMessageRepository.CreateAsync(new RecruitmentMessageInput
        {
            PersonId = recruitment.PersonId,
            RecruitmentId = recruitment.Id,
            Message = request.Model.Message,
            File = request.Model.File.FileName,
        }, cancellationToken);

        await blobStorage.SaveAsync(messageId.ToString(), request.Model.File, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new RecruitmentCreateResult.Success();
    }
}