using Diploma.Application.Interfaces.Database;
using Diploma.Domain.PersonEmployments.Aggregates;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEmployments;
using MediatR;

namespace Diploma.Application.PersonEmployments.Commands.UseCases;

public class PersonEmploymentDeleteHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IPersonEmploymentService employmentService
    ) : IRequestHandler<PersonEmploymentDeleteHandler.Request, PersonEmploymentDeleteResult>
{
    public sealed record Request : IRequest<PersonEmploymentDeleteResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid EmploymentId { get; init; }
    }


    public async Task<PersonEmploymentDeleteResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonEmploymentDeleteResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonEmploymentDeleteResult.Failure.Forbidden();

        var employmentResult = await employmentService.GetAsync(request.EmploymentId, cancellationToken);

        if (!employmentResult.HasValue)
            return new PersonEmploymentDeleteResult.Failure.NotFound();

        var result = await employmentService.DeleteAsync(employmentResult.Value, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return result switch
        {
            PersonEmploymentServiceResult.Sucess => new PersonEmploymentDeleteResult.Success(),
            PersonEmploymentServiceResult.Failure.NotExist => new PersonEmploymentDeleteResult.Failure.NotFound(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEmploymentServiceResult)}: {result.GetType()}")
        };
    }
}