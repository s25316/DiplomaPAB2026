using Diploma.Application.Interfaces.Database;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Domain.PersonUris.Aggregates;
using Diploma.Models.PersonUris;
using MediatR;

namespace Diploma.Application.PersonUris.Commands.UseCases;

public class PersonUriDeleteHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IPersonUriRepository uriRepository
    ) : IRequestHandler<PersonUriDeleteHandler.Request, PersonUriDeleteResult>
{
    public sealed record Request : IRequest<PersonUriDeleteResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid UriId { get; init; }
    }


    public async Task<PersonUriDeleteResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync();
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonUriDeleteResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonUriDeleteResult.Failure.Forbidden();

        var uriResult = await uriRepository.GetAsync(request.UriId, cancellationToken);

        if (!uriResult.HasValue)
            return new PersonUriDeleteResult.Failure.NotFound();

        var uri = uriResult.Value;

        if (uri.PersonId.Value != request.PersonId)
            return new PersonUriDeleteResult.Failure.Forbidden();

        await uriRepository.DeleteAsync(uri, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new PersonUriDeleteResult.Success();
    }
}