using Diploma.Models.Educations;

namespace Diploma.Models.PersonEducations;

public abstract record PersonDisciplineQueryResult
{
    public abstract record Failure : PersonDisciplineQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(IEnumerable<EducationDisciplineDto> Response) : PersonDisciplineQueryResult;
}