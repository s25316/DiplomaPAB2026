using Diploma.API.Extensions;
using Diploma.Application.RecruitmentMessages.Commands.UseCases;
using Diploma.Application.RecruitmentMessages.Queries.UseCases;
using Diploma.Application.Recruitments.Commands.UseCases;
using Diploma.Application.Recruitments.Queries.UseCases;
using Diploma.Models.RecruitmentMessages;
using Diploma.Models.Recruitments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.Recruitments;

[Route("api/recruitments")]
[ApiController]
public class RecruitmentController(IMediator mediator) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> CreateRecruitmentCreateAsync(
        [FromQuery] RecruitmentQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new RecruitmentPersonGetHandler.Request
        {
            PersonId = personId.Value,
            Model = queryParameters,
        }, cancellationToken);

        return result switch
        {
            RecruitmentQueryResult.Success => Created(),
            RecruitmentQueryResult.Failure.NotFound => NotFound(),
            RecruitmentQueryResult.Failure.ProfileInactive => Conflict(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(RecruitmentQueryResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateRecruitmentCreateAsync(
        [FromForm] RecruitmentCreateRequest model,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new RecruitmentCreateHandler.Request
        {
            PersonId = personId.Value,
            Model = model,
        }, cancellationToken);

        return result switch
        {
            RecruitmentCreateResult.Success => Created(),
            RecruitmentCreateResult.Failure.NotFound => NotFound(),
            RecruitmentCreateResult.Failure.IsExistRecruitment => Conflict(),
            RecruitmentCreateResult.Failure.NotSameProject => BadRequest("Klucz projektu nie zgadza sie z kluczmi ról."),
            RecruitmentCreateResult.Failure.EmptyProjectRoles => BadRequest("Lista ról w projecie nie moze byc pusta."),
            _ => throw new NotImplementedException($"Unknown type of {nameof(RecruitmentCreateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPut("{recruitmentId:guid}")]
    public async Task<IActionResult> CreateRecruitmentUpdateAsync(
        Guid recruitmentId,
        RecruitmentUpdateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new RecruitmentUpdateHandler.Request
        {
            PersonId = personId.Value,
            RecruitmentId = recruitmentId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            RecruitmentUpdateResult.Success => Ok(),
            RecruitmentUpdateResult.Failure.NotFound => NotFound(),
            RecruitmentUpdateResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(RecruitmentUpdateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpGet("{recruitmentId:guid}/messages")]
    public async Task<IActionResult> GetRecruitmentMessageAsync(
        Guid recruitmentId,
        [FromQuery] RecruitmentMessageQueryParameters model,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new RecruitmentMessageGetHandler.Request
        {
            PersonId = personId.Value,
            RecruitmentId = recruitmentId,
            Model = model,
        }, cancellationToken);

        return result switch
        {
            RecruitmentMessageQueryResult.Success success => Ok(success.Response),
            RecruitmentMessageQueryResult.Failure.NotFound => NotFound(),
            RecruitmentMessageQueryResult.Failure.Forbidden => Forbid(),
            RecruitmentMessageQueryResult.Failure.ProfileInactive => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(RecruitmentMessageQueryResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpGet("messages/{recruitmentMessageId:guid}/file")]
    public async Task<IActionResult> GetRecruitmentMessageFileAsync(
        Guid recruitmentMessageId,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new RecruitmentMessageFileGetHandler.Request
        {
            PersonId = personId.Value,
            RecruitmentMessageId = recruitmentMessageId,
        }, cancellationToken);

        return result switch
        {
            RecruitmentMessageFileResult.Success success => File(
                success.File.OpenReadStream(),
                success.File.ContentType,
                success.File.FileName
            ),
            RecruitmentMessageFileResult.Failure.NotFound => NotFound(),
            RecruitmentMessageFileResult.Failure.Forbidden => Forbid(),
            RecruitmentMessageFileResult.Failure.ProfileInactive => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(RecruitmentMessageFileResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPost("{recruitmentId:guid}/messages")]
    public async Task<IActionResult> CreateRecruitmentMessageCreateAsync(
        Guid recruitmentId,
        [FromForm] RecruitmentMessageCreateRequest model,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new RecruitmentMessageCreateHandler.Request
        {
            PersonId = personId.Value,
            RecruitmentId = recruitmentId,
            Model = model,
        }, cancellationToken);

        return result switch
        {
            RecruitmentMessageCreateResult.Success => Ok(),
            RecruitmentMessageCreateResult.Failure.NotFound => NotFound(),
            RecruitmentMessageCreateResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(RecruitmentMessageCreateResult)}: {result.GetType()}"),
        };
    }
}