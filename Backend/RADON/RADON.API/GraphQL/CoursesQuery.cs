using HotChocolate;
using HotChocolate.Types;
using RADON.Application.Interfaces.Courses;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Models.Courses;
using RADON.Models.Courses.Responses;
using RADON.Models.Dictionaries.Responses;
using RADON.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace RADON.API.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]
public class CoursesQuery
{
    [Display(Name = nameof(ApiDescription.GetCourses_Description), ResourceType = typeof(ApiDescription))]
    public async Task<Response<Course>> GetCourses(
        [Service] ICourseRepository repository,
        QueryParameters queryParameters,
        CancellationToken cancellationToken)
        => await repository.GetAsync(queryParameters, cancellationToken);


    [Display(Name = nameof(ApiDescription.GetCourseLevels_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetCourseLevels(
        [Service] ICourseLevelRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);


    [Display(Name = nameof(ApiDescription.GetCourseProfiles_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetCourseProfiles(
        [Service] ICourseProfileRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);


    [Display(Name = nameof(ApiDescription.GetIsceds_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetIsceds(
        [Service] IIscedRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);


    [Display(Name = nameof(ApiDescription.GetCourseStatuses_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetCourseStatuses(
        [Service] ICourseStatusRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);

    [Display(Name = nameof(ApiDescription.GetInstanceStatuses_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetInstanceStatuses(
        [Service] ICourseInstanceStatusRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);


    [Display(Name = nameof(ApiDescription.GetCourseForms_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetCourseForms(
        [Service] ICourseFormRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);


    [Display(Name = nameof(ApiDescription.GetLanguages_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetLanguages(
        [Service] ILanguageRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);


    [Display(Name = nameof(ApiDescription.GetProfessionalTitles_Summary), ResourceType = typeof(ApiDescription))]
    public async Task<IDictionary<string, DictionaryItem>> GetProfessionalTitles(
        [Service] IProfessionalTitleRepository repository,
        CancellationToken cancellationToken)
        => await repository.GetAsync(cancellationToken);
}