using Diploma.API.Controllers.Services;
using Diploma.API.Extensions;
using Diploma.Application.PersonEducations.Commands.UseCases;
using Diploma.Models.Dictionaries;
using Diploma.Models.PersonEducations;
using Diploma.Shared.Semesters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.PersonProfile;

[Authorize]
[Route("api/person/profile/educations")]
[ApiController]
public class PersonProfileEducationsController(
    IMediator mediator,
    IPersonsService personsService
    ) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("semesters")]
    public IActionResult GetSemesters()
    {
        var items = Semester.All.Select(i => new DictionaryItem<int>
        {
            Code = i.Id,
            Name = i.Name,
        });
        return Ok(items);
    }


    [Authorize]
    [HttpGet("disciplines")]
    public async Task<IActionResult> GetDisciplinesAsync(
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        return await personsService.GetEducationDisciplinesAsync(personId.Value, cancellationToken);
    }

    [Authorize]
    [HttpGet()]
    public async Task<IActionResult> GetAsync(
        [FromQuery] PersonEducationQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        return await personsService.GetEducationHistoryAsync(personId.Value, queryParameters, cancellationToken);
    }

    [Authorize]
    [HttpPost()]
    public async Task<IActionResult> CreateAsync(
        [FromBody] PersonEducationCreateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonEducationCreateHandler.Request
        {
            PersonId = personId.Value,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonEducationCreateResult.Success => Created(),
            PersonEducationCreateResult.Failure.NotFound => NotFound(),
            PersonEducationCreateResult.Failure.Forbidden => Forbid(),
            PersonEducationCreateResult.Failure.OverLimit overLimit => Conflict(),
            PersonEducationCreateResult.Failure.NotFoundCourse notFoundCourse => NotFound(),
            PersonEducationCreateResult.Failure.NotFoundCourseInstance notFoundCourseInstance => NotFound(),
            PersonEducationCreateResult.Failure.InvalidCourseDates invalidCourseDates => BadRequest(),
            PersonEducationCreateResult.Failure.InvalidCourseInstanceDates invalidCourseInstanceDates => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEducationCreateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPut("{educationId:guid}")]
    public async Task<IActionResult> UpdateAsync(
        Guid educationId,
        [FromBody] PersonEducationUpdateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonEducationUpdateHandler.Request
        {
            PersonId = personId.Value,
            EducationId = educationId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonEducationUpdateResult.Success => Created(),
            PersonEducationUpdateResult.Failure.NotFound => NotFound(),
            PersonEducationUpdateResult.Failure.Forbidden => Forbid(),
            PersonEducationUpdateResult.Failure.OverLimit overLimit => BadRequest(),
            PersonEducationUpdateResult.Failure.NotFoundCourse notFoundCourse => NotFound(),
            PersonEducationUpdateResult.Failure.NotFoundCourseInstance notFoundCourseInstance => NotFound(),
            PersonEducationUpdateResult.Failure.InvalidCourseDates invalidCourseDates => BadRequest(),
            PersonEducationUpdateResult.Failure.InvalidCourseInstanceDates invalidCourseInstanceDates => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEducationCreateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpDelete("{educationId:guid}")]
    public async Task<IActionResult> DeleteAsync(
        Guid educationId,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonEducationDeleteHandler.Request
        {
            PersonId = personId.Value,
            EducationId = educationId,
        }, cancellationToken);

        return result switch
        {
            PersonEducationDeleteResult.Success => Ok(),
            PersonEducationDeleteResult.Failure.NotFound => NotFound(),
            PersonEducationDeleteResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEducationDeleteResult)}: {result.GetType()}"),
        };
    }
}