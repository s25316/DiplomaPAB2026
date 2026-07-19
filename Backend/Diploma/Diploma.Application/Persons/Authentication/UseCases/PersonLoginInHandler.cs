using Diploma.Application.Interfaces.Database;
using Diploma.Application.Interfaces.Security;
using Diploma.Application.Persons.Extensions;
using Diploma.Application.Services.Generators;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.Persons.Events.Authentication;
using Diploma.Domain.ValueObjects;
using Diploma.Models.Persons.Authentication;
using MediatR;

namespace Diploma.Application.Persons.Authentication.UseCases;

public class PersonLoginInHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository repository,
    IPasswordHasherService passwordHasherService,
    IJwtGenerator jwtGenerator,
    IRefreshTokenGenerator refreshTokenGenerator
    ) : IRequestHandler<PersonLoginInHandler.Request, PersonLoginInResult>
{
    public sealed record Request : IRequest<PersonLoginInResult>
    {
        public required Guid PersonOperationId { get; init; }
        public required PersonLoginInRequest Model { get; init; }
    }


    private static readonly PersonLoginInResult.Failure Failure = new();


    public async Task<PersonLoginInResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await repository.GetAsync(new Email(request.Model.Login), cancellationToken);

        if (!personResult.HasValue)
            return Failure;

        var person = personResult.Value;

        if (person.HasAnonymized)
            return Failure;

        if (!person.HasActivated)
        {
            await PrepareNotActivatedAsync(person, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return Failure;
        }

        if (person.HasRemoved)
        {
            await PrepareRemovedAsync(person, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return Failure;
        }


        var hashedResult = passwordHasherService.Hash(request.Model.Password, person.Salt);

        if (hashedResult.HashedPassword != person.Password)
        {
            await PrepareInvalidPasswordAsync(person, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return Failure;
        }


        ArgumentNullException.ThrowIfNull(person.Id);
        var jwtResult = jwtGenerator.Generate(person.Id.Value);
        var refreshTokenResult = refreshTokenGenerator.Generate();

        person.LoginInSucess(refreshTokenResult.RefreshToken, refreshTokenResult.ExpiresAt);
        await mediator.PublishEventsAsync(person, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new PersonLoginInResult.Success
        {
            JwtToken = jwtResult.Jwt,
            JwtTokenExpiresAt = jwtResult.ExpiresAt,
            RefreshToken = refreshTokenResult.RefreshToken,
            RefreshTokenTokenExpiresAt = refreshTokenResult.ExpiresAt,
        };
    }

    private async Task PrepareNotActivatedAsync(Person person, CancellationToken cancellationToken)
    {
        person.LoginInUnSuccess(new PersonLoginInUnSuccessReason.ProfileIsNotActivated());
        await mediator.PublishEventsAsync(person, cancellationToken);
    }

    private async Task PrepareRemovedAsync(Person person, CancellationToken cancellationToken)
    {
        person.LoginInUnSuccess(new PersonLoginInUnSuccessReason.ProfileRemoved());
        await mediator.PublishEventsAsync(person, cancellationToken);
    }

    private async Task PrepareInvalidPasswordAsync(Person person, CancellationToken cancellationToken)
    {
        person.LoginInUnSuccess(new PersonLoginInUnSuccessReason.InvalidPassword());
        await mediator.PublishEventsAsync(person, cancellationToken);
    }
}