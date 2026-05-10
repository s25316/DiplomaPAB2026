using Microsoft.EntityFrameworkCore;
using RADON.Database;
using RADON.Database.Models.Courses;
using RADON.Infrastructure.QueryBuilders.Base;
using RADON.Models.Shared;
using static RADON.Models.Courses.QueryParameters;

namespace RADON.Infrastructure.QueryBuilders;

public sealed class CourseQueryBuilder(RadonDbContext context) : BaseQueryBuilder<Course>(context
    .Courses
    .AsNoTracking()

    .Include(i => i.DataSource)
    .Include(i => i.CourseLevel)
    .Include(i => i.CourseProfile)
    .Include(i => i.Isced)
    .Include(i => i.CourseStatus)

    .Include(i => i.Disciplines)
    .ThenInclude(i => i.Discipline)

    .Include(i => i.CourseInstances)
    .ThenInclude(i => i.CourseForm)

    .Include(i => i.CourseInstances)
    .ThenInclude(i => i.ProfessionalTitle)

    .Include(i => i.CourseInstances)
    .ThenInclude(i => i.Language)

    .Include(i => i.CourseInstances)
    .ThenInclude(i => i.CourseInstanceStatus)

    .Include(i => i.CourseInstances)
    .ThenInclude(i => i.PhilologicalLanguages))
{
    public CourseQueryBuilder WithCourseUuids(IEnumerable<Guid> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i => values.Contains(i.CourseUuid)));
        return this;
    }


    public CourseQueryBuilder WithInstitutionUuids(IEnumerable<Guid> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i => values.Contains(i.InstitutionUuid)));
        return this;
    }


    public CourseQueryBuilder WithName(string? value)
    {
        var searchWords = SplitToUpperInvariant(value);
        if (!searchWords.Any())
            return this;

        With(query => query.Where(i => searchWords.Any(w =>
                i.Name.ToUpper().Contains(w)
            )
        ));
        return this;
    }


    public CourseQueryBuilder WithIsTeacherTraining(bool? value)
    {
        if (!value.HasValue)
            return this;

        With(query => query.Where(i => i.IsTeacherTraining == value.Value));
        return this;
    }


    public CourseQueryBuilder WithIsPhilological(bool? value)
    {
        if (!value.HasValue)
            return this;

        With(query => query.Where(i => i.IsPhilological == value.Value));
        return this;
    }


    public CourseQueryBuilder WithLevelCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i => values.Contains(i.CourseLevelCode)));
        return this;
    }


    public CourseQueryBuilder WithProfileCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i => values.Contains(i.CourseProfileCode)));
        return this;
    }


    public CourseQueryBuilder WithIscedCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i => values.Contains(i.IscedCode)));
        return this;
    }


    public CourseQueryBuilder WithStatusCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i => values.Contains(i.CourseStatusCode)));
        return this;
    }


    public CourseQueryBuilder WithDisciplineCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i =>
            i.Disciplines.Any(d =>
                values.Contains(d.DisciplineCode)
            )
        ));
        return this;
    }



    public CourseQueryBuilder WithCourseInstanceUuids(IEnumerable<Guid> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i =>
            i.CourseInstances.Any(ci =>
                values.Contains(ci.CourseInstanceUuid)
            )
        ));
        return this;
    }


    public CourseQueryBuilder WithIsDual(bool? value)
    {
        if (!value.HasValue)
            return this;

        With(query => query.Where(i =>
            i.CourseInstances.Any(ci => ci.IsDual == value.Value)
        ));
        return this;
    }


    public CourseQueryBuilder WithIsBridging(bool? value)
    {
        if (!value.HasValue)
            return this;

        With(query => query.Where(i =>
            i.CourseInstances.Any(ci => ci.IsBridging == value.Value)
        ));
        return this;
    }


    public CourseQueryBuilder WithIsCoopWithVocational(bool? value)
    {
        if (!value.HasValue)
            return this;

        With(query => query.Where(i =>
            i.CourseInstances.Any(ci => ci.IsCoopWithVocational == value.Value)
        ));
        return this;
    }

    public CourseQueryBuilder WithFormCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i =>
            i.CourseInstances.Any(ci =>
                values.Contains(ci.CourseFormCode)
            )
        ));
        return this;
    }


    public CourseQueryBuilder WithProfessionalTitleCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i =>
            i.CourseInstances.Any(ci =>
                values.Contains(ci.ProfessionalTitleCode)
            )
        ));
        return this;
    }


    public CourseQueryBuilder WithLanguageCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i =>
            i.CourseInstances.Any(ci =>
                values.Contains(ci.LanguageCode)
            )
        ));
        return this;
    }


    public CourseQueryBuilder WithInstanceStatusCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i =>
            i.CourseInstances.Any(ci =>
                values.Contains(ci.CourseInstanceStatusCode)
            )
        ));
        return this;
    }


    public CourseQueryBuilder WithPhilologicalLanguageCodes(IEnumerable<string> values)
    {
        if (!values.Any())
            return this;

        With(query => query.Where(i =>
            i.CourseInstances.Any(ci =>
                ci.PhilologicalLanguages.Any(pl =>
                    values.Contains(pl.LanguageCode)
                )
            )
        ));
        return this;
    }

    public CourseQueryBuilder WithOrderBy(
        QueryParametersOrderBy orderBy,
        Order order,
        QueryParametersPagination pagination)
    {
        With(query => orderBy switch
        {
            QueryParametersOrderBy.Name => order == Order.Ascending
                ? query.OrderBy(i => i.Name)
                : query.OrderByDescending(i => i.Name),

            QueryParametersOrderBy.CreationDate => order == Order.Ascending
                ? query.OrderBy(i => i.CreationDate ?? DateOnly.MinValue)
                : query.OrderByDescending(i => i.CreationDate ?? DateOnly.MinValue),

            QueryParametersOrderBy.InstanceStartDate => order == Order.Ascending
                ? query.OrderBy(i =>
                    i
                    .CourseInstances
                    .OrderBy(ci => ci.EducationStartDate)
                    .First()
                    .EducationStartDate
                  )
                : query.OrderByDescending(i =>
                    i
                    .CourseInstances
                    .OrderBy(ci => ci.EducationStartDate)
                    .First()
                    .EducationStartDate),

            _ => throw new NotImplementedException(orderBy.ToString())
        });
        Paginate(pagination);
        return this;
    }
}