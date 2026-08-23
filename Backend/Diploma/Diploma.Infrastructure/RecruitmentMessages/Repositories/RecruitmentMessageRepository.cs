using Diploma.Application.RecruitmentMessages.Commands.Repositories;
using Diploma.Database;
using Diploma.Database.Models.Projects.Recruitments;
using Diploma.Domain.Base.Results;
using Diploma.Infrastructure.QueryBuilders.Projects;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Infrastructure.RecruitmentMessages.Repositories;

public class RecruitmentMessageRepository(
    DiplomaDbContext context,
    RecruitmentMessageQueryBuilder builder
    ) : IRecruitmentMessageRepository
{
    public async Task<Guid> CreateAsync(
        RecruitmentMessageInput input,
        CancellationToken cancellationToken = default)
    {
        var databaseItem = new RecruitmentMessage
        {
            PersonId = input.PersonId,
            RecruitmentId = input.RecruitmentId,
            Message = input.Message,
            File = input.File,
            CreatedAt = DateTimeOffset.Now,
        };

        await context.RecruitmentMessages.AddAsync(databaseItem, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return databaseItem.RecruitmentMessageId;
    }

    public async Task<OptionalResult<RecruitmentMessageItem>> GetAsync(
        Guid messageId,
        CancellationToken cancellationToken = default)
    {
        var query = builder
            .WithMessageId(messageId)
            .Build();

        var datbaseItem = await query.FirstOrDefaultAsync(cancellationToken);

        if (datbaseItem is null)
            return OptionalResult<RecruitmentMessageItem>.NotFound();

        return OptionalResult.Success(Map(datbaseItem));
    }

    private static RecruitmentMessageItem Map(RecruitmentMessage item) => new()
    {
        MessageId = item.RecruitmentMessageId,
        RecruitmentId = item.RecruitmentId,
        PersonId = item.Recruitment.PersonId,
        ProjectId = item.Recruitment.ProjectId,
        Message = item.Message,
        File = item.File,
        CreatedAt = item.CreatedAt,
    };
}