using Diploma.Application.Interfaces.Database;
using Diploma.Domain.PersonEducations.Aggregates;
using Diploma.Domain.PersonEducations.ValueObjects;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEducations;
using Diploma.Shared.Semesters;
using MediatR;

namespace Diploma.Application.PersonEducations.Commands.UseCases;

public class PersonEducationCreateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IPersonEducationService service
    ) : IRequestHandler<PersonEducationCreateHandler.Request, PersonEducationCreateResult>
{
    public sealed record Request : IRequest<PersonEducationCreateResult>
    {
        public required Guid PersonId { get; init; }
        public required PersonEducationCreateRequest Model { get; init; }
    }


    public async Task<PersonEducationCreateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonEducationCreateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonEducationCreateResult.Failure.Forbidden();

        var personEmployment = PersonEducation.Create(
            request.PersonId,
            request.Model.EducationCourseId,
            request.Model.EducationCourseInstanceId,
            new EducationSemestr(request.Model.Start.Year, Semester.FromId(request.Model.Start.SemestrId)),
            request.Model.End is not null
                ? new EducationSemestr(request.Model.End.Year, Semester.FromId(request.Model.End.SemestrId))
                : null
        );

        var result = await service.CreateAsync(personEmployment, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return result switch
        {
            PersonEducationServiceResult.Sucess => new PersonEducationCreateResult.Success(),
            PersonEducationServiceResult.Failure.OverLimit overLimit => new PersonEducationCreateResult.Failure.OverLimit(overLimit.MaxCount),
            PersonEducationServiceResult.Failure.NotExist => new PersonEducationCreateResult.Failure.NotFound(),
            PersonEducationServiceResult.Failure.NotExistCourseInstance notExistCourseInstance => new PersonEducationCreateResult.Failure.NotFoundCourseInstance(notExistCourseInstance.CourseInstanceId, notExistCourseInstance.CourseId?.Value),
            PersonEducationServiceResult.Failure.NotExistCourse notExistCourse => new PersonEducationCreateResult.Failure.NotFoundCourse(notExistCourse.Id),
            PersonEducationServiceResult.Failure.InvalidCourseInstanceDates invalidCourseInstanceDates => new PersonEducationCreateResult.Failure.InvalidCourseInstanceDates(
                invalidCourseInstanceDates.StartDate,
                invalidCourseInstanceDates.EndDate
            ),
            PersonEducationServiceResult.Failure.InvalidCourseDates invalidCourseDates => new PersonEducationCreateResult.Failure.InvalidCourseDates(
                invalidCourseDates.StartDate,
                invalidCourseDates.EndDate
            ),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEducationServiceResult)}: {result.GetType()}")
        };
    }
}