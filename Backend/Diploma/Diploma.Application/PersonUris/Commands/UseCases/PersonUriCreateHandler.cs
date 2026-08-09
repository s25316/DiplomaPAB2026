using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.PersonUris.Aggregates;
using Diploma.Models.PersonUris;
using MediatR;

namespace Diploma.Application.PersonUris.Commands.UseCases;

public class PersonUriCreateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IPersonUriRepository uriRepository
    ) : IRequestHandler<PersonUriCreateHandler.Request, PersonUriCreateResult>
{
    public sealed record Request : IRequest<PersonUriCreateResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonUriCreateRequest Model { get; init; }
    }


    public async Task<PersonUriCreateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync();
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonUriCreateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonUriCreateResult.Failure.Forbidden();

        var uri = PersonUri.Create(
            request.PersonId,
            new Uri(request.Model.Uri),
            request.Model.Name,
            request.Model.Description
        );

        await uriRepository.CreateAsync(uri, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new PersonUriCreateResult.Success();
    }
}