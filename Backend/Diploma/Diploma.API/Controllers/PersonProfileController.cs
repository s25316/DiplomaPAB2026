using Diploma.API.Extensions;
using Diploma.Application.Persons.Authentication.UseCases;
using Diploma.Application.Persons.Lifecycle.UseCases;
using Diploma.Application.Persons.Profile.UseCases;
using Diploma.Models.Persons.Authentication;
using Diploma.Models.Persons.Lifecycle;
using Diploma.Models.Persons.Profile;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers;

[Route("api/person/profile")]
[ApiController]
public class PersonProfileController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(
        PersonCreateRequest body,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonCreateHandler.Request
        {
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonCreateResult.Success success => Ok(success),
            PersonCreateResult.Failure.LoginTaken => Conflict("Wybierz inny login."),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonCreateResult)}: {result.GetType()}"),
        };
    }


    [HttpPost("activate/{operationId}")]
    public async Task<IActionResult> ActivateAsync(
        Guid operationId,
        PersonActivateRequest body,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonActivateHandler.Request
        {
            OperationId = operationId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonActivateResult.Success => NoContent(),
            PersonActivateResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonActivateResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPost("remove")]
    public async Task<IActionResult> RemoveAsync(CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonRemoveHandler.Request
        {
            PersonId = personId.Value,
        }, cancellationToken);

        return result switch
        {
            PersonRemoveResult.Success => NoContent(),
            PersonRemoveResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonActivateResult)}: {result.GetType()}"),
        };
    }


    [HttpPost("restore/{operationId}")]
    public async Task<IActionResult> RemoveAsync(
        Guid operationId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonRestoreHandler.Request
        {
            OperationId = operationId,
        }, cancellationToken);

        return result switch
        {
            PersonRestoreResult.Success => NoContent(),
            PersonRestoreResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonActivateResult)}: {result.GetType()}"),
        };
    }


    [HttpPost("loginIn")]
    public async Task<IActionResult> LoginInAsync(
        Guid personOperationId,
        PersonLoginInRequest body,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonLoginInHandler.Request
        {
            PersonOperationId = personOperationId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonLoginInResult.Success success => Ok(success),
            PersonLoginInResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonLoginInResult)}: {result.GetType()}"),
        };
    }


    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshTokenAsync(
        RefreshTokenRequest body,
        CancellationToken cancellationToken)
    {
        if (!Request.TryGetJwt(out var jwtToken))
            return Unauthorized("Brak JWT tokena.");

        var result = await mediator.Send(new RefreshTokenHandler.Request
        {
            Jwt = jwtToken,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            RefreshTokenResult.Success success => Ok(success),
            RefreshTokenResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(RefreshTokenResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPost("logOut")]
    public async Task<IActionResult> LogOutAsync(
        PersonLogOutRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonLogOutHandler.Request
        {
            PersonId = personId.Value,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonLogOutResult.Success success => Ok(success),
            PersonLogOutResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonLogOutResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPost("login/initiation")]
    public async Task<IActionResult> UpdateLoginInitiationAsync(CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonUpdateLoginInitiationHandler.Request
        {
            PersonId = personId.Value,
        }, cancellationToken);

        return result switch
        {
            PersonUpdateLoginResult.Initiation initiation => Ok(initiation),
            PersonUpdateLoginResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUpdateLoginResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPost("login/{operationId}")]
    public async Task<IActionResult> UpdateLoginAsync(
        Guid operationId,
        PersonUpdateLoginRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonUpdateLoginHandler.Request
        {
            PersonId = personId.Value,
            OperationId = operationId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonUpdateLoginResult.Success => NoContent(),
            PersonUpdateLoginResult.Failure.General => BadRequest(),
            PersonUpdateLoginResult.Failure.LoginTaken => Conflict("Wybierz inny login."),
            PersonUpdateLoginResult.Failure.LoginExist => Conflict("Nowy login nie może być taki sam jak obecny."),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUpdateLoginResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPost("password")]
    public async Task<IActionResult> UpdatePasswordAsync(
        PersonUpdatePasswordRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonUpdatePasswordHandler.Request
        {
            PersonId = personId.Value,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonUpdatePasswordResult.Success => NoContent(),
            PersonUpdatePasswordResult.Failure.General => BadRequest(),
            PersonUpdatePasswordResult.Failure.PasswordExist => BadRequest("Nowe hasło nie może być takie samo jak obecne."),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUpdatePasswordResult)}: {result.GetType()}"),
        };
    }


    [HttpPost("password/recovery/initiation")]
    public async Task<IActionResult> UpdatePasswordRecoveryInitiationAsync(
        PersonUpdatePasswordRecoveryInitiationRequest body,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonUpdatePasswordRecoveryInitiationHandler.Request
        {
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonUpdatePasswordResult.Initiation initiation => Ok(initiation),
            PersonUpdatePasswordResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUpdatePasswordResult)}: {result.GetType()}"),
        };
    }


    [HttpPost("password/recovery/{operationId}")]
    public async Task<IActionResult> UpdatePasswordRecoveryAsync(
        Guid operationId,
        PersonUpdatePasswordRecoveryRequest body,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new PersonUpdatePasswordRecoveryHandler.Request
        {
            OperationId = operationId,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonUpdatePasswordResult.Success => NoContent(),
            PersonUpdatePasswordResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUpdatePasswordResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPost("identity")]
    public async Task<IActionResult> UpdateIdentityDataAsync(
        PersonUpdateIdentityDataRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonUpdateIdentityDataHandler.Request
        {
            PersonId = personId.Value,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonUpdateIdentityDataResult.Success => NoContent(),
            PersonUpdateIdentityDataResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUpdateIdentityDataResult)}: {result.GetType()}"),
        };
    }


    [Authorize]
    [HttpPost("profile")]
    public async Task<IActionResult> UpdateProfileDataAsync(
        PersonUpdateProfileDataRequest body,
        CancellationToken cancellationToken)
    {
        if (!User.TryGetNameIdentifier(out var personId))
            return Unauthorized();

        var result = await mediator.Send(new PersonUpdateProfileDataHandler.Request
        {
            PersonId = personId.Value,
            Model = body,
        }, cancellationToken);

        return result switch
        {
            PersonUpdateProfileDataResult.Success => NoContent(),
            PersonUpdateProfileDataResult.Failure => BadRequest(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonUpdateProfileDataResult)}: {result.GetType()}"),
        };
    }
}