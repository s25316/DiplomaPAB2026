using Microsoft.AspNetCore.Mvc;
using RADON.Application.Interfaces.Courses;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Contracts.Shared.Responses;
using RADON.Models.Courses;
using RADON.Models.Courses.Responses;
using RADON.Models.Dictionaries.Responses;

namespace RADON.API.Controllers;

[Route("api/courses")]
[ApiController]
public class CoursesController : ControllerBase
{
    [ProducesResponseType(typeof(Response<Course>), 200)]
    [ProducesResponseType(500)]
    [HttpGet()]
    public async Task<IActionResult> GetInstitutionKindsAsync(
        [FromServices] ICourseRepository repository,
        [FromQuery] QueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(queryParameters, cancellationToken);
        return Ok(response);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("levels")]
    public async Task<IActionResult> GetCourseLevelsAsync(
        [FromServices] ICourseLevelRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("profiles")]
    public async Task<IActionResult> GetCourseProfilesAsync(
        [FromServices] ICourseProfileRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("isceds")]
    public async Task<IActionResult> GetIscedsAsync(
        [FromServices] IIscedRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("statuses")]
    public async Task<IActionResult> GetCourseStatusesAsync(
        [FromServices] ICourseStatusRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("instances/statuses")]
    public async Task<IActionResult> GetInstanceStatusesAsync(
        [FromServices] ICourseInstanceStatusRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }

    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("instances/forms")]
    public async Task<IActionResult> GetCourseFormsAsync(
        [FromServices] ICourseFormRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("instances/languages")]
    public async Task<IActionResult> GetLanguagesAsync(
        [FromServices] ILanguageRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("instances/titles")]
    public async Task<IActionResult> GetProfessionalTitlesAsync(
        [FromServices] IProfessionalTitleRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }
}