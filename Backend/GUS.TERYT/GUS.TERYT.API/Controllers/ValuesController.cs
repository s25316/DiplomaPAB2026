using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Models.Requests.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace GUS.TERYT.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController(IGminaTypeRepository gminaTypeRepository) : ControllerBase
{
    [HttpGet("test")]
    public async Task<IActionResult> GetTercAsync(
        [FromQuery] WojewodztwoParameters parameters,
        CancellationToken cancellationToken)
    {
        return Ok(parameters);
    }

    [HttpGet("test2")]
    public async Task<IActionResult> GetTercAsync2(
        [FromQuery] WojewodztwoParameters parameters,
        CancellationToken cancellationToken)
    {
        var dic = await gminaTypeRepository.GetAsync(cancellationToken);
        return Ok(dic);
    }

}
