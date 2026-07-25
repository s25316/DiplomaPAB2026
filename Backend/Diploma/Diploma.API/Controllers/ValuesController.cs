using Diploma.Domain.EducationCourseInstances.Aggregates;
using Diploma.Domain.EducationCourses.Aggregates;
using Diploma.Domain.EducationDisciplines.ValueObjects;
using Diploma.Domain.EducationInstitutions.Aggregates;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("EducationInstitution")]
        public async Task<IActionResult> GetASync1(
            Guid id,
            IEducationInstitutionRepository repository,
            CancellationToken cancellationToken)
        {
            var item = await repository.GetAsync(id, cancellationToken);
            return Ok(item);
        }


        [HttpGet("EducationDiscipline")]
        public async Task<IActionResult> GetASync4(
            IEducationDisciplineRepository repository,
            CancellationToken cancellationToken)
        {
            var items = await repository.GetAsync(cancellationToken);
            return Ok(items);
        }


        [HttpGet("EducationCourse")]
        public async Task<IActionResult> GetASync2(
            Guid id,
            IEducationCourseRepository repository,
            CancellationToken cancellationToken)
        {
            var item = await repository.GetAsync(id, cancellationToken);
            return Ok(item);
        }


        [HttpGet("EducationCourseInstance")]
        public async Task<IActionResult> GetASync3(
            Guid id,
            IEducationCourseInstanceRepository repository,
            CancellationToken cancellationToken)
        {
            var item = await repository.GetAsync(id, cancellationToken);
            return Ok(item);
        }
    }
}
