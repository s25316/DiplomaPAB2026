using Microsoft.AspNetCore.Mvc;
using RADON.Application.Interfaces.Courses.Dictionaries;
using RADON.Models.Dictionaries.Responses;

namespace RADON.API.Controllers;

[Route("api/courses")]
[ApiController]
public class CoursesController : ControllerBase
{
    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("forms")]
    public async Task<IActionResult> GetCourseFormsAsync(
        [FromServices] ICourseFormRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
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
    [HttpGet("languages")]
    public async Task<IActionResult> GetLanguagesAsync(
        [FromServices] ILanguageRepository repository,
        CancellationToken cancellationToken)
    {
        var items = await repository.GetAsync(cancellationToken);
        return Ok(items);
    }


    [ProducesResponseType(typeof(IEnumerable<DictionaryItem>), 200)]
    [ProducesResponseType(500)]
    [HttpGet("titles")]
    public async Task<IActionResult> GetProfessionalTitlesAsync(
        [FromServices] IProfessionalTitleRepository repository,
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
}