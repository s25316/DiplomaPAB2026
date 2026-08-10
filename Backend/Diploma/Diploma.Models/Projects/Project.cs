using Diploma.Models.Dictionaries;
using Diploma.Models.Shared;

namespace Diploma.Models.Projects;


public class ProjectQueryParameters : BaseQueryParameters
{
    public enum ProjectOrderBy
    {
        Title = 1,
        CreatedAt = 2,
        //Disciplines = 2,
    }

    public required IList<string> Disciplines { get; init; } = [];
    public required IList<Guid> Institutions { get; init; } = [];
    public required IList<Guid> ProjectIds { get; init; } = [];

    public required ProjectOrderBy OrderBy { get; init; }
    public required Order Order { get; init; } = Order.Ascending;
}

/*
public abstract record PersonUriQueryResult
{
    public abstract record Failure : PersonUriQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(Response<PersonUriDto> Response) : PersonUriQueryResult;
}
*/
public sealed record ProjectDto
{
    public sealed record ProjectDiscipline
    {
        public required Guid ProjectDisciplineId { get; init; }
        public required DictionaryItem<string> Discipline { get; init; }
    }

    public sealed record ProjectEductionInstitution
    {
        public required Guid ProjectDisciplineId { get; init; }
        public required Guid EductionInstitutionId { get; init; }
    }


    public required Guid ProjectId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required bool IsVisible { get; init; }
    public required bool IsAvailableRecruitment { get; init; }

    public required IList<ProjectDiscipline> Disciplines { get; init; } = [];
    public required IList<ProjectEductionInstitution> EductionInstitutions { get; init; } = [];
}