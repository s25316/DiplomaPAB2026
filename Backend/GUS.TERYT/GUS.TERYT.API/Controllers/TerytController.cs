// Ignore spelling: api, Teryt, wojewodztwa, powiaty, gminy, miejscowosci
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Models.Requests.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace GUS.TERYT.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TerytController : ControllerBase
{
    [HttpGet("wojewodztwa")]
    public async Task<IActionResult> GetWojewodztwaAsync(
        [FromServices] IWojewodztwoRepository repository,
        [FromQuery] WojewodztwoParameters parameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(parameters, cancellationToken);
        return Ok(response);
    }

    [HttpGet("powiaty")]
    public async Task<IActionResult> GetPowiatyAsync(
        [FromServices] IPowiatRepository repository,
        [FromQuery] PowiatParameters parameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(parameters, cancellationToken);
        return Ok(response);
    }

    [HttpGet("powiaty/types")]
    public async Task<IActionResult> GetPowiatyTypesAsync(
        [FromServices] IPowiatTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(cancellationToken);
        return Ok(response);
    }

    [HttpGet("gminy")]
    public async Task<IActionResult> GetPowiatyAsync(
        [FromServices] IGminaRepository repository,
        [FromQuery] GminaParameters parameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(parameters, cancellationToken);
        return Ok(response);
    }

    [HttpGet("gminy/types")]
    public async Task<IActionResult> GetPowiatyTypesAsync(
        [FromServices] IGminaTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(cancellationToken);
        return Ok(response);
    }

    [HttpGet("miejscowosci")]
    public async Task<IActionResult> GetMiejscowosciAsync(
        [FromServices] IMiejscowoscRepository repository,
        [FromQuery] MiejscowoscParameters parameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(parameters, cancellationToken);
        return Ok(response);
    }

    [HttpGet("miejscowosci/types")]
    public async Task<IActionResult> GetMiejscowosciTypesAsync(
        [FromServices] IMiejscowoscTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(cancellationToken);
        return Ok(response);
    }
}
