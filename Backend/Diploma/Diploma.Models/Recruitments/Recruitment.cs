using Diploma.Models.Dictionaries;
using Diploma.Models.Shared;
using System.ComponentModel.DataAnnotations;
using static Diploma.Models.ProjectRoles.ProjectRoleDto;

namespace Diploma.Models.Recruitments;

public sealed class RecruitmentQueryParameters : BaseQueryParameters
{
    public enum RecruitmentOrderBy
    {
        CreatedAt = 2,
    }

    [Range(1, 3)]
    public required int? StatusId { get; set; }

    public required RecruitmentOrderBy OrderBy { get; init; }
    public required Order Order { get; init; }
}

public abstract record RecruitmentQueryResult
{
    public abstract record Failure : RecruitmentQueryResult
    {
        public sealed record NotFound : Failure;
        public sealed record Forbidden : Failure;
        public sealed record ProfileInactive : Failure;
    };
    public sealed record Success(Response<RecruitmentDto> Response) : RecruitmentQueryResult;
}

public sealed record RecruitmentDto
{
    public sealed record ProjectRecruitmentDto
    {
        public required Guid ProjectId { get; init; }

        public required string Title { get; init; }
        public required string Description { get; init; }
        public required DateTimeOffset CreatedAt { get; init; }

        public required IList<DictionaryItem<string>> Disciplines { get; init; } = [];
        public required IList<Guid> EductionInstitutionIds { get; init; } = [];
    }


    public sealed record ProjectRoleRecruitmentDto
    {
        public required Guid ProjectRoleId { get; init; }

        public required string Title { get; init; }
        public required string Description { get; init; }
        public required DateTimeOffset CreatedAt { get; init; }

        public required IList<ProjectRoleDiscipline> Disciplines { get; init; } = [];
        public required IList<ProjectRoleEductionInstitution> EductionInstitutionIds { get; init; } = [];
    }

    public required Guid RecruitmentId { get; init; }
    public required DictionaryItem<int> Status { get; init; }
    public required ProjectRecruitmentDto? Project { get; init; }
    public required IEnumerable<ProjectRoleRecruitmentDto> ProjectRoles { get; init; }
}