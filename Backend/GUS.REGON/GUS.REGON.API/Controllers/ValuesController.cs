using GUS.REGON.Application.Interfaces;
using GUS.REGON.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GUS.REGON.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController(IReportRepository reportRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] InputParameters parameters, CancellationToken cancellationToken)
        {
            var items = await reportRepository.GetAsync(parameters, cancellationToken);
            return Ok(items);
        }
    }
}
