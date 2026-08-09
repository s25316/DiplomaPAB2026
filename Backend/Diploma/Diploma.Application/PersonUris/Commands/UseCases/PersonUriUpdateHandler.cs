using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.PersonUris.Aggregates;
using Diploma.Models.PersonUris;
using MediatR;

namespace Diploma.Application.PersonUris.Commands.UseCases;

public class PersonUriUpdateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IPersonUriRepository uriRepository
    ) : IRequestHandler<PersonUriUpdateHandler.Request, PersonUriUpdateResult>
{
    public sealed record Request : IRequest<PersonUriUpdateResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid UriId { get; init; }
        public required PersonUriUpdateRequest Model { get; init; }
    }

    public async Task<PersonUriUpdateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync();
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonUriUpdateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonUriUpdateResult.Failure.Forbidden();

        var uriResult = await uriRepository.GetAsync(request.UriId, cancellationToken);

        if (!uriResult.HasValue)
            return new PersonUriUpdateResult.Failure.NotFound();

        var uri = uriResult.Value;

        if (uri.PersonId.Value != request.PersonId)
            return new PersonUriUpdateResult.Failure.Forbidden();

        uri.Name = request.Model.Name;
        uri.Description = request.Model.Description;

        await uriRepository.UpdateAsync(uri, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new PersonUriUpdateResult.Success();
    }
}