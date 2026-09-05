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
            PersonEducationCreateResult.Success => Created(), // Ewentualnie CreatedAtAction z lokalizacją
            PersonEducationCreateResult.Failure.NotFound => NotFound(new { message = "Nie znaleziono wskazanego zasobu." }),
            PersonEducationCreateResult.Failure.Forbidden => Forbid(),
            PersonEducationCreateResult.Failure.OverLimit overLimit => Conflict(new { message = $"Osiągnięto maksymalną dozwoloną liczbę wpisów ({overLimit.MaxCount}).", maxCount = overLimit.MaxCount }),
            PersonEducationCreateResult.Failure.NotFoundCourse => NotFound(new { message = "Wybrany kurs nie istnieje." }),
            PersonEducationCreateResult.Failure.NotFoundCourseInstance => NotFound(new { message = "Wybrana edycja kursu nie istnieje." }),
            PersonEducationCreateResult.Failure.InvalidCourseDates => BadRequest(new { message = "Podane daty kursu są nieprawidłowe." }),
            PersonEducationCreateResult.Failure.InvalidCourseInstanceDates => BadRequest(new { message = "Podane daty edycji kursu są nieprawidłowe." }),
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
            PersonEducationUpdateResult.Success => Ok(),
            PersonEducationUpdateResult.Failure.NotFound => NotFound(new { message = "Nie znaleziono wskazanego zasobu." }),
            PersonEducationUpdateResult.Failure.Forbidden => Forbid(),
            PersonEducationUpdateResult.Failure.OverLimit overLimit => Conflict(new { message = $"Osiągnięto maksymalną dozwoloną liczbę wpisów ({overLimit.MaxCount}).", maxCount = overLimit.MaxCount }),
            PersonEducationUpdateResult.Failure.NotFoundCourse => NotFound(new { message = "Wybrany kurs nie istnieje." }),
            PersonEducationUpdateResult.Failure.NotFoundCourseInstance => NotFound(new { message = "Wybrana edycja kursu nie istnieje." }),
            PersonEducationUpdateResult.Failure.InvalidCourseDates => BadRequest(new { message = "Podane daty kursu są nieprawidłowe." }),
            PersonEducationUpdateResult.Failure.InvalidCourseInstanceDates => BadRequest(new { message = "Podane daty edycji kursu są nieprawidłowe." }),
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