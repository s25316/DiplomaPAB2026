using Diploma.Application.Interfaces.Database;
using Diploma.Application.Persons.Authentication.Projections.RefreshTokens;
using Diploma.Application.Persons.Extensions;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Authentication;
using MediatR;

namespace Diploma.Application.Persons.Authentication.UseCases;

public class PersonLogOutHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository repository,
    IPersonRefreshTokenProjectionService refreshTokenProjectionService
    ) : IRequestHandler<PersonLogOutHandler.Request, PersonLogOutResult>
{
    public sealed record Request : IRequest<PersonLogOutResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonLogOutRequest Model { get; init; }
    }


    private static readonly PersonLogOutResult.Failure Failure = new();
    private static readonly PersonLogOutResult.Success Success = new();


    public async Task<PersonLogOutResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);

        var projectionResult = await refreshTokenProjectionService.GetAsync(
            request.Model.RefreshToken,
            cancellationToken);

        if (!projectionResult.HasValue)
            return Failure;

        var projection = projectionResult.Value;
        var projectionPersonId = projection.PersonId;

        if (request.PersonId != projectionPersonId.Value)
            return Failure;

        if (projection.HasLogOut)
            return Success;

        var person = await GetPersonAsync(projectionPersonId, cancellationToken);

        if (!person.HasActive)
            return Failure;

        person.LogOut(projection.PersonRefreshTokenId);
        await mediator.PublishEventsAsync(person, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return Success;
    }

    private async Task<Person> GetPersonAsync(Guid personId, CancellationToken cancellationToken)
    {
        var personResult = await repository.GetAsync(personId, cancellationToken);
        return personResult.Value ?? throw new InvalidOperationException($"Unable get {typeof(Person)} by Id : {personId}."); ;
    }
}