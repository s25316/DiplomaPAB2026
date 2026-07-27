using Diploma.Application.Interfaces.Database;
using Diploma.Application.Interfaces.Security;
using Diploma.Application.Persons.Commands.Extensions;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.Persons.Authentication;
using MediatR;

namespace Diploma.Application.Persons.Commands.Authentication.UseCases;

public class PersonUpdatePasswordHandler(
    IMediator mediator,
    IUnitOfWorkFactory unitOfWorkFactory,
    IPasswordHasherService passwordHasher,
    IPersonRepository repository
    ) : IRequestHandler<PersonUpdatePasswordHandler.Request, PersonUpdatePasswordResult>
{
    public sealed record Request : IRequest<PersonUpdatePasswordResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonUpdatePasswordRequest Model { get; init; }
    }


    private static PersonUpdatePasswordResult.Failure.General Failure => new();
    private static PersonUpdatePasswordResult.Success Success => new();


    public async Task<PersonUpdatePasswordResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var person = await GetPersonAsync(request.PersonId, cancellationToken);

        var oldHashedPassword = passwordHasher.Hash(request.Model.OldPassword, person.Salt);
        if (person.Password != oldHashedPassword.HashedPassword)
            return Failure;

        var newHashedPassword = passwordHasher.Hash(request.Model.NewPassword, person.Salt);
        if (person.Password == newHashedPassword.HashedPassword)
            return new PersonUpdatePasswordResult.Failure.PasswordExist();

        var hashedPassword = passwordHasher.Hash(request.Model.NewPassword);
        person.UpdatePassword(hashedPassword.HashedPassword, hashedPassword.Salt);
        var updatingResult = await repository.UpdateAsync(person, cancellationToken);

        switch (updatingResult)
        {
            case PersonResult.Updating.Success:
                break;

            default:
                throw new NotImplementedException($"Unknown type of {nameof(PersonResult.Updating)}: {updatingResult.GetType()}");
        }

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