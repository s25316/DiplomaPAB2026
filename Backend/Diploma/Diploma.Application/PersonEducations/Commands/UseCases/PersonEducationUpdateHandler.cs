using Diploma.Application.Interfaces.Database;
using Diploma.Domain.PersonEducations.Aggregates;
using Diploma.Domain.PersonEducations.ValueObjects;
using Diploma.Domain.Persons.Aggregates;
using Diploma.Models.PersonEducations;
using Diploma.Shared.Semesters;
using MediatR;

namespace Diploma.Application.PersonEducations.Commands.UseCases;

public class PersonEducationUpdateHandler(
    IUnitOfWorkFactory unitOfWorkFactory,
    IPersonRepository personRepository,
    IPersonEducationService service
    ) : IRequestHandler<PersonEducationUpdateHandler.Request, PersonEducationUpdateResult>
{
    public sealed record Request : IRequest<PersonEducationUpdateResult>
    {
        public required Guid PersonId { get; init; }
        public required Guid EducationId { get; init; }
        public required PersonEducationUpdateRequest Model { get; init; }
    }


    public async Task<PersonEducationUpdateResult> Handle(Request request, CancellationToken cancellationToken)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync();
        var personResult = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (!personResult.HasValue)
            return new PersonEducationUpdateResult.Failure.NotFound();

        var person = personResult.Value;

        if (!person.HasActive)
            return new PersonEducationUpdateResult.Failure.Forbidden();

        var educationResult = await service.GetAsync(request.EducationId, cancellationToken);

        if (!educationResult.HasValue)
            return new PersonEducationUpdateResult.Failure.NotFound();

        var education = educationResult.Value;

        education.UpdateSemestrs(
            new EducationSemestr(request.Model.Start.Year, Semester.FromId(request.Model.Start.SemestrId)),
            request.Model.End is not null
                ? new EducationSemestr(request.Model.End.Year, Semester.FromId(request.Model.End.SemestrId))
                : null
        );

        var result = await service.UpdateAsync(education, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return result switch
        {
            PersonEducationServiceResult.Sucess => new PersonEducationUpdateResult.Success(),
            PersonEducationServiceResult.Failure.OverLimit overLimit => new PersonEducationUpdateResult.Failure.OverLimit(overLimit.MaxCount),
            PersonEducationServiceResult.Failure.NotExist => new PersonEducationUpdateResult.Failure.NotFound(),
            PersonEducationServiceResult.Failure.NotExistCourseInstance notExistCourseInstance => new PersonEducationUpdateResult.Failure.NotFoundCourseInstance(notExistCourseInstance.CourseInstanceId, notExistCourseInstance.CourseId?.Value),
            PersonEducationServiceResult.Failure.NotExistCourse notExistCourse => new PersonEducationUpdateResult.Failure.NotFoundCourse(notExistCourse.Id),
            PersonEducationServiceResult.Failure.InvalidCourseInstanceDates invalidCourseInstanceDates => new PersonEducationUpdateResult.Failure.InvalidCourseInstanceDates(
                invalidCourseInstanceDates.StartDate,
                invalidCourseInstanceDates.EndDate
            ),
            PersonEducationServiceResult.Failure.InvalidCourseDates invalidCourseDates => new PersonEducationUpdateResult.Failure.InvalidCourseDates(
                invalidCourseDates.StartDate,
                invalidCourseDates.EndDate
            ),
            _ => throw new NotImplementedException($"Unknown type of {nameof(PersonEducationServiceResult)}: {result.GetType()}")
        };
    }
}