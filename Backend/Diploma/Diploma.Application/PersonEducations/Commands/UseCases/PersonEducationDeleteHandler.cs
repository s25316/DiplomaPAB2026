using Diploma.Domain.PersonEducations.Aggregates;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEducations;
using MediatR;

namespace Diploma.Application.PersonEducations.Commands.UseCases;

public class PersonEducationDeleteHandler(
    IPersonRepository personRepository,
    IPersonEducationService service
    ) : IRequestHandler<PersonEducationDeleteHandler.Request, PersonEducationDeleteResult>
{
    public sealed record Request : IRequest<PersonEducationDeleteResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid EducationId { get; init; }
    }


    public async Task<PersonEducationDeleteResult> Handle(Request request, CancellationToken cancellationToken)
    {
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonEducationDeleteResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonEducationDeleteResult.Failure.Forbidden();

        var educationResult = await service.GetAsync(request.EducationId, cancellationToken);

        if (!educationResult.HasValue)
            return new PersonEducationDeleteResult.Failure.NotFound();

        var education = educationResult.Value;
        var result = await service.DeleteAsync(education, cancellationToken);

        return result switch
        {
            PersonEducationServiceResult.Sucess => new PersonEducationDeleteResult.Success(),
            PersonEducationServiceResult.Failure.NotExist => new PersonEducationDeleteResult.Failure.NotFound(),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEducationServiceResult)}: {result.GetType()}")
        };
    }
}