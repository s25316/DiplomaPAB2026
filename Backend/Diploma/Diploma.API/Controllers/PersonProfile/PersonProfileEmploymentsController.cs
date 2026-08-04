using Diploma.API.Controllers.Services;
using Diploma.API.Extensions;
using Diploma.Application.PersonEmployments.Commands.UseCases;
using Diploma.Models.PersonEmployments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.PersonProfile;

[Authorize]
[Route("api/person/profile/employments")]
[ApiController]
public class PersonProfileEmploymentsController(
    IMediator mediator,
    IPersonsService personsService
    ) : ControllerBase
{
    [Authorize]
    [HttpGet()]
    public async Task<IActionResult> GetAsync(
        [FromQuery] PersonEmploymentQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        return await personsService.GetEmploymentsAsync(personId.Value, queryParameters, cancellationToken);
    }


    [Authorize]
    [HttpPost()]
    public async Task<IActionResult> CreateAsync(
        [FromBody] PersonEmploymentCreateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonEmploymentCreateHandler.Request
        {
            PersonId = personId.Value,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonEmploymentCreateResult.Success => Created(),
            PersonEmploymentCreateResult.Failure.NotFound => NotFound(),
            PersonEmploymentCreateResult.Failure.Forbidden => Forbid(),
            PersonEmploymentCreateResult.Failure.NotFoundCompany company => NotFound(company.Regon.ToString()),
            PersonEmploymentCreateResult.Failure.InvalidCompanyDates dates => BadRequest(dates),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEmploymentCreateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPut("{employmentId:guid}")]
    public async Task<IActionResult> UpdateAsync(
        Guid employmentId,
        [FromBody] PersonEmploymentUpdateRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonEmploymentUpdateHandler.Request
        {
            PersonId = personId.Value,
            EmploymentId = employmentId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonEmploymentUpdateResult.Success => Ok(),
            PersonEmploymentUpdateResult.Failure.NotFound => NotFound(),
            PersonEmploymentUpdateResult.Failure.Forbidden => Forbid(),
            PersonEmploymentUpdateResult.Failure.NotFoundCompany company => NotFound(company.Regon.ToString()),
            PersonEmploymentUpdateResult.Failure.InvalidCompanyDates dates => BadRequest(dates),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEmploymentUpdateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpDelete("{employmentId:guid}")]
    public async Task<IActionResult> DeleteAsync(
        Guid employmentId,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonEmploymentDeleteHandler.Request
        {
            PersonId = personId.Value,
            EmploymentId = employmentId,
        }, cancellationToken);

        return result switch
        {
            PersonEmploymentDeleteResult.Success => Ok(),
            PersonEmploymentDeleteResult.Failure.NotFound => NotFound(),
            PersonEmploymentDeleteResult.Failure.Forbidden => Forbid(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEmploymentDeleteResult)}: {result.GetType()}"),
        };
    }
}