using Diploma.Domain.PersonEmployments.Aggregates;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEmployments;
using MediatR;

namespace Diploma.Application.PersonEmployments.Commands.UseCases;

public class PersonEmploymentUpdateHandler(
    IPersonRepository personRepository,
    IPersonEmploymentService employmentService
    ) : IRequestHandler<PersonEmploymentUpdateHandler.Request, PersonEmploymentUpdateResult>
{
    public sealed record Request : IRequest<PersonEmploymentUpdateResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid EmploymentId { get; init; }
        public required PersonEmploymentUpdateRequest Model { get; init; }
    }


    public async Task<PersonEmploymentUpdateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonEmploymentUpdateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonEmploymentUpdateResult.Failure.Forbidden();

        var employmentResult = await employmentService.GetAsync(request.EmploymentId, cancellationToken);

        if (!employmentResult.HasValue)
            return new PersonEmploymentUpdateResult.Failure.NotFound();

        var employment = employmentResult.Value;

        employment.Position = request.Model.Position;
        employment.Description = request.Model.Descrition;
        employment.UpdateDates(
            request.Model.From,
            request.Model.To
        );

        var result = await employmentService.UpdateAsync(employment, cancellationToken);

        return result switch
        {
            PersonEmploymentServiceResult.Sucess => new PersonEmploymentUpdateResult.Success(),
            PersonEmploymentServiceResult.Failure.NotExist => new PersonEmploymentUpdateResult.Failure.NotFound(),
            PersonEmploymentServiceResult.Failure.NotExistCompany notExistCompany => new PersonEmploymentUpdateResult.Failure.NotFoundCompany(notExistCompany.Regon),
            PersonEmploymentServiceResult.Failure.InvalidCompanyDates invalidCompanyDates => new PersonEmploymentUpdateResult.Failure.InvalidCompanyDates(invalidCompanyDates.Start, invalidCompanyDates.End),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEmploymentServiceResult)}: {result.GetType()}")
        };
    }
}