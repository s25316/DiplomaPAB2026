using GUS.TERYT.Models.Requests.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace GUS.TERYT.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet("test")]
    public async Task<IActionResult> GetTercAsync(
        [FromQuery] WojewodztwoParameters parameters,
        CancellationToken cancellationToken)
    {
        return Ok(parameters);
    }

}
