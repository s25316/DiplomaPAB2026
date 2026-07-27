using Diploma.Domain.Base.Results;
using Diploma.Domain.EducationCourseInstances.Aggregates;
using Diploma.Domain.EducationCourses.Aggregates;

namespace Diploma.Domain.PersonEducations.Aggregates;

public abstract record PersonEducationServiceResult
{
    public sealed record Sucess : PersonEducationServiceResult;
    public abstract record Failure : PersonEducationServiceResult
    {
        public sealed record OverLimit(int MaxCount, int CurrentCount) : Failure;
        public sealed record NotExist : Failure;
        public sealed record NotExistCourseInstance(EducationCourseInstanceId Id) : Failure;
        public sealed record InvalidCourseInstanceDates(DateOnly? StartDate, DateOnly? EndDate) : Failure;
        public sealed record NotExistCourse(EducationCourseId Id) : Failure;
        public sealed record InvalidCourseDates(DateOnly? StartDate, DateOnly? EndDate) : Failure;

    }
}

public interface IPersonEducationService
{
    Task<OptionalResult<PersonEducation>> GetAsync(PersonEducationId id, CancellationToken cancellationToken = default);
    Task<PersonEducationServiceResult> CreateAsync(PersonEducation item, CancellationToken cancellationToken = default);
    Task<PersonEducationServiceResult> UpdateAsync(PersonEducation item, CancellationToken cancellationToken = default);
    Task<PersonEducationServiceResult> DeleteAsync(PersonEducation item, CancellationToken cancellationToken = default);
}

public class PersonEducationService(
    IEducationCourseRepository courseRepository,
    IEducationCourseInstanceRepository courseInstanceRepository,
    IPersonEducationRepository personEducationRepository
    ) : IPersonEducationService
{
    private const int MAX_COUNT = 100;

    private static readonly PersonEducationServiceResult.Sucess Sucess = new();
    private static readonly PersonEducationServiceResult.Failure.NotExist NotExist = new();

    public async Task<OptionalResult<PersonEducation>> GetAsync(
        PersonEducationId id,
        CancellationToken cancellationToken = default
    ) => await personEducationRepository.GetAsync(id, cancellationToken);

    public async Task<PersonEducationServiceResult> DeleteAsync(
        PersonEducation item,
        CancellationToken cancellationToken = default)
    {
        var result = await personEducationRepository.DeleteAsync(item, cancellationToken);

        if (result.IsNotFound)
            return NotExist;

        return Sucess;
    }

    public async Task<PersonEducationServiceResult> CreateAsync(
        PersonEducation item,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await personEducationRepository.TotalCountAsync(item.PersonId, cancellationToken);

        if (totalCount >= MAX_COUNT)
            return new PersonEducationServiceResult.Failure.OverLimit(MAX_COUNT, totalCount);

        var result = await IsValidAsync(item, cancellationToken);

        if (result is not null)
            return result;

        await personEducationRepository.CreateAsync(item, cancellationToken);
        return Sucess;
    }

    public async Task<PersonEducationServiceResult> UpdateAsync(
        PersonEducation item,
        CancellationToken cancellationToken = default)
    {
        var result = await IsValidAsync(item, cancellationToken);

        if (result is not null)
            return result;

        var updatingResult = await personEducationRepository.UpdateAsync(item, cancellationToken);

        if (updatingResult.IsNotFound)
            return NotExist;

        return Sucess;
    }

    private async Task<PersonEducationServiceResult.Failure?> IsValidAsync(
        PersonEducation item,
        CancellationToken cancellationToken = default)
    {
        if (item.EducationCourseInstanceId is not null)
        {
            var courseInstanceResult = await courseInstanceRepository.GetAsync(
                item.EducationCourseInstanceId,
                cancellationToken
            );

            if (!courseInstanceResult.HasValue)
                return new PersonEducationServiceResult.Failure.NotExistCourseInstance(item.EducationCourseInstanceId);

            var courseInstance = courseInstanceResult.Value;

            if (item.Start.SmesterStart < courseInstance.EducationStartDate)
            {
                return new PersonEducationServiceResult.Failure.InvalidCourseInstanceDates(
                    courseInstance.EducationStartDate,
                    courseInstance.LiquidationDate
                );
            }

            if (item.End is not null &&
                courseInstance.LiquidationDate.HasValue &&
                item.End.SmesterStart > courseInstance.LiquidationDate)
            {
                return new PersonEducationServiceResult.Failure.InvalidCourseInstanceDates(
                    courseInstance.EducationStartDate,
                    courseInstance.LiquidationDate
                );
            }

            return null;
        }


        var courseResult = await courseRepository.GetAsync(
            item.EducationCourseId,
            cancellationToken
        );

        if (!courseResult.HasValue)
            return new PersonEducationServiceResult.Failure.NotExistCourse(item.EducationCourseId);

        var course = courseResult.Value;

        if (course.CreationDate.HasValue &&
            item.Start.SmesterStart < course.CreationDate)
        {
            return new PersonEducationServiceResult.Failure.InvalidCourseDates(
                course.CreationDate,
                course.LiquidationDate
            );
        }

        if (item.End is not null &&
            course.LiquidationDate.HasValue &&
            item.End.SmesterStart < course.LiquidationDate)
        {
            return new PersonEducationServiceResult.Failure.InvalidCourseDates(
                course.CreationDate,
                course.LiquidationDate
            );
        }

        return null;
    }
}