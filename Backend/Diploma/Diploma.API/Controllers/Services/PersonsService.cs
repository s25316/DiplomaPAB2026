using Diploma.Application.PersonEducations.Queries.UseCases;
using Diploma.Application.PersonEmployments.Queries.UseCases;
using Diploma.Application.Persons.Queries.Profile.UseCases;
using Diploma.Application.PersonUris.Queries.UseCases;
using Diploma.Models.PersonEducations;
using Diploma.Models.PersonEmployments;
using Diploma.Models.Persons.Profile;
using Diploma.Models.PersonUris;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers.Services;

public interface IPersonsService
{
    Task<IActionResult> GetIdentityDataAsync(Guid personId, CancellationToken cancellationToken);
    Task<IActionResult> GetProfileDataAsync(Guid personId, CancellationToken cancellationToken);
    Task<IActionResult> GetEducationDisciplinesAsync(Guid personId, CancellationToken cancellationToken);
    Task<IActionResult> GetEducationHistoryAsync(Guid personId, PersonEducationQueryParameters queryParameters, CancellationToken cancellationToken);
    Task<IActionResult> GetEmploymentsAsync(Guid personId, PersonEmploymentQueryParameters queryParameters, CancellationToken cancellationToken);
    Task<IActionResult> GetUrisAsync(Guid personId, PersonUriQueryParameters queryParameters, CancellationToken cancellationToken);
}

public class PersonsService(IMediator mediator) : IPersonsService
{
    public async Task<IActionResult> GetIdentityDataAsync(
        Guid personId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonGetIdentityDataHandler.Request
        {
            PersonId = personId,
        }, cancellationToken);

        return result switch
        {
            PersonIdentityDataQueryResult.Success success => new OkObjectResult(success.Response),
            PersonIdentityDataQueryResult.Failure.NotFound => new NotFoundResult(),
            PersonIdentityDataQueryResult.Failure.ProfileInactive => new ForbidResult(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonIdentityDataQueryResult)}: {result.GetType()}"),
        };
    }

    public async Task<IActionResult> GetProfileDataAsync(
        Guid personId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonGetProfileDataHandler.Request
        {
            PersonId = personId,
        }, cancellationToken);

        return result switch
        {
            PersonProfileDataQueryResult.Success success => new OkObjectResult(success.Response),
            PersonProfileDataQueryResult.Failure.NotFound => new NotFoundResult(),
            PersonProfileDataQueryResult.Failure.ProfileInactive => new ForbidResult(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonProfileDataQueryResult)}: {result.GetType()}"),
        };
    }

    public async Task<IActionResult> GetEducationDisciplinesAsync(
        Guid personId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonDisciplineGetHandler.Request
        {
            PersonId = personId,
        }, cancellationToken);

        return result switch
        {
            PersonDisciplineQueryResult.Success success => new OkObjectResult(success.Response),
            PersonDisciplineQueryResult.Failure.NotFound => new NotFoundResult(),
            PersonDisciplineQueryResult.Failure.ProfileInactive => new ForbidResult(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonDisciplineQueryResult)}: {result.GetType()}"),
        };
    }

    public async Task<IActionResult> GetEducationHistoryAsync(
        Guid personId,
        PersonEducationQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonEducationGetHandler.Request
        {
            PersonId = personId,
            Model = queryParameters,
        }, cancellationToken);

        return result switch
        {
            PersonEducationQueryResult.Success success => new OkObjectResult(success.Response),
            PersonEducationQueryResult.Failure.NotFound => new NotFoundResult(),
            PersonEducationQueryResult.Failure.ProfileInactive => new ForbidResult(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEducationQueryResult)}: {result.GetType()}"),
        };
    }

    public async Task<IActionResult> GetEmploymentsAsync(
        Guid personId,
        PersonEmploymentQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonEmploymentGetHandler.Request
        {
            PersonId = personId,
            Model = queryParameters,
        }, cancellationToken);

        return result switch
        {
            PersonEmploymentQueryResult.Success success => new OkObjectResult(success.Response),
            PersonEmploymentQueryResult.Failure.NotFound => new NotFoundResult(),
            PersonEmploymentQueryResult.Failure.ProfileInactive => new ForbidResult(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEmploymentQueryResult)}: {result.GetType()}"),
        };
    }

    public async Task<IActionResult> GetUrisAsync(
        Guid personId,
        PersonUriQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonUriGetHandler.Request
        {
            PersonId = personId,
            Model = queryParameters,
        }, cancellationToken);

        return result switch
        {
            PersonUriQueryResult.Success success => new OkObjectResult(success.Response),
            PersonUriQueryResult.Failure.NotFound => new NotFoundResult(),
            PersonUriQueryResult.Failure.ProfileInactive => new ForbidResult(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUriQueryResult)}: {result.GetType()}"),
        };
    }
}