// Ignore spelling: api, Teryt, wojewodztwa, Powiaty, Gminy, Miejscowosci, Ulicy
using Base.Models.Interfaces.Repositories;
using GUS.TERYT.Application.Repositories;
using GUS.TERYT.Models.Requests.Parameters;
using GUS.TERYT.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GUS.TERYT.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TerytController : ControllerBase
{
    [HttpGet("wojewodztwa")]
    [ProducesResponseType(typeof(Response<Wojewodztwo>.ManyItems), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWojewodztwaAsync(
        [FromServices] IWojewodztwoRepository repository,
        [FromQuery] WojewodztwoParameters parameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(parameters, cancellationToken);
        return Ok(response);
    }


    [HttpGet("powiaty")]
    [ProducesResponseType(typeof(Response<Powiat>.ManyItems), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPowiatyAsync(
        [FromServices] IPowiatRepository repository,
        [FromQuery] PowiatParameters parameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(parameters, cancellationToken);
        return Ok(response);
    }


    [HttpGet("powiaty/types")]
    [ProducesResponseType(typeof(IDictionary<int, Powiat.Type>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPowiatyTypesAsync(
        [FromServices] IPowiatTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(cancellationToken);
        return Ok(response);
    }


    [HttpGet("gminy")]
    [ProducesResponseType(typeof(Response<Gmina>.ManyItems), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGminyAsync(
        [FromServices] IGminaRepository repository,
        [FromQuery] GminaParameters parameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(parameters, cancellationToken);
        return Ok(response);
    }


    [HttpGet("gminy/types")]
    [ProducesResponseType(typeof(IDictionary<string, Gmina.Type>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGminyTypesAsync(
        [FromServices] IGminaTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(cancellationToken);
        return Ok(response);
    }


    [HttpGet("miejscowosci")]
    [ProducesResponseType(typeof(Response<Miejscowosc>.ManyItems), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMiejscowosciAsync(
        [FromServices] IMiejscowoscRepository repository,
        [FromQuery] MiejscowoscParameters parameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(parameters, cancellationToken);
        return Ok(response);
    }


    [HttpGet("miejscowosci/types")]
    [ProducesResponseType(typeof(IDictionary<string, Miejscowosc.Type>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMiejscowosciTypesAsync(
        [FromServices] IMiejscowoscTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(cancellationToken);
        return Ok(response);
    }


    [HttpGet("ulicy")]
    [ProducesResponseType(typeof(Response<Ulica>.ManyItems), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUlicyAsync(
        [FromServices] IUlicaRepository repository,
        [FromQuery] UlicaParameters parameters,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(parameters, cancellationToken);
        return Ok(response);
    }


    [HttpGet("ulicy/types")]
    [ProducesResponseType(typeof(IDictionary<int, Ulica.Type>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUlicyTypesAsync(
        [FromServices] IUlicaTypeRepository repository,
        CancellationToken cancellationToken)
    {
        var response = await repository.GetAsync(cancellationToken);
        return Ok(response);
    }
}