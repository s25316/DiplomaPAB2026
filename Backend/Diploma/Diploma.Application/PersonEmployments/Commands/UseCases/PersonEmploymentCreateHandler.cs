using Diploma.Application.Interfaces.Database;
using Diploma.Domain.PersonEmployments.Aggregates;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEmployments;
using MediatR;

namespace Diploma.Application.PersonEmployments.Commands.UseCases;

public class PersonEmploymentCreateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IPersonEmploymentService employmentService
    ) : IRequestHandler<PersonEmploymentCreateHandler.Request, PersonEmploymentCreateResult>
{
    public sealed record Request : IRequest<PersonEmploymentCreateResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonEmploymentCreateRequest Model { get; init; }
    }


    public async Task<PersonEmploymentCreateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync();
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonEmploymentCreateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonEmploymentCreateResult.Failure.Forbidden();

        var personEmployment = PersonEmployment.Create(
            request.PersonId,
            request.Model.Regon,
            request.Model.Position,
            request.Model.Descrition,
            request.Model.From,
            request.Model.To
        );

        var result = await employmentService.CreateAsync(personEmployment, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return result switch
        {
            PersonEmploymentServiceResult.Sucess => new PersonEmploymentCreateResult.Success(),
            PersonEmploymentServiceResult.Failure.NotExist => new PersonEmploymentCreateResult.Failure.NotFound(),
            PersonEmploymentServiceResult.Failure.NotExistCompany notExistCompany => new PersonEmploymentCreateResult.Failure.NotFoundCompany(notExistCompany.Regon),
            PersonEmploymentServiceResult.Failure.InvalidCompanyDates invalidCompanyDates => new PersonEmploymentCreateResult.Failure.InvalidCompanyDates(invalidCompanyDates.Start, invalidCompanyDates.End),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEmploymentServiceResult)}: {result.GetType()}")
        };
    }
}